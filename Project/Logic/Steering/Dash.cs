using Core.Math;
using Logic.Controller;
using Logic.Misc;
using Logic.Property;

namespace Logic.Steering
{
	public class Dash : BaseSteering
	{
		private AnimationCurve _curve;
		private float _duration;

		private float _time;
		private Vec3 _dir;

		public bool complete { get; private set; }

		public Dash( SteeringBehaviors behaviors ) : base( behaviors )
		{
		}

		public void Set( Vec3 targetPoint, float startSpeed, AnimationCurve curve, float duration )
		{
			this._curve = curve;
			this._duration = duration;

			Entity self = this._behaviors.owner;
			this._dir = targetPoint == self.property.position
				            ? self.property.direction
				            : Vec3.Normalize( targetPoint - self.property.position );
			this._time = 0f;

			self.property.Equal( Attr.Direction, this._dir );
			self.ignoreSpeedLimits = true;
			self.UpdateVelocity( this._dir * startSpeed );

			this.complete = false;
		}

		public override Vec3 Steer()
		{
			Entity self = this._behaviors.owner;
			float dt = self.battle.deltaTime;
			this._time += dt;
			float t = this._time / this._duration;
			if ( t >= 1f )
			{
				this.complete = true;
				return Vec3.zero;
			}
			float f = this._curve == null ? 1 : this._curve.Evaluate( t );
			self.ignoreSpeedLimits = true;
			return this._dir * f;
		}
	}
}