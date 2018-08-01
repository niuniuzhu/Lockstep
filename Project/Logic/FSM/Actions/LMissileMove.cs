using Core.Math;
using Logic.Model;
using Logic.Steering;

namespace Logic.FSM.Actions
{
	public class LMissileMove : MissileAction
	{
		public delegate void MoveCompleteHander();

		private MoveCompleteHander _moveCompleteHander;
		private Vec3 _lastTargetPos;
		private float _time;

		protected override void OnEnter( object[] param )
		{
			this._moveCompleteHander = ( MoveCompleteHander )param[0];

			switch ( this.owner.flightType )
			{
				case FlightType.Parabola:
					{
						Vec3 targetPoint;
						if ( this.owner.target != null )
						{
							targetPoint = this.owner.target.PointToWorld( this.owner.target.hitPoint );
							this._lastTargetPos = this.owner.target.property.position;
						}
						else
						{
							targetPoint = this.owner.targetPoint;
							this._lastTargetPos = targetPoint;
						}
						this.owner.steering.parabola.Set( this.owner.arc, targetPoint );
						this.owner.steering.On( SteeringBehaviors.BehaviorType.Parabola );
					}
					break;

				case FlightType.Target:
					{
						this.owner.steering.pursuit.Set( this.owner.target, this.owner.hitPoint );
						this.owner.steering.pursuit.MaxVelocity();
						this.owner.steering.On( SteeringBehaviors.BehaviorType.Pursuit );
					}
					break;

				case FlightType.Point:
					{
						this.owner.steering.seek.Set( this.owner.target?.property.position ?? this.owner.targetPoint );
						this.owner.steering.seek.MaxVelocity();
						this.owner.steering.On( SteeringBehaviors.BehaviorType.Seek );
					}
					break;

				case FlightType.Directional:
					{
						Vec3 targetPoint = this.owner.target?.property.position ?? this.owner.targetPoint;
						targetPoint.y = this.owner.property.position.y;
						targetPoint = this.owner.property.position + Vec3.Normalize( targetPoint - this.owner.property.position ) * 9999f;
						this.owner.steering.seek.Set( targetPoint );
						this.owner.steering.seek.MaxVelocity();
						this.owner.steering.On( SteeringBehaviors.BehaviorType.Seek );
						this._time = 0f;
					}
					break;
			}
		}

		protected override void OnExit()
		{
			this.owner.steering.Off( SteeringBehaviors.BehaviorType.Pursuit );
			this.owner.steering.Off( SteeringBehaviors.BehaviorType.Seek );
			this.owner.steering.Off( SteeringBehaviors.BehaviorType.Parabola );
			this._moveCompleteHander = null;
		}

		protected override void OnUpdate( UpdateContext context )
		{
			switch ( this.owner.flightType )
			{
				case FlightType.Directional:
					{
						this._time += context.deltaTime;
						if ( this._time >= this.owner.duration )
						{
							this.OnMoveComplete();
							return;
						}
					}
					break;
			}

			if ( this.owner.target != null )
			{
				if ( this.owner.target.isDead )
				{
					if ( this.owner.steering.IsOn( SteeringBehaviors.BehaviorType.Pursuit ) )
					{
						this.owner.steering.Off( SteeringBehaviors.BehaviorType.Pursuit );
						this.owner.steering.seek.Set( this.owner.target.hitPoint );
						this.owner.steering.On( SteeringBehaviors.BehaviorType.Seek );
					}
				}
				else
				{
					if ( this.owner.flightType == FlightType.Parabola && this.owner.target != null && this.owner.target.property.position != this._lastTargetPos )
					{
						this.owner.steering.Off( SteeringBehaviors.BehaviorType.Parabola );
						this._lastTargetPos = this.owner.target.property.position;
						this.owner.steering.pursuit.Set( this.owner.target, this.owner.hitPoint );
						this.owner.steering.On( SteeringBehaviors.BehaviorType.Pursuit );
					}
				}
			}
			if ( this.owner.steering.IsOn( SteeringBehaviors.BehaviorType.Seek ) )
			{
				if ( this.owner.steering.seek.complete )
					this.OnMoveComplete();
			}
			else if ( this.owner.steering.IsOn( SteeringBehaviors.BehaviorType.Parabola ) )
			{
				if ( this.owner.steering.parabola.complete )
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