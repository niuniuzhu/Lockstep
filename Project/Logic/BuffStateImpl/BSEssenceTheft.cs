using System.Collections.Generic;
using Logic.Controller;
using Logic.Property;

namespace Logic.BuffStateImpl
{
	//参数1-每个技能的最大充能数,2-达到指定充能总数触发回血,3-回血百分比
	public class BSEssenceTheft : BSBase
	{
		private readonly Dictionary<string, int> _chargesPreSkill = new Dictionary<string, int>();
		private int _totalCharges;

		protected override void Reset()
		{
			this._totalCharges = 0;
			this._chargesPreSkill.Clear();
		}

		protected override void DamageInternal( Buff buff, Bio target, float damage )
		{
			if ( buff.skillData.isCommon )
				return;

			int charge;
			if ( !this._chargesPreSkill.TryGetValue( buff.skillData.id, out charge ) )
			{
				this._chargesPreSkill[buff.skillData.id] = 0;
				charge = 0;
			}
			int maxChargePreSkill = ( int ) this.extra[0];
			if ( charge < maxChargePreSkill )
			{
				++this._totalCharges;
				++charge;
				this._chargesPreSkill[buff.skillData.id] = charge > maxChargePreSkill ? maxChargePreSkill : charge;
			}

			if ( this._totalCharges > ( int ) this.extra[1] )
			{
				this.owner.property.Add( Attr.Hp, damage * this.extra[2] );
				this._totalCharges = 0;
				this._chargesPreSkill.Clear();
			}
		}
	}
}