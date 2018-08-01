using Core.Math;

namespace Logic.Misc
{
	public static class BezierHelper
	{
		public static void GetParabolaControlPoints( Vec3 p0, Vec3 p1, Vec3 p2, ref Vec3[] ctlPoints )
		{
			Vec3 m0 = Vec3.Lerp( p0, p1, 0.5f );
			m0.y = p1.y;
			Vec3 m1 = Vec3.Lerp( p1, p2, 0.5f );
			m1.y = p1.y;
			ctlPoints[0] = p0;
			ctlPoints[1] = m0;
			ctlPoints[2] = m1;
			ctlPoints[3] = p2;
		}

		public static Vec3 GetPointAtTime( float t, Vec3[] controlP )
		{
			float u = 1.0f - t;
			float tt = t * t;
			float uu = u * u;
			float uuu = uu * u;
			float ttt = tt * t;
			Vec3 p = uuu * controlP[0]; //first term
			p += 3 * uu * t * controlP[1]; //second term
			p += 3 * u * tt * controlP[2]; //third term
			p += ttt * controlP[3]; //fourth term
			return p;
		}

		public static Vec3 GetPointAtTime( float t, Vec3 p0, Vec3 p1, Vec3 p2 )
		{
			float u = 1.0f - t;
			float tt = t * t;
			float uu = u * u;
			Vec3 p = uu * p0; //first term
			p += 2 * t * u * p1; //second term
			p += tt * p2; //third term
			return p;
		}
	}
}