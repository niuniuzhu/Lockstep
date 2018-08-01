using Core.Math;
using UnityEngine;

namespace View.Misc
{
	public static class VectorHelper
	{
		public static void Clamp( ref Vec2 value, Vec2 min, Vec2 max )
		{
			value.x = MathUtils.Clamp( value.x, min.x, max.x );
			value.y = MathUtils.Clamp( value.y, min.y, max.y );
		}

		public static void Clamp( ref Vec3 value, Vec3 min, Vec3 max )
		{
			value.x = MathUtils.Clamp( value.x, min.x, max.x );
			value.y = MathUtils.Clamp( value.y, min.y, max.y );
			value.z = MathUtils.Clamp( value.z, min.z, max.z );
		}

		public static void Clamp( ref Vec4 value, Vec4 min, Vec4 max )
		{
			value.x = MathUtils.Clamp( value.x, min.x, max.x );
			value.y = MathUtils.Clamp( value.y, min.y, max.y );
			value.z = MathUtils.Clamp( value.z, min.z, max.z );
			value.w = MathUtils.Clamp( value.w, min.w, max.w );
		}

		public static void Clamp( ref Vector2 value, Vector2 min, Vector2 max )
		{
			value.x = MathUtils.Clamp( value.x, min.x, max.x );
			value.y = MathUtils.Clamp( value.y, min.y, max.y );
		}

		public static void Clamp( ref Vector3 value, Vector3 min, Vector3 max )
		{
			value.x = MathUtils.Clamp( value.x, min.x, max.x );
			value.y = MathUtils.Clamp( value.y, min.y, max.y );
			value.z = MathUtils.Clamp( value.z, min.z, max.z );
		}

		public static void Clamp( ref Vector4 value, Vector4 min, Vector4 max )
		{
			value.x = MathUtils.Clamp( value.x, min.x, max.x );
			value.y = MathUtils.Clamp( value.y, min.y, max.y );
			value.z = MathUtils.Clamp( value.z, min.z, max.z );
			value.w = MathUtils.Clamp( value.w, min.w, max.w );
		}

		public static Vec2 ToVec3( this Vector2 v )
		{
			return new Vec2( v.x, v.y );
		}

		public static Vec3 ToVec3( this Vector3 v )
		{
			return new Vec3( v.x, v.y, v.z );
		}

		public static Vec4 ToVec3( this Vector4 v )
		{
			return new Vec4( v.x, v.y, v.z, v.w );
		}

		public static Quat ToVec3( this Quaternion q )
		{
			return new Quat( q.x, q.y, q.z, q.w );
		}

		public static Vector2 ToVector3( this Vec2 v )
		{
			return new Vector2( v.x, v.y );
		}

		public static Vector3 ToVector3( this Vec3 v )
		{
			return new Vector3( v.x, v.y, v.z );
		}

		public static Vector4 ToVector3( this Vec4 v )
		{
			return new Vector4( v.x, v.y, v.z, v.w );
		}

		public static Quaternion ToVector3( this Quat q )
		{
			return new Quaternion( q.x, q.y, q.z, q.w );
		}

		public static Color ToColor( this Color4 color )
		{
			return new Color( color.r, color.g, color.b, color.a );
		}

		public static Color4 ToColor4( this Color color )
		{
			return new Color4( color.r, color.g, color.b, color.a );
		}

		public static Vec3[] ToVec3Array( Vector3[] v )
		{
			if ( v == null )
				return null;
			int count = v.Length;
			if ( count == 0 )
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

		public static Vector3[] ToVector3Array( Vec3[] v )
		{
			if ( v == null )
				return null;
			int count = v.Length;
			if ( count == 0 )
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