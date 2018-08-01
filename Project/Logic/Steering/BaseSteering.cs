using Core.Math;

namespace Logic.Steering
{
	public abstract class BaseSteering
	{
		protected readonly SteeringBehaviors _behaviors;

		protected BaseSteering( SteeringBehaviors behaviors )
		{
			this._behaviors = behaviors;
		}

		public virtual Vec3 Steer() { return Vec3.zero; }

		public virtual void AfterUpdatePosition()
		{
		}
	}
}