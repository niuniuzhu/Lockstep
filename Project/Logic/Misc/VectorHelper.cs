using Core.Math;
using org.critterai;

namespace Logic.Misc
{
	public static class VectorHelper
	{
		public static Vec2 ToVec2( this Vector2 v )
		{
			return new Vec2( v.x, v.y );
		}

		public static Vec3 ToVec3( this Vector3 v )
		{
			return new Vec3( v.x, v.y, v.z );
		}
		public static Vector2 ToVector2( this Vec2 v )
		{
			return new Vector2( v.x, v.y );
		}

		public static Vector3 ToVector3( this Vec3 v )
		{
			return new Vector3( v.x, v.y, v.z );
		}

		public static Vec3[] ToVec3Array( ref Vector3[] v, int count )
		{
			if ( v == null )
				return null;
			if ( count <= 0 )
				return new Vec3[0];
			Vec3[] o = new Vec3[count];
			unsafe
			{
				fixed ( Vector3* v0 = &v[0] )
				{
					Vector3* ps = v0;
					fixed ( Vec3* v1 = &o[0] )
					{
						Vec3* pt = v1;
						for ( int i = 0; i < count; i++ )
						{
							*pt = *( Vec3* )ps;
							++ps;
							++pt;
						}
					}
				}
			}
			return o;
		}

		public static Vector3[] ToVector3Array( ref Vec3[] v, int count )
		{
			if ( v == null )
				return null;
			if ( count <= 0 )
				return new Vector3[0];
			Vector3[] o = new Vector3[count];
			unsafe
			{
				fixed ( Vec3* v0 = &v[0] )
				{
					Vec3* ps = v0;
					fixed ( Vector3* v1 = &o[0] )
					{
						Vector3* pt = v1;
						for ( int i = 0; i < count; i++ )
						{
							*pt = *( Vector3* )ps;
							++ps;
							++pt;
						}
					}
				}
			}
			return o;
		}
	}
}