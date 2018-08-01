using Core.Math;
using Logic.Controller;
using Logic.Misc;
using Logic.Model;
using Logic.Property;

namespace Logic.BuffImpl
{
	public class BIOrbOfDeception : BIBase
	{
		private AnimationCurve _curve;
		private bool _reverted;
		private float _time;

		protected override void Reset()
		{
			this._reverted = false;
			this._curve = null;
		}

		protected override void CreateInternal()
		{
			float[] param = this._buff.extra;
			this._curve = new AnimationCurve();
			for ( int i = 2; i < param.Length; i += 4 )
				this._curve.AddKey( new Keyframe( param[i + 0], param[i + 1], param[i + 2], param[i + 3] ) );

			this._buff.property.Equal( Attr.Direction, Vec3.Normalize( this._buff.targetPoint - this._buff.caster.property.position ) );

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
			if ( !this._reverted &&
				 speed < 0 )
			{
				this._triggerCountMap.Clear();
				this._reverted = true;
			}
		}

		protected override float CalcDamageInternal( Bio caster, Bio target, int index )
		{
			BuffData.Trigger trigger = this._buff.trigger;
			float cad = caster.property.ad;
			float cap = caster.property.ap;

			if ( !this._reverted )
			{
				float sad = trigger.ad?[index] ?? 0f;
				float sap = trigger.ap?[index] ?? 0f;
				float padp = trigger.padp?[index] ?? 0f;
				float papp = trigger.papp?[index] ?? 0f;

				float armorResist = MathUtils.Max( 0, target.property.armor * ( 1 - caster.property.armorPen ) - caster.property.armorPenFlat );
				float tad = sad + cad * padp;
				float pdamage = tad * ( 100 / ( 100 + armorResist ) );

				float magicResist = MathUtils.Max( 0, target.property.magicResist * ( 1 - caster.property.magicPen ) - caster.property.magicPenFlat );
				float tap = sap + cap * papp;
				float mdamage = tap * ( 100 / ( 100 + magicResist ) );

				return pdamage + mdamage;
			}
			float std = trigger.td?[index] ?? 0f;
			float tpadp = trigger.tpadp?[index] ?? 0f;
			float tpapp = trigger.tpapp?[index] ?? 0f;
			return std + cad * tpadp + cap * tpapp;
		}
	}
}