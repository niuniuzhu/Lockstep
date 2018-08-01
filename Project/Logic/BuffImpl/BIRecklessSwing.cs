using Logic.Controller;
using Logic.Property;

namespace Logic.BuffImpl
{
	//鲁莽挥击
	//参数1-每次伤害对自己反噬的百分比
	public class BIRecklessSwing : BIBase
	{
		protected override void OnDamage( float damage, Bio caster, Bio target, int triggerIndex )
		{
			if ( target.property.hp <= 0 &&
				 target.sensorySystem.killer == caster )
			{
				//如果此技能将目标击杀，则会返还所有施法消耗
			}
			else
				caster.property.Add( Attr.Hp, -damage * this._buff.trigger.extras[triggerIndex][0] );
		}
	}
}