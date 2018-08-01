using Core.Math;
using Logic.Controller;

namespace Logic.Steering
{
	public class Evade : BaseSteering
	{
		private Entity _pursuer;

		public Evade( SteeringBehaviors behaviors ) : base( behaviors )
		{
		}

		public void Set( Entity pursuer )
		{
			this._pursuer = pursuer;
		}

		public override Vec3 Steer()
		{
			Entity self = this._behaviors.owner;

			/* Not necessary to include the check for facing direction this time */

			Vec3 toPursuer = this._pursuer.property.position - self.property.position;

			//uncomment the following two lines to have Evade only consider pursuers 
			//within a 'threat range'
			const float threatRange = 100.0f;
			if ( toPursuer.SqrMagnitude() > threatRange * threatRange ) return Vec3.zero;

			//the lookahead time is propotional to the distance between the pursuer
			//and the pursuer; and is inversely proportional to the sum of the
			//agents' velocities
			float lookAheadTime = toPursuer.Magnitude() / ( ( self.maxSpeed + this._pursuer.maxSpeed ) * self.property.moveSpeedFactor );

			//now flee away from predicted future position of the pursuer
			this._behaviors.flee.Set( this._pursuer.property.position + this._pursuer.property.velocity * lookAheadTime );
			return this._behaviors.flee.Steer();
		}
	}
}