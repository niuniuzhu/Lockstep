using Core.Math;
using Logic.Controller;

namespace Logic.Steering
{
	public class Arrive : BaseSteering
	{
		public enum Deceleration
		{
			Fast = 1,
			Normal,
			Slow
		}

		private Vec3 _targetPos;
		private Deceleration _deceleration;

		public bool complete { get; private set; }

		public Arrive( SteeringBehaviors behaviors ) : base( behaviors )
		{
		}

		public void Set( Vec3 targetPos, Deceleration deceleration )
		{
			this._targetPos = targetPos;
			this._deceleration = deceleration;
			this.complete = false;
		}

		public void MaxVelocity()
		{
			Entity self = this._behaviors.owner;
			self.UpdateVelocity( Vec3.Normalize( this._targetPos - self.property.position ) * ( self.maxSpeed * self.property.moveSpeedFactor ) );
		}

		public override Vec3 Steer()
		{
			Entity self = this._behaviors.owner;

			Vec3 toTarget = this._targetPos - self.property.position;
			float dist = toTarget.Magnitude();
			if ( dist > 0.1f )
			{
				const float decelerationTweaker = 0.01f;
				float speed = dist / ( ( float ) this._deceleration * decelerationTweaker );
				speed = MathUtils.Min( speed, self.maxSpeed * self.property.moveSpeedFactor );
				Vec3 desiredVelocity = toTarget * speed / dist;
				return desiredVelocity - self.property.velocity;
			}
			this.complete = true;
			return Vec3.zero;
		}
	}
}