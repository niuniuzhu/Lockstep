using Core.Math;
using Logic.Controller;

namespace Logic.Steering
{
	public class Repelled : BaseSteering
	{
		private Vec3 _targetPoint;
		private float _speed;

		public bool complete { get; private set; }

		public Repelled( SteeringBehaviors behaviors ) : base( behaviors )
		{
		}

		public void Set( Vec3 targetPoint, float speed )
		{
			this._targetPoint = targetPoint;
			this._speed = speed;
			this.complete = false;
		}

		public override Vec3 Steer()
		{
			Entity self = this._behaviors.owner;
			self.ignoreSpeedLimits = true;
			Vec3 toTarget = this._targetPoint - self.property.position;
			float dist = toTarget.Magnitude();
			if ( dist > 0.2f )
			{
				Vec3 desiredVelocity = toTarget * this._speed / dist;
				return desiredVelocity - self.property.velocity;
			}
			this.complete = true;
			return Vec3.zero;
		}
	}
}