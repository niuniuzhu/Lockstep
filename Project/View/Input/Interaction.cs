using Logic.Controller;
using View.Controller;

namespace View.Input
{
	public class Interaction
	{
		public VBattle battle { get; private set; }

		private AbsInteractionState[] _states;

		private int _currentStateIndex;
		private AbsInteractionState _currentState;
		private bool _isOverUIObject;

		public Interaction( VBattle battle )
		{
			this.battle = battle;
			this.battle.input.pointerHandler = this.PointerHandler;

			this._states = new AbsInteractionState[2];
			this._states[0] = new CommonInteraction( this );
			this._states[1] = new SkillInteraction( this );
			this._currentState = this._states[0];
		}

		public void Dispose()
		{
			this.DropSkill();

			this.battle = null;
			this._states = null;
		}

		private void PointerHandler( IInteractive interactive, PointerType type, InputData data )
		{

			switch ( type )
			{
				case PointerType.Down:
					this._isOverUIObject = UIEventProxy.IsOverUIObject();
					if ( !this._isOverUIObject )
						this.HandlerPointerDown( interactive, data );
					break;

				case PointerType.Up:
					if ( !this._isOverUIObject )
						this.HandlerPointerUp( interactive, data );
					this._isOverUIObject = false;
					break;

				case PointerType.Move:
					if ( !this._isOverUIObject )
						this.HandlerPointerMove( interactive, data );
					break;

				case PointerType.Click:
					if ( !this._isOverUIObject )
						this.HandlerPointerClick( interactive, data );
					break;
			}
		}

		private void HandlerPointerDown( IInteractive interactive, InputData data )
		{
			this._currentState.HandlerPointerDown( interactive, data );
		}

		private void HandlerPointerUp( IInteractive interactive, InputData data )
		{
			this._currentState.HandlerPointerUp( interactive, data );
		}

		private void HandlerPointerMove( IInteractive interactive, InputData data )
		{
			this._currentState.HandlerPointerMove( interactive, data );
		}

		private void HandlerPointerClick( IInteractive interactive, InputData data )
		{
			this._currentState.HandlerPointerClick( interactive, data );
		}

		private void ChangeState( int index, params object[] param )
		{
			if ( this._currentStateIndex == index )
				return;
			this._states[this._currentStateIndex].OnExit();
			this._currentStateIndex = index;
			this._currentState = this._states[this._currentStateIndex];
			this._currentState.OnEnter( param );
		}

		public void PickSkill( Skill skill )
		{
			this.ChangeState( 1, skill );
		}

		public void DropSkill()
		{
			this.ChangeState( 0 );
		}
	}
}