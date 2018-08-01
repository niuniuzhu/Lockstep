namespace Logic.FSM.Actions
{
	public abstract class AbsAction : IAction
	{
		public virtual FSMState state { get; set; }

		public bool enable { get; set; }

		protected AbsAction()
		{
			this.enable = true;
		}

		public void Enter( object[] param )
		{
			if ( !this.enable ) return;
			this.OnEnter( param );
		}

		public void Exit()
		{
			if ( !this.enable ) return;
			this.OnExit();
		}

		public void Update( UpdateContext context )
		{
			if ( !this.enable ) return;

			this.OnUpdate( context );
		}

		protected virtual void OnEnter( object[] param )
		{
		}

		protected virtual void OnExit()
		{
		}

		protected virtual void OnUpdate( UpdateContext context )
		{
		}
	}
}