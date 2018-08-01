using Core.Math;
using Logic.Controller;
using Logic.Misc;
using Logic.Property;
using Logic.Steering;

namespace Logic.FSM.Actions
{
	public class LPursue : BioAction
	{
		private const float DETECT_INTERVAL = 0.3f;

		private float _time;
		private Vec3 _lastTargetPos;
		private bool _moveComplete;
		private bool _rotComplete;

		private Skill _skill;
		private Bio _target;
		private Vec3 _targetPoint;
		private bool _recalPath;

		protected override void OnEnter( object[] param )
		{
			this._skill = ( Skill )param[0];
			this._target = ( Bio )param[1];
			this._targetPoint = ( Vec3 )param[2];

			this._target?.AddRef();
			this._lastTargetPos = Vec3.zero;
			this._time = DETECT_INTERVAL;
			this._moveComplete = this._rotComplete = false;
			this._recalPath = true;
		}

		protected override void OnExit()
		{
			this.owner.steering.Off( SteeringBehaviors.BehaviorType.FollowPath );
			this.owner.steering.Off( SteeringBehaviors.BehaviorType.ObstacleAvoidance );

			this._skill = null;
			this._target?.RedRef();
			this._target = null;
		}

		private Vec3 CalcTargetPoint()
		{
			return this._target?.property.position ?? this._targetPoint;
		}

		protected override void OnUpdate( UpdateContext context )
		{
			if ( this._target != null &&
				 ( this._target.isDead || !this.owner.WithinFov( this._target ) ) ) //todo 超过fov放弃是否合理?
			{
				SyncEventHelper.ChangeState( this.owner.rid, FSMStateType.Idle );
				this.owner.ChangeState( FSMStateType.Idle );
				return;
			}

			if ( !this._moveComplete )
			{
				Vec3 targetPos = this.CalcTargetPoint();
				float distance = this._skill.distance;
				if ( ( this.owner.property.position - targetPos ).SqrMagnitude() <= distance * distance )
				{
					if ( this._skill.ignoreObstacles )
						this._moveComplete = true;
					else
					{
						//检测和目标之间是否有障碍物
						if ( !this.owner.battle.NavMeshRaycast( this.owner.property.position, targetPos, out _, out _ ) )
							this._moveComplete = true;
					}
				}
				if ( this._moveComplete )
				{
					this.owner.UpdateVelocity( Vec3.zero );
					this.owner.steering.Off( SteeringBehaviors.BehaviorType.FollowPath );
					this.owner.steering.Off( SteeringBehaviors.BehaviorType.ObstacleAvoidance );
				}
			}

			if ( this._moveComplete &&
				 !this._rotComplete )
			{
				Vec3 targetPos = this._target?.property.position ?? this._targetPoint;
				Vec3 toDir = Vec3.Normalize( targetPos - this.owner.property.position );
				float angle = Vec3.Angle( this.owner.property.direction, toDir );
				if ( angle < 15f ) //x度内让他可以攻击了
					this._rotComplete = true;
				else
				{
					this.owner.property.Equal( Attr.Direction, Vec3.Slerp( this.owner.property.direction, toDir,
																		   context.deltaTime * this.owner.rotSpeed *
																		   this.owner.property.moveSpeedFactor * 2f ) );
				}
			}

			if ( this._moveComplete &&
				 this._rotComplete )
			{
				this.owner.Attack( this._skill, this._target, this._targetPoint );
				return;
			}

			if ( this._target != null )
			{
				this._time += context.deltaTime;
				if ( this._time >= DETECT_INTERVAL )
				{
					this._time = 0f;
					if ( this._lastTargetPos != this._target.property.position )
					{
						this._recalPath = true;
						this._lastTargetPos = this._target.property.position;
					}
				}
			}

			if ( !this._recalPath )
				return;

			this._recalPath = false;
			Vec3[] corners = this.owner.battle.GetPathCorners( this.owner.property.position, this.CalcTargetPoint() );
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
			this._moveComplete = false;
		}
	}
}