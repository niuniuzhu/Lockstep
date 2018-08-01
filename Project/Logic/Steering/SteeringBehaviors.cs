using System;
using Core.Math;
using Logic.Controller;
using Logic.Property;

namespace Logic.Steering
{
	public class SteeringBehaviors
	{
		[Flags]
		public enum BehaviorType
		{
			None = 1 << 0,
			Seek = 1 << 1,
			Flee = 1 << 2,
			Arrive = 1 << 3,
			Wander = 1 << 4,
			ObstacleAvoidance = 1 << 5,
			FollowPath = 1 << 6,
			Pursuit = 1 << 7,
			Evade = 1 << 8,
			OffsetPursuit = 1 << 9,
			Dash = 1 << 10,
			Parabola = 1 << 11,
			Repelled = 1 << 12,
		};

		public Entity owner { get; private set; }
		public Seek seek { get; private set; }
		public Flee flee { get; private set; }
		public Arrive arrive { get; private set; }
		public Pursuit pursuit { get; private set; }
		public Evade evade { get; private set; }
		public Wander wander { get; private set; }
		public ObstacleAvoidance obstacleAvoidance { get; private set; }
		public FollowPath followPath { get; private set; }
		public Dash dash { get; private set; }
		public Parabola parabola { get; private set; }
		public Repelled replled { get; private set; }

		private Vec3 _steeringForce;

		private BehaviorType _btFlag;

		public SteeringBehaviors( Entity owner )
		{
			this.owner = owner;
			this.seek = new Seek( this );
			this.flee = new Flee( this );
			this.arrive = new Arrive( this );
			this.pursuit = new Pursuit( this );
			this.evade = new Evade( this );
			this.wander = new Wander( this );
			this.obstacleAvoidance = new ObstacleAvoidance( this );
			this.followPath = new FollowPath( this );
			this.dash = new Dash( this );
			this.parabola = new Parabola( this );
			this.replled = new Repelled( this );
		}

		public void On( BehaviorType type )
		{
			this._btFlag |= type;
		}

		public void Off( BehaviorType type )
		{
			this._btFlag &= ~type;
		}

		public bool IsOn( BehaviorType type )
		{
			return ( this._btFlag & type ) == type;
		}

		public void Update( UpdateContext context )
		{
			Vec3 steeringForce = this.Calculate();
			Vec3 acceleration = steeringForce / this.owner.mass;
			Vec3 velocity = this.owner.property.velocity + acceleration * context.deltaTime;
			this.owner.UpdateVelocity( velocity );
			this.owner.property.Equal( Attr.Position, this.owner.property.position + this.owner.property.velocity * context.deltaTime );
			if ( this.owner.property.speed > 0.00001f )
				this.owner.property.Equal( Attr.Direction, Vec3.Slerp( this.owner.property.direction, this.owner.property.velocity,
																	  context.deltaTime * this.owner.rotSpeed *
																	  this.owner.property.moveSpeedFactor ) );
			this.AfterUpdatePosition();
		}

		private Vec3 Calculate()
		{
			this._steeringForce.Set( 0, 0, 0 );

			if ( this.IsOn( BehaviorType.Repelled ) )
				this._steeringForce += this.replled.Steer();

			if ( this.IsOn( BehaviorType.Dash ) )
				this._steeringForce += this.dash.Steer();

			if ( this.IsOn( BehaviorType.Parabola ) )
				this._steeringForce += this.parabola.Steer();

			if ( this.IsOn( BehaviorType.Seek ) )
				this._steeringForce += this.seek.Steer();

			if ( this.IsOn( BehaviorType.Flee ) )
				this._steeringForce += this.flee.Steer();

			if ( this.IsOn( BehaviorType.Arrive ) )
				this._steeringForce += this.arrive.Steer();

			if ( this.IsOn( BehaviorType.Pursuit ) )
				this._steeringForce += this.pursuit.Steer();

			if ( this.IsOn( BehaviorType.Evade ) )
				this._steeringForce += this.evade.Steer();

			if ( this.IsOn( BehaviorType.Wander ) )
				this._steeringForce += this.wander.Steer();

			if ( this.IsOn( BehaviorType.FollowPath ) )
				this._steeringForce += this.followPath.Steer();

			if ( this.IsOn( BehaviorType.ObstacleAvoidance ) )
				this._steeringForce += this.obstacleAvoidance.Steer();

			return this._steeringForce;
		}

		private void AfterUpdatePosition()
		{
			if ( this.IsOn( BehaviorType.Repelled ) )
				this.replled.AfterUpdatePosition();

			if ( this.IsOn( BehaviorType.Dash ) )
				this.dash.AfterUpdatePosition();

			if ( this.IsOn( BehaviorType.Parabola ) )
				this.parabola.AfterUpdatePosition();

			if ( this.IsOn( BehaviorType.Seek ) )
				this.seek.AfterUpdatePosition();

			if ( this.IsOn( BehaviorType.Flee ) )
				this.flee.AfterUpdatePosition();

			if ( this.IsOn( BehaviorType.Arrive ) )
				this.arrive.AfterUpdatePosition();

			if ( this.IsOn( BehaviorType.Pursuit ) )
				this.pursuit.AfterUpdatePosition();

			if ( this.IsOn( BehaviorType.Evade ) )
				this.evade.AfterUpdatePosition();

			if ( this.IsOn( BehaviorType.Wander ) )
				this.wander.AfterUpdatePosition();

			if ( this.IsOn( BehaviorType.FollowPath ) )
				this.followPath.AfterUpdatePosition();

			if ( this.IsOn( BehaviorType.ObstacleAvoidance ) )
				this.obstacleAvoidance.AfterUpdatePosition();
		}

		public void MaxVelocity( Vec3 targetPos )
		{
			//开始就把速度提升到最快
			Entity self = this.owner;
			Vec3 dir = Vec3.Normalize( targetPos - this.owner.property.position );
			self.UpdateVelocity( dir * ( self.maxSpeed * self.property.moveSpeedFactor ) );
		}
	}
}