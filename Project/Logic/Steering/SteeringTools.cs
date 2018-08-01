using Core.Math;
using Logic.Controller;

namespace Logic.Steering
{
	public static class SteeringTools
	{
		public static bool CheckPointCrossTargetPoint( Vec3 currentPoint, Vec3 targetPoint )
		{
			if ( currentPoint == targetPoint )
				return true;
			return ( currentPoint - targetPoint ).SqrMagnitude() < 0.1f;
		}

		public static bool ReachPoint( Entity vehicle, Vec3 point )
		{
			float r = MathUtils.Max( vehicle.size.x, vehicle.size.z );
			return ( vehicle.property.position - point ).SqrMagnitude() <= r * r;
		}

		public static bool ReachTarget( Entity vehicle, Entity target )
		{
			float r = MathUtils.Max( vehicle.size.x, vehicle.size.z ) + MathUtils.Max( target.size.x, target.size.z );
			return ( vehicle.property.position - target.property.position ).SqrMagnitude() <= r * r;
		}
	}
}