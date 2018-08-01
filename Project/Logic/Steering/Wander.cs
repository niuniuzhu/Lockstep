using Core.Math;
using Logic.Controller;

namespace Logic.Steering
{
	public class Wander : BaseSteering
	{
		private const float WANDER_RAD = 1.2f;
		private const float WANDER_DIST = 2.0f;
		private const float WANDER_JITTER_PER_SEC = 80.0f;

		private Vec3 _wanderTarget;

		public Wander( SteeringBehaviors behaviors ) : base( behaviors )
		{
		}

		public void Set( Vec3 wanderTarget )
		{
			this._wanderTarget = wanderTarget;
		}

		public override Vec3 Steer()
		{
			Entity self = this._behaviors.owner;
			//this behavior is dependent on the update rate, so this line must
			//be included when using time independent framerate.
			float jitterThisTimeSlice = WANDER_JITTER_PER_SEC * self.battle.deltaTime;

			//first, add a small random vector to the target's position
			this._wanderTarget += new Vec3( self.battle.random.Next( 0, 1 ) * jitterThisTimeSlice, 0,
										self.battle.random.Next( 0, 1 ) * jitterThisTimeSlice );

			//reproject this new vector back on to a unit circle
			this._wanderTarget.Normalize();

			//increase the length of the vector to the same as the radius
			//of the wander circle
			this._wanderTarget *= WANDER_RAD;

			//move the target into a position WanderDist in front of the agent
			Vec3 targetLocal = this._wanderTarget + new Vec3( WANDER_DIST, 0, WANDER_DIST );

			//project the target into world space
			Vec3 targetWorld = self.PointToWorld( targetLocal );

			//and steer towards it
			return targetWorld - self.property.position;
		}
	}
}