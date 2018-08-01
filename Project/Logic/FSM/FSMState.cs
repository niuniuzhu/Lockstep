using System.Collections.Generic;
using System.Linq;
using Logic.FSM.Actions;

namespace Logic.FSM
{
	public enum FSMStateType
	{
		Global,
		Idle,
		Move,
		Track,
		Pursue,
		Attack,
		Dead
	}

	public class FSMState
	{
		public object owner => this.fsm.owner;

		public FSMStateType type { get; private set; }

		public FiniteStateMachine fsm { get; private set; }

		private readonly List<IAction> _actions = new List<IAction>();

		public int actionCount => this._actions.Count;

		internal FSMState( FSMStateType type, FiniteStateMachine fsm )
		{
			this.type = type;
			this.fsm = fsm;
		}

		public void Enter( object[] param )
		{
			int count = this._actions.Count;
			for ( int i = 0; i < count; i++ )
			{
				IAction action = this._actions[i];
				action.Enter( param );
			}
		}

		public void Exit()
		{
			int count = this._actions.Count;
			for ( int i = 0; i < count; i++ )
			{
				IAction action = this._actions[i];
				action.Exit();
			}
		}

		internal void UpdateHandler( UpdateContext context )
		{
			int count = this._actions.Count;
			for ( int i = 0; i < count; i++ )
			{
				IAction action = this._actions[i];
				action.Update( context );
			}
		}

		public T CreateAction<T>() where T : IAction, new()
		{
			IAction action = new T();
			action.state = this;
			this._actions.Add( action );
			return ( T ) action;
		}

		public T GetAction<T>() where T : IAction
		{
			foreach ( T action in this._actions.OfType<T>() )
				return action;
			return default( T );
		}

		public void DestroyAction( IAction action )
		{
			this._actions.Remove( action );
		}
	}
}