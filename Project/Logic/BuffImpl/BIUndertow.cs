using Core.Math;
using Logic.Misc;
using Logic.Property;

namespace Logic.BuffImpl
{
	//参数,0-时间因子,1-速度因子,往后作为速度曲线的参数
	public class BIUndertow : BIBase
	{
		private AnimationCurve _curve;
		private float _time;

		protected override void Reset()
		{
			this._curve = null;
		}

		protected override void CreateInternal()
		{
			float[] param = this._buff.extra;
			this._curve = new AnimationCurve();
			for ( int i = 2; i < param.Length; i += 4 )
				this._curve.AddKey( new Keyframe( param[i + 0], param[i + 1], param[i + 2], param[i + 3] ) );

			this._buff.property.Equal( Attr.Direction,
									   Vec3.Normalize( this._buff.targetPoint - this._buff.caster.property.position ) );

			this._time = 0f;
		}

		protected override void DoUpdateOrbit( float dt )
		{
			this._time += dt;
			float[] param = this._buff.extra;
			float timeFactor = param[0];
			float speedFactor = param[1];
			float speed = this._curve.Evaluate( this._time / timeFactor );
			Vec3 pos = this._buff.property.position;
			pos += this._buff.property.direction * speed * speedFactor * dt;
			this._buff.property.Equal( Attr.Position, pos );
		}
	}
}