using System.Collections.Generic;
using UnityEngine;
using View.Controller;

namespace View.Input
{
	public enum PointerType
	{
		Down,
		Up,
		Enter,
		Exit,
		Move,
		Click,
		InitializePotentialDrag,
		BeginDrag,
		Drag,
		EndDrag,
		Drop,
		Scroll
	}

	public class InputData
	{
		public enum FramePressState
		{
			Pressed,
			Released,
			PressedAndReleased,
			NotChanged
		}

		public enum InputButton
		{
			Left = 0,
			Right = 1,
			Middle = 2
		}

		private IInteractive _pointerDown;

		public InputButton button;
		public int clickCount;
		public float clickTime;
		public RaycastResult currentRaycast;
		public Vector2 delta;
		public bool dragging;
		public int pointerId;
		public Vector2 position;
		public Vector2 pressPosition;
		public Vector2 scrollDelta;

		public IInteractive pointerDown
		{
			get { return this._pointerDown; }
			set
			{
				if ( this._pointerDown == value )
					return;

				this.lastDown = this._pointerDown;
				this._pointerDown = value;
			}
		}

		public IInteractive lastDown { get; private set; }
		public IInteractive pointerDrag { get; set; }
		public IInteractive pointerEnter { get; set; }

		public InputData()
		{
			this.pointerId = -1;
			this.position = Vector2.zero;
			this.delta = Vector2.zero;
			this.pressPosition = Vector2.zero;
			this.clickTime = 0.0f;
			this.clickCount = 0;
			this.scrollDelta = Vector2.zero;
			this.dragging = false;
			this.button = InputButton.Left;
		}

		public void Reset()
		{
			this.currentRaycast.Clear();
		}

		public bool IsPointerMoving()
		{
			return this.delta.sqrMagnitude > 0.0f;
		}

		public bool IsScrolling()
		{
			return this.scrollDelta.sqrMagnitude > 0.0f;
		}
	}

	public struct RaycastResult
	{
		public float distance;
		public IInteractive interactive;
		public Vector3 point;
		public Collider collider;

		public void Clear()
		{
			this.distance = 0f;
			this.interactive = null;
			this.collider = null;
		}
	}

	public class PointerInput
	{
		public delegate void OnPointerHandler( IInteractive interactive, PointerType type, InputData data );

		private const float PIXEL_DRAG_THRESHOLD = 5f;

		public const int K_MOUSE_LEFT_ID = 0;
		public const int K_MOUSE_RIGHT_ID = 1;
		public const int K_MOUSE_MIDDLE_ID = 2;

		private readonly Camera _eventCamera;
		private readonly List<IInteractive> _interactives = new List<IInteractive>();
		private readonly List<RaycastResult> _raycasts = new List<RaycastResult>();
		private readonly MouseState _mouseState = new MouseState();
		private readonly Dictionary<int, InputData> _pointerData = new Dictionary<int, InputData>();

		public OnPointerHandler pointerHandler;

		public List<RaycastResult> raycasts => this._raycasts;

		public PointerInput( Camera eventCamera )
		{
			this._eventCamera = eventCamera;
		}

		public void Dispose()
		{
			this._pointerData.Clear();
			this._interactives.Clear();
		}

		public void RegisterInteractive( IInteractive interactive )
		{
			if ( !this._interactives.Contains( interactive ) )
				this._interactives.Add( interactive );
		}

		public bool UnregisterInteractive( IInteractive interactive )
		{
			return this._interactives.Remove( interactive );
		}

		public void Process()
		{
			if ( !this.ProcessTouchEvents() && UnityEngine.Input.mousePresent )
				this.ProcessMouseEvent();
		}

		private bool ProcessTouchEvents()
		{
			for ( int i = 0; i < UnityEngine.Input.touchCount; ++i )
			{
				Touch input = UnityEngine.Input.GetTouch( i );

				if ( input.type == TouchType.Indirect )
					continue;

				bool released;
				bool pressed;
				InputData pointer = this.GetTouchData( input, out pressed, out released );

				this.ProcessTouchPress( pointer, pressed, released );

				if ( !released )
				{
					this.ProcessMove( pointer );
					this.ProcessDrag( pointer );
				}
				else
					this.RemovePointerData( pointer );
			}
			return UnityEngine.Input.touchCount > 0;
		}

