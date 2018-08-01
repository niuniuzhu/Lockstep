using Core.Math;
using Logic.Controller;
using Logic.Misc;
using Logic.Steering;

namespace Logic.FSM.Actions
{
	public class LTrack : BioAction
	{
		private const float DETECT_INTERVAL = 0.3f;

		private Bio _target;
		private double _time;
		private Vec3 _lastTargetPos;

		protected override void OnEnter( object[] param )
		{
			this._target = ( Bio ) param[0];

			this._target.AddRef();
			this._lastTargetPos = Vec3.zero;
			this._time = DETECT_INTERVAL;
		}

		protected override void OnExit()
		{
			this.owner.steering.Off( SteeringBehaviors.BehaviorType.FollowPath );
			this.owner.steering.Off( SteeringBehaviors.BehaviorType.ObstacleAvoidance );

			this._target.RedRef();
			this._target = null;
		}

		protected override void OnUpdate( UpdateContext context )
		{
			bool complete = this._target.isDead;

			if ( complete )
			{
				//todo 此处应处理追踪失败
			}
			else
			{
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
			}
			if ( this.owner.steering.followPath.complete )
			{
				this.owner.UpdateVelocity( Vec3.zero );
			}
		}
	}
}