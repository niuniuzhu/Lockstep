using Core.Math;

namespace Logic.Misc
{
	public class ConsistentRandom : System.Random
	{
		public Vec2 onUnitCircle
		{
			get
			{
				float angle = this.NextFloat( 0f, 2 * MathUtils.PI );
				return new Vec2( MathUtils.Cos( angle ), MathUtils.Sin( angle ) );
			}
		}

		public Vec2 insideUnitCircle
		{
			get
			{
				float radius = this.NextFloat();
				float angle = this.NextFloat( 0f, 2 * MathUtils.PI );
				return new Vec2( radius * MathUtils.Cos( angle ), radius * MathUtils.Sin( angle ) );
			}
		}

		public Vec3 onUnitSphere
		{
			get
			{
				float theta = this.NextFloat( 0f, 2 * MathUtils.PI );
				float phi = MathUtils.Acos( 2f * this.NextFloat() - 1f );
				return new Vec3( ( MathUtils.Cos( theta ) * MathUtils.Sin( phi ) ), ( MathUtils.Sin( theta ) * MathUtils.Sin( phi ) ), ( MathUtils.Cos( phi ) ) );
			}
		}

		public Vec3 insideUnitSphere
		{
			get
			{
				float theta = this.NextFloat( 0f, 2 * MathUtils.PI );
				float phi = MathUtils.Acos( 2f * this.NextFloat() - 1f );
				float r = MathUtils.Pow( this.NextFloat(), 1f / 3f );
				return new Vec3( ( r * MathUtils.Cos( theta ) * MathUtils.Sin( phi ) ), ( r * MathUtils.Sin( theta ) * MathUtils.Sin( phi ) ), ( r * MathUtils.Cos( phi ) ) );
			}
		}

		public Quat rotation
		{
			get
			{
				float theta = this.NextFloat( 0f, 2 * MathUtils.PI );
				float phi = this.NextFloat( -MathUtils.PI * 05f, MathUtils.PI * 0.5f );
				Vec3 v = new Vec3( MathUtils.Sin( phi ) * MathUtils.Sin( theta ), MathUtils.Cos( phi ) * MathUtils.Sin( theta ), MathUtils.Cos( theta ) );
				return Quat.FromToRotation( Vec3.forward, v );
			}
		}

		public Quat rotationUniform => Quat.FromToRotation( Vec3.forward, this.onUnitSphere );

		private int _inc;

		public ConsistentRandom( int seed )
			: base( seed )
		{
		}

		public float NextFloat( float min, float max )
		{
			return this.NextFloat() * ( max - min ) + min;
		}

		public float NextFloat()
		{
			return ( float )this.NextDouble();
		}

		public Color4 ColorHSV( float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax, float alphaMin, float alphaMax )
		{
			float h = MathUtils.Lerp( hueMin, hueMax, this.NextFloat() );
			float s = MathUtils.Lerp( saturationMin, saturationMax, this.NextFloat() );
			float v = MathUtils.Lerp( valueMin, valueMax, this.NextFloat() );
			Color4 result = Color4.HsvtoRgb( h, s, v, true );
			result.a = MathUtils.Lerp( alphaMin, alphaMax, this.NextFloat() );
			return result;
		}

		public string IdHash( string id )
		{
			long hash = ( long )this._inc << 32 | ( long )this.Next();
			this._inc = this._inc >= int.MaxValue ? 0 : this._inc + 1;
			return id + "@" + hash;
		}
	}
}