		private void RemovePointerData( InputData data )
		{
			this._pointerData.Remove( data.pointerId );
		}

		private void ProcessTouchPress( InputData pointerEvent, bool pressed, bool released )
		{
			IInteractive currentOverObj = pointerEvent.currentRaycast.interactive;
			if ( pressed )
			{
				pointerEvent.delta = Vector2.zero;
				pointerEvent.dragging = false;
				pointerEvent.pressPosition = pointerEvent.position;

				if ( pointerEvent.pointerEnter != currentOverObj )
				{
					this.HandlePointerExitAndEnter( pointerEvent, currentOverObj );
					pointerEvent.pointerEnter = currentOverObj;
				}

				if ( currentOverObj != null && this.pointerHandler != null )
					this.pointerHandler.Invoke( currentOverObj, PointerType.Down, pointerEvent );

				float time = Time.unscaledTime;

				if ( currentOverObj == pointerEvent.lastDown )
				{
					float diffTime = time - pointerEvent.clickTime;
					if ( diffTime < 0.3f )
						++pointerEvent.clickCount;
					else
						pointerEvent.clickCount = 1;

					pointerEvent.clickTime = time;
				}
				else
					pointerEvent.clickCount = 1;

				pointerEvent.pointerDown = currentOverObj;
				pointerEvent.pointerDrag = currentOverObj;
				pointerEvent.clickTime = time;

				if ( pointerEvent.pointerDrag != null && this.pointerHandler != null )
					this.pointerHandler.Invoke( pointerEvent.pointerDrag, PointerType.InitializePotentialDrag, pointerEvent );
			}

			if ( released )
			{
				if ( pointerEvent.pointerDown != null && this.pointerHandler != null )
				{
					this.pointerHandler.Invoke( pointerEvent.pointerDown, PointerType.Up, pointerEvent );
					if ( pointerEvent.pointerDown == currentOverObj )
						this.pointerHandler.Invoke( pointerEvent.pointerDown, PointerType.Click, pointerEvent );
				}

				if ( pointerEvent.pointerDrag != null && pointerEvent.dragging && currentOverObj != null && this.pointerHandler != null )
					this.pointerHandler.Invoke( currentOverObj, PointerType.Drop, pointerEvent );

				pointerEvent.pointerDown = null;

				if ( pointerEvent.pointerDrag != null && pointerEvent.dragging && this.pointerHandler != null )
					this.pointerHandler.Invoke( pointerEvent.pointerDrag, PointerType.EndDrag, pointerEvent );

				pointerEvent.dragging = false;
				pointerEvent.pointerDrag = null;

				if ( pointerEvent.pointerEnter != null && this.pointerHandler != null )
					this.pointerHandler.Invoke( pointerEvent.pointerEnter, PointerType.Exit, pointerEvent );
				pointerEvent.pointerEnter = null;
			}
		}

		private void ProcessMouseEvent()
		{
			MouseState mouseData = this.GetMouseData();

			MouseButtonEventData leftButtonData = mouseData.GetButtonState( InputData.InputButton.Left ).eventData;
			this.ProcessMousePress( leftButtonData );
			this.ProcessMove( leftButtonData.buttonData );
			this.ProcessDrag( leftButtonData.buttonData );

			this.ProcessMousePress( mouseData.GetButtonState( InputData.InputButton.Right ).eventData );
			this.ProcessDrag( mouseData.GetButtonState( InputData.InputButton.Right ).eventData.buttonData );

			this.ProcessMousePress( mouseData.GetButtonState( InputData.InputButton.Middle ).eventData );
			this.ProcessDrag( mouseData.GetButtonState( InputData.InputButton.Middle ).eventData.buttonData );

			if ( !Mathf.Approximately( leftButtonData.buttonData.scrollDelta.sqrMagnitude, 0.0f ) )
			{
				IInteractive currentOverObj = leftButtonData.buttonData.currentRaycast.interactive;
				if ( currentOverObj != null && this.pointerHandler != null )
					this.pointerHandler.Invoke( currentOverObj, PointerType.Scroll, leftButtonData.buttonData );
			}
		}

		private void ProcessMousePress( MouseButtonEventData data )
		{
			InputData pointerEvent = data.buttonData;
			IInteractive currentOverObj = pointerEvent.currentRaycast.interactive;

			if ( data.PressedThisFrame() )
			{
				pointerEvent.delta = Vector2.zero;
				pointerEvent.dragging = false;
				pointerEvent.pressPosition = pointerEvent.position;

				if ( currentOverObj != null && this.pointerHandler != null )
					this.pointerHandler.Invoke( currentOverObj, PointerType.Down, pointerEvent );

				float time = Time.unscaledTime;
				if ( currentOverObj == pointerEvent.lastDown )
				{
					float diffTime = time - pointerEvent.clickTime;
					if ( diffTime < 0.3f )
						++pointerEvent.clickCount;
					else
						pointerEvent.clickCount = 1;

					pointerEvent.clickTime = time;
				}
				else
					pointerEvent.clickCount = 1;

				pointerEvent.pointerDown = currentOverObj;
				pointerEvent.pointerDrag = currentOverObj;
				pointerEvent.clickTime = time;

				if ( pointerEvent.pointerDrag != null && this.pointerHandler != null )
					this.pointerHandler.Invoke( pointerEvent.pointerDrag, PointerType.InitializePotentialDrag, pointerEvent );
			}

			if ( data.ReleasedThisFrame() )
			{
				if ( pointerEvent.pointerDown != null && this.pointerHandler != null )
				{
					this.pointerHandler.Invoke( pointerEvent.pointerDown, PointerType.Up, pointerEvent );

					if ( pointerEvent.pointerDown == currentOverObj )
						this.pointerHandler.Invoke( pointerEvent.pointerDown, PointerType.Click, pointerEvent );
				}

				if ( pointerEvent.pointerDrag != null && pointerEvent.dragging && currentOverObj != null && this.pointerHandler != null )
					this.pointerHandler.Invoke( currentOverObj, PointerType.Drop, pointerEvent );

				pointerEvent.pointerDown = null;

				if ( pointerEvent.pointerDrag != null && pointerEvent.dragging && this.pointerHandler != null )
					this.pointerHandler.Invoke( pointerEvent.pointerDrag, PointerType.EndDrag, pointerEvent );

				pointerEvent.dragging = false;
				pointerEvent.pointerDrag = null;

				if ( currentOverObj != pointerEvent.pointerEnter )
				{
					this.HandlePointerExitAndEnter( pointerEvent, null );
					this.HandlePointerExitAndEnter( pointerEvent, currentOverObj );
				}
			}
		}

		private void ProcessMove( InputData pointerEvent )
		{
			if ( this.pointerHandler != null )
				this.pointerHandler.Invoke( pointerEvent.currentRaycast.interactive, PointerType.Move, pointerEvent );

			this.HandlePointerExitAndEnter( pointerEvent, pointerEvent.currentRaycast.interactive );
		}

		private void HandlePointerExitAndEnter( InputData currentPointerData, IInteractive newEnterTarget )
		{
			if ( currentPointerData.pointerEnter == newEnterTarget )
				return;

			if ( newEnterTarget == null )
			{
				if ( currentPointerData.pointerEnter != null )
				{
					if ( this.pointerHandler != null )
						this.pointerHandler.Invoke( currentPointerData.pointerEnter, PointerType.Exit, currentPointerData );
					currentPointerData.pointerEnter = null;
				}
				return;
			}

			currentPointerData.pointerEnter = newEnterTarget;

			if ( this.pointerHandler != null )
				this.pointerHandler.Invoke( currentPointerData.pointerEnter, PointerType.Enter, currentPointerData );
		}

		private void ProcessDrag( InputData pointerEvent )
		{
			bool moving = pointerEvent.IsPointerMoving();

			if ( moving )
			{
				if ( pointerEvent.pointerDrag != null )
				{
					if ( !pointerEvent.dragging )
					{
						if ( ShouldStartDrag( pointerEvent.pressPosition, pointerEvent.position, PIXEL_DRAG_THRESHOLD ) )
						{
							if ( this.pointerHandler != null )
								this.pointerHandler.Invoke( pointerEvent.pointerDrag, PointerType.BeginDrag, pointerEvent );
							pointerEvent.dragging = true;
						}
					}
				}
			}

			if ( pointerEvent.dragging && moving && pointerEvent.pointerDrag != null && this.pointerHandler != null )
				this.pointerHandler.Invoke( pointerEvent.pointerDrag, PointerType.Drag, pointerEvent );
		}

		private static bool ShouldStartDrag( Vector2 pressPos, Vector2 currentPos, float threshold )
		{
			return ( pressPos - currentPos ).sqrMagnitude >= threshold * threshold;
		}

		private MouseState GetMouseData()
		{
			InputData leftData;
			bool created = this.GetPointerData( K_MOUSE_LEFT_ID, out leftData, true );

			leftData.Reset();

			if ( created )
				leftData.position = UnityEngine.Input.mousePosition;

			Vector2 pos = UnityEngine.Input.mousePosition;
			leftData.delta = pos - leftData.position;
			leftData.position = pos;

			leftData.scrollDelta = UnityEngine.Input.mouseScrollDelta;
			leftData.button = InputData.InputButton.Left;
			leftData.currentRaycast = this.Raycast( leftData, this._raycasts );

			InputData rightData;
			this.GetPointerData( K_MOUSE_RIGHT_ID, out rightData, true );
			this.CopyFromTo( leftData, rightData );
			rightData.button = InputData.InputButton.Right;

			InputData middleData;
			this.GetPointerData( K_MOUSE_MIDDLE_ID, out middleData, true );
			this.CopyFromTo( leftData, middleData );
			middleData.button = InputData.InputButton.Middle;

			this._mouseState.SetButtonState( InputData.InputButton.Left, StateForMouseButton( 0 ), leftData );
			this._mouseState.SetButtonState( InputData.InputButton.Right, StateForMouseButton( 1 ), rightData );
			this._mouseState.SetButtonState( InputData.InputButton.Middle, StateForMouseButton( 2 ), middleData );

			return this._mouseState;
		}

		private RaycastResult Raycast( InputData data, List<RaycastResult> results )
		{
			results.Clear();

			Ray ray = this._eventCamera.ScreenPointToRay( data.position );

			RaycastHit[] raycastHits = Physics.RaycastAll( ray, Mathf.Infinity );

			int c2 = raycastHits.Length;
			for ( int i = 0; i < c2; i++ )
			{
				RaycastHit hitInfo = raycastHits[i];
				int c1 = this._interactives.Count;
				for ( int j = 0; j < c1; j++ )
				{
					IInteractive interactive = this._interactives[j];

					if ( interactive.collider != hitInfo.collider )
						continue;

					RaycastResult result;
					result.interactive = interactive;
					result.point = hitInfo.point;
					result.distance = hitInfo.distance;
					result.collider = hitInfo.collider;
					results.Add( result );
				}
			}

			RaycastResult first;
			if ( results.Count > 0 )
			{
				if ( results.Count == 1 )
					first = results[0];
				else
				{
					results.Sort( ( x, y ) =>
					{
						float d1 = x.distance;
						float d2 = y.distance;
						if ( d1 > d2 )
							return 1;
						if ( d1 < d2 )
							return -1;
						return 0;
					} );
					first = results[0];
				}
			}
			else
				first = new RaycastResult();
			return first;
		}

		private bool GetPointerData( int id, out InputData data, bool create )
		{
			if ( !this._pointerData.TryGetValue( id, out data ) && create )
			{
				data = new InputData { pointerId = id };
				this._pointerData.Add( id, data );
				return true;
			}
			return false;
		}

		private void CopyFromTo( InputData @from, InputData @to )
		{
			@to.position = @from.position;
			@to.pressPosition = @from.pressPosition;
			@to.delta = @from.delta;
			@to.scrollDelta = @from.scrollDelta;
			@to.currentRaycast = @from.currentRaycast;
		}

		private static InputData.FramePressState StateForMouseButton( int buttonId )
		{
			bool pressed = UnityEngine.Input.GetMouseButtonDown( buttonId );
			bool released = UnityEngine.Input.GetMouseButtonUp( buttonId );
			if ( pressed && released )
				return InputData.FramePressState.PressedAndReleased;
			if ( pressed )
				return InputData.FramePressState.Pressed;
			if ( released )
				return InputData.FramePressState.Released;
			return InputData.FramePressState.NotChanged;
		}

		private InputData GetTouchData( Touch input, out bool pressed, out bool released )
		{
			InputData pointerData;
			bool created = this.GetPointerData( input.fingerId, out pointerData, true );

			pointerData.Reset();

			pressed = created || ( input.phase == TouchPhase.Began );
			released = ( input.phase == TouchPhase.Canceled ) || ( input.phase == TouchPhase.Ended );

			if ( created )
				pointerData.position = input.position;

			if ( pressed )
				pointerData.delta = Vector2.zero;
			else
				pointerData.delta = input.position - pointerData.position;

			pointerData.position = input.position;
			pointerData.button = InputData.InputButton.Left;
			pointerData.currentRaycast = this.Raycast( pointerData, this._raycasts );
			return pointerData;
		}

		public InputData GetLastPointerEventData( int id )
		{
			InputData data;
			this.GetPointerData( id, out data, false );
			return data;
		}

		private class ButtonState
		{
			private InputData.InputButton _button = InputData.InputButton.Left;
			public MouseButtonEventData eventData { get; set; }

			public InputData.InputButton button
			{
				get { return this._button; }
				set { this._button = value; }
			}
		}

		private class MouseState
		{
			private readonly List<ButtonState> _trackedButtons = new List<ButtonState>();

			public bool AnyPressesThisFrame()
			{
				int count = this._trackedButtons.Count;
				for ( int i = 0; i < count; i++ )
				{
					if ( this._trackedButtons[i].eventData.PressedThisFrame() )
						return true;
				}
				return false;
			}

			public bool AnyReleasesThisFrame()
			{
				int count = this._trackedButtons.Count;
				for ( int i = 0; i < count; i++ )
				{
					if ( this._trackedButtons[i].eventData.ReleasedThisFrame() )
						return true;
				}
				return false;
			}

			public ButtonState GetButtonState( InputData.InputButton button )
			{
				ButtonState tracked = null;
				int count = this._trackedButtons.Count;
				for ( int i = 0; i < count; i++ )
				{
					if ( this._trackedButtons[i].button == button )
					{
						tracked = this._trackedButtons[i];
						break;
					}
				}

				if ( tracked == null )
				{
					tracked = new ButtonState { button = button, eventData = new MouseButtonEventData() };
					this._trackedButtons.Add( tracked );
				}
				return tracked;
			}

			public void SetButtonState( InputData.InputButton button, InputData.FramePressState stateForMouseButton,
										InputData data )
			{
				ButtonState toModify = this.GetButtonState( button );
				toModify.eventData.buttonState = stateForMouseButton;
				toModify.eventData.buttonData = data;
			}
		}

		private class MouseButtonEventData
		{
			public InputData buttonData;
			public InputData.FramePressState buttonState;

			public bool PressedThisFrame()
			{
				return this.buttonState == InputData.FramePressState.Pressed ||
					   this.buttonState == InputData.FramePressState.PressedAndReleased;
			}

			public bool ReleasedThisFrame()
			{
				return this.buttonState == InputData.FramePressState.Released ||
					   this.buttonState == InputData.FramePressState.PressedAndReleased;
			}
		}
	}
}