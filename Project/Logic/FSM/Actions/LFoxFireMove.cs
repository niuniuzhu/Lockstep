using Logic.Controller;
using Logic.Steering;

namespace Logic.FSM.Actions
{
	public class LFoxFireMove : BioAction
	{
		public delegate void MoveCompleteHander();

		private MoveCompleteHander _moveCompleteHander;
		private Bio _target;

		protected override void OnEnter( object[] param )
		{
			this._target = ( Bio ) param[0];
			this._moveCompleteHander = ( MoveCompleteHander ) param[1];

			this.owner.steering.pursuit.Set( this._target, this._target.hitPoint );
			this.owner.steering.pursuit.MaxVelocity();
			this.owner.steering.On( SteeringBehaviors.BehaviorType.Pursuit );
		}

		protected override void OnExit()
		{
			this.owner.steering.Off( SteeringBehaviors.BehaviorType.Pursuit );
			this.owner.steering.Off( SteeringBehaviors.BehaviorType.Seek );
			this._target = null;
			this._moveCompleteHander = null;
		}

		protected override void OnUpdate( UpdateContext context )
		{
			if ( this._target.isDead )
			{
				this.owner.steering.Off( SteeringBehaviors.BehaviorType.Pursuit );
				this.owner.steering.seek.Set( this._target.hitPoint );
				this.owner.steering.On( SteeringBehaviors.BehaviorType.Seek );
			}
			if ( this.owner.steering.IsOn( SteeringBehaviors.BehaviorType.Seek ) )
			{
				if ( this.owner.steering.seek.complete )
					this.OnMoveComplete();
			}
			else
			{
				if ( this.owner.steering.pursuit.complete )
					this.OnMoveComplete();
			}
		}

		private void OnMoveComplete()
		{
			this._moveCompleteHander?.Invoke();
			this._moveCompleteHander = null;
		}
	}
}