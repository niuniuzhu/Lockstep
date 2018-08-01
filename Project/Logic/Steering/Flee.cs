using Core.Math;
using Logic.Controller;

namespace Logic.Steering
{
	public class Flee : BaseSteering
	{
		private Vec3 _targetPoint;

		public Flee( SteeringBehaviors behaviors ) : base( behaviors )
		{
		}

		public void Set( Vec3 targetPos )
		{
			this._targetPoint = targetPos;
		}

		public void MaxVelocity()
		{
			//开始就把速度提升到最快
			Entity self = this._behaviors.owner;
			Vec3 dir = Vec3.Normalize( self.property.position - this._targetPoint );
			self.UpdateVelocity( dir * ( self.maxSpeed * self.property.moveSpeedFactor ) );
		}

		public override Vec3 Steer()
		{
			Entity self = this._behaviors.owner;
			Vec3 desiredVelocity = Vec3.Normalize( self.property.position - this._targetPoint ) * self.maxSpeed * self.property.moveSpeedFactor;
			return desiredVelocity - self.property.velocity;
		}
	}
}