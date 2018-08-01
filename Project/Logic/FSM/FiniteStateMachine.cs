using System;
using System.Collections.Generic;
using Logic.Misc;

namespace Logic.FSM
{
	public sealed class FiniteStateMachine
	{
		public FSMState previousState { get; private set; }

		public FSMState globalState { get; private set; }

		public FSMState currState { get; private set; }

		public object owner { get; private set; }

		public bool enableDebug { get; set; }

		private bool _enable;
		public bool enable
		{
			get { return this._enable; }
			set
			{
				if ( this._enable == value )
					return;

				this._enable = value;
				if ( this._enable )
					this.OnEnable();
				else
					this.OnDisable();
			}
		}

		public FiniteStateMachine parent { get; private set; }

		public bool isRunning { get; private set; }

		public bool disposed { get; private set; }

		private readonly Dictionary<FSMStateType, FSMState> _states = new Dictionary<FSMStateType, FSMState>();

		private readonly List<FiniteStateMachine> _subFSMList = new List<FiniteStateMachine>();

		public FSMState this[FSMStateType type] => this._states[type];

		public FiniteStateMachine( object owner )
		{
			this.owner = owner;
			this.enable = true;
		}

		public void Dispose()
		{
			if ( this.disposed )
				return;

			foreach ( FiniteStateMachine subFSM in this._subFSMList )
				subFSM.Dispose();
			this._subFSMList.Clear();

			this.disposed = true;

			this.Stop();

			this._states.Clear();

			this.owner = null;
			this.parent = null;
			this.globalState = this.currState = this.previousState = null;
		}

		public void Start()
		{
			if ( this.isRunning )
				return;

			this.isRunning = true;

			int count = this._subFSMList.Count;
			for ( int i = 0; i < count; i++ )
			{
				FiniteStateMachine subFSM = this._subFSMList[i];
				subFSM.Start();
			}

			this.globalState?.Enter( null );
		}

		public void Stop()
		{
			if ( !this.isRunning )
				return;

			this.isRunning = false;

			this.globalState?.Exit();
			this.currState?.Exit();

			int count = this._subFSMList.Count;
			for ( int i = 0; i < count; i++ )
			{
				FiniteStateMachine subFSM = this._subFSMList[i];
				subFSM.Stop();
			}
		}

		/// <summary>
		/// 创建状态
		/// </summary>
		/// <param name="type">类型</param>
		/// <returns>被创建的状态</returns>
		public FSMState CreateState( FSMStateType type )
		{
			if ( this._states.ContainsKey( type ) )
				throw new Exception( $"Specified name '{type}' of component already exists" );

			FSMState state = new FSMState( type, this );
			this._states.Add( type, state );

			return state;
		}

		/// <summary>
		/// 销毁状态
		/// </summary>
		/// <param name="type">类型</param>
		public void DestroyState( FSMStateType type )
		{
			this._states.Remove( type );
		}

		/// <summary>
		/// 创建全局状态
		/// </summary>
		/// <param name="type">类型</param>
		/// <returns>全局状态</returns>
		public FSMState CreateGlobalState( FSMStateType type )
		{
			if ( this.globalState != null )
				throw new Exception( "A global state already exist." );

			if ( this._states.ContainsKey( type ) )
				throw new Exception( $"Specified name '{type}' of component already exists" );

			this.globalState = new FSMState( type, this );
			if ( this.isRunning )
				this.globalState.Enter( null );
			return this.globalState;
		}

		public void DestroyGlobalState()
		{
			this.globalState = null;
		}

		/// <summary>
		/// 状态转换
		/// </summary>
		/// <param name="type">指定需要转换的状态</param>
		/// <param name="force">是否强制转换(即需要转换的状态是当前状态的情况下仍然转换)</param>
		/// <param name="param">参数</param>
		public bool ChangeState( FSMStateType type, bool force = false, params object[] param )
		{
			FSMState state;
			this._states.TryGetValue( type, out state );
			if ( state != null )
				return this.InternalChangeState( state, force, param );

			LLogger.Warning( "State '{0}' not exist.", type );
			return false;
		}

		private bool InternalChangeState( FSMState state, bool force = false, object[] param = null )
		{
			if ( !force && this.currState == state )
				return false;

			if ( this.enableDebug )
				LLogger.Log( "Change state:{0}", state.type );

			this.previousState = this.currState;
			this.currState?.Exit();

			this.currState = state;
			this.currState?.Enter( param );

			return true;
		}

		public void RevertToPreviousState()
		{
			if ( this.previousState != null )
				this.InternalChangeState( this.previousState );
		}

		public void Push( FSMStateType type, object[] param = null )
		{
			if ( this.enableDebug )
				LLogger.Log( "Change state:{0}", type );

			if ( !this._states.ContainsKey( type ) )
				LLogger.Log( "State '{0}' not found.", type );
			else
				this.Push( this._states[type], param );
		}

		public void Push( FSMState state, object[] param = null )
		{
			if ( this.currState == state )
				return;
			this.previousState = this.currState;
			this.currState = state;
			this.currState.Enter( param );
		}

		public void Pop()
		{
			FSMState state = this.currState;
			state.Exit();
			this.currState = this.previousState;
			this.previousState = state;
		}

		/// <summary>
		/// 创建子状态机
		/// </summary>
		/// <param name="subFSM"></param>
		/// <returns>子状态机</returns>
		public void AddSubFSM( FiniteStateMachine subFSM )
		{
			subFSM.owner = this.owner;
			subFSM.parent = this;
			subFSM.enable = this.enable;
			this._subFSMList.Add( subFSM );
		}

		/// <summary>
		/// 销毁所有子状态机
		/// </summary>
		public void DestroyAllSubFSM()
		{
			foreach ( FiniteStateMachine subFSM in this._subFSMList )
				subFSM.Dispose();
			this._subFSMList.Clear();
		}

		/// <summary>
		/// 销毁子状态机
		/// </summary>
		public void DestroySubFSM( FiniteStateMachine subFSM )
		{
			subFSM.Dispose();
			this._subFSMList.Remove( subFSM );
		}

		/// <summary>
		/// 获取子状态机数量
		/// </summary>
		public int SubFSMCount()
		{
			return this._subFSMList.Count;
		}

		public FiniteStateMachine GetSubFsm( int index )
		{
			return this._subFSMList[index];
		}

		/// <summary>
		/// 获取指定名称的状态
		/// </summary>
		/// <param name="type">类型</param>
		/// <returns>状态</returns>
		public FSMState GetState( FSMStateType type )
		{
			FSMState state;
			this._states.TryGetValue( type, out state );
			return state;
		}

		/// <summary>
		/// 获取状态数量
		/// </summary>
		/// <returns>状态数量</returns>
		public int StateCount()
		{
			return this._states.Count;
		}

		public void Update( UpdateContext context )
		{
			if ( !this.isRunning || !this.enable ) return;
			this.globalState?.UpdateHandler( context );
			this.currState?.UpdateHandler( context );
			int count = this._subFSMList.Count;
			for ( int i = 0; i < count; i++ )
			{
				FiniteStateMachine subFSM = this._subFSMList[i];
				subFSM.Update( context );
			}
		}

		private void OnEnable()
		{
			int count = this._subFSMList.Count;
			for ( int i = 0; i < count; i++ )
			{
				FiniteStateMachine subFSM = this._subFSMList[i];
				subFSM.OnEnable();
			}
		}

		private void OnDisable()
		{
			int count = this._subFSMList.Count;
			for ( int i = 0; i < count; i++ )
			{
				FiniteStateMachine subFSM = this._subFSMList[i];
				subFSM.OnDisable();
			}
		}
	}
}