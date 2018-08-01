using Core.Math;
using Logic.Controller;

namespace Logic.Steering
{
	public class Seek : BaseSteering
	{
		private Vec3 _targetPoint;

		public bool complete { get; private set; }

		public Seek( SteeringBehaviors behaviors ) : base( behaviors )
		{
		}

		public void Set( Vec3 targetPos )
		{
			this._targetPoint = targetPos;
			this.complete = false;
		}

		public void MaxVelocity()
		{
			//开始就把速度提升到最快
			Entity self = this._behaviors.owner;
			Vec3 dir = Vec3.Normalize( this._targetPoint - this._behaviors.owner.property.position );
			self.UpdateVelocity( dir * ( self.maxSpeed * self.property.moveSpeedFactor ) );
		}

		public override Vec3 Steer()
		{
			if ( this.complete )
				return Vec3.zero;

			Entity self = this._behaviors.owner;
			Vec3 dir = Vec3.Normalize( this._targetPoint - this._behaviors.owner.property.position );
			Vec3 desiredVelocity = dir * ( self.maxSpeed * self.property.moveSpeedFactor );
			return desiredVelocity - self.property.velocity;
		}

		public override void AfterUpdatePosition()
		{
			Entity self = this._behaviors.owner;
			if ( SteeringTools.CheckPointCrossTargetPoint( self.property.position, this._targetPoint ) )
			{
				self.UpdateVelocity( Vec3.zero );
				this.complete = true;
			}
		}
	}
}