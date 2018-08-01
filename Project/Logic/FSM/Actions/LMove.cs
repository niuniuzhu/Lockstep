using Core.Math;
using Logic.Misc;
using Logic.Steering;

namespace Logic.FSM.Actions
{
	public class LMove : BioAction
	{
		private Vec3 _targetPoint;

		protected override void OnEnter( object[] param )
		{
			this._targetPoint = ( Vec3 ) param[0];
			Vec3[] corners = this.owner.battle.GetPathCorners( this.owner.property.position, this._targetPoint );
			if ( corners == null )
			{
				SyncEventHelper.ChangeState( this.owner.rid, FSMStateType.Idle );
				this.owner.ChangeState( FSMStateType.Idle );
				return;
			}
			this.owner.steering.followPath.Set( corners );
			this.owner.steering.followPath.MaxVelocity();
			this.owner.steering.On( SteeringBehaviors.BehaviorType.FollowPath );
			if ( this.owner.property.ignoreVolumetric == 0 )
				this.owner.steering.On( SteeringBehaviors.BehaviorType.ObstacleAvoidance );
		}

		protected override void OnExit()
		{
			this.owner.steering.Off( SteeringBehaviors.BehaviorType.FollowPath );
			this.owner.steering.Off( SteeringBehaviors.BehaviorType.ObstacleAvoidance );
		}

		protected override void OnUpdate( UpdateContext context )
		{
			if ( !this.owner.steering.followPath.complete )
				return;

			SyncEventHelper.ChangeState( this.owner.rid, FSMStateType.Idle );
			this.owner.ChangeState( FSMStateType.Idle );
		}
	}
}