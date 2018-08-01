using Logic.Controller;

namespace Logic.AI.Atomic
{
	public abstract class Goal : GPoolObject
	{
		public enum Type
		{
			Undefined,
			Brain,
			GoalMarch,
			Retreat,
			Attack
		}

		public enum Status { Active, Inactive, Completed, Failed }

		public Entity owner { get; internal set; }

		public bool completed => this._status == Status.Completed;

		public bool active => this._status == Status.Active;

		public bool inactive => this._status == Status.Inactive;

		public bool failed => this._status == Status.Failed;

		public abstract Type type { get; }

		protected Status _status = Status.Inactive;

		protected void ActivateIfInactive()
		{
			if ( this.inactive )
				this.Activate();
		}

		protected void ReactivateIfFailed()
		{
			if ( this.failed )
				this._status = Status.Inactive;
		}

		protected abstract void Activate();

		internal virtual void Terminate()
		{
			this._status = Status.Inactive;
			this.owner = null;
		}

		internal abstract Status Process( float dt );

		protected override void InternalDispose()
		{
		}
	}
}