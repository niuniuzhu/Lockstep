using Core.Math;
using Logic.Controller;
using Logic.FSM;
using Logic.Misc;
using Logic.Steering;

namespace Logic.BuffStateImpl
{
	public class BSCharm : BSBase
	{
		private const float DETECT_INTERVAL = 0.3f;
		
		private double _time;
		private Vec3 _lastTargetPos;
		private Bio _target;

		protected override void CreateInternal()
		{
			this.owner.brain.enable = false;
			this.owner.UpdateVelocity( Vec3.zero );

			SyncEventHelper.ChangeState( this.owner.rid, FSMStateType.Idle );
			this.owner.ChangeState( FSMStateType.Idle );

			this._target = this.buff.caster;
			this._target.AddRef();
			this._lastTargetPos = Vec3.zero;
			this._time = DETECT_INTERVAL;
		}

		protected override void DestroyInternal()
		{
			this.owner.brain.Rearbitrate();
			this.owner.brain.enable = true;
			this.owner.steering.Off( SteeringBehaviors.BehaviorType.FollowPath );
			this.owner.steering.Off( SteeringBehaviors.BehaviorType.ObstacleAvoidance );
			this.owner.UpdateVelocity( Vec3.zero );
			this._target.RedRef();
		}

		protected override void UpdateInternal( UpdateContext context )
		{
			if ( !this.owner.CanCharmed() )
				return;

			this._time += context.deltaTime;
			if ( this._time >= DETECT_INTERVAL )
			{
				this._time = 0f;
				if ( this._lastTargetPos != this._target.property.position )
				{
					Vec3[] corners = this.owner.battle.GetPathCorners( this.owner.property.position, this._target.property.position );
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
					this._lastTargetPos = this._target.property.position;
				}
			}
			if ( this.owner.steering.followPath.complete )
			{
				this.owner.UpdateVelocity( Vec3.zero );
			}
		}
	}
}