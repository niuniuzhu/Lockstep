using Core.Math;
using Logic.Controller;
using Logic.Property;

namespace Logic.Steering
{
	public class Pursuit : BaseSteering
	{
		private Entity _evader;
		private Vec3 _offset;

		public bool complete { get; private set; }

		public Pursuit( SteeringBehaviors behaviors ) : base( behaviors )
		{
		}

		public void Set( Entity evader, Vec3 offset )
		{
			this._evader = evader;
			this._offset = offset;
			this.complete = false;
		}

		public void MaxVelocity()
		{
			//一开始就把速度提升到最快
			Entity self = this._behaviors.owner;
			Vec3 targetPoint = this._evader.PointToWorld( this._offset );
			self.UpdateVelocity( Vec3.Normalize( targetPoint - self.property.position ) * ( self.maxSpeed * self.property.moveSpeedFactor ) );
		}

		public override Vec3 Steer()
		{
			if ( this.complete )
				return Vec3.zero;

			Vec3 targetPoint = this._evader.PointToWorld( this._offset );

			//Vec3 toEvader = targetPoint - self.position;

			//float relativeHeading = Vec3.Dot( self.direction, this._evader.direction );

			//if ( Vec3.Dot( toEvader, self.direction ) > 0 && relativeHeading < -0.95f )  //acos(0.95)=18 degs
			//	return this.Seek( targetPoint );

			//float lookAheadTime = toEvader.magnitude / ( self.maxSpeed * self.property.moveSpeedFactor + this._evader.maxSpeed * this._evader.property.moveSpeedFactor );

			//return this.Seek( targetPoint + this._evader.property.velocity * lookAheadTime );
			return this.Seek( targetPoint );
		}

		public override void AfterUpdatePosition()
		{
			Entity self = this._behaviors.owner;
			if ( SteeringTools.ReachTarget( self, this._evader ) )
			{
				Vec3 targetPoint = this._evader.PointToWorld( this._offset );
				self.property.Equal( Attr.Position, targetPoint );
				self.UpdateVelocity( Vec3.zero );
				this.complete = true;
			}
		}

		private Vec3 Seek( Vec3 targetPoint )
		{
			Entity self = this._behaviors.owner;
			Vec3 dir = Vec3.Normalize( targetPoint - this._behaviors.owner.property.position );
			Vec3 desiredVelocity = dir * ( self.maxSpeed * self.property.moveSpeedFactor );
			return desiredVelocity - self.property.velocity;
		}
	}
}