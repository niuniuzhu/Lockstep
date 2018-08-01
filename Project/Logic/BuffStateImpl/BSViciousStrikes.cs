using Logic.Controller;
using Logic.Property;

namespace Logic.BuffStateImpl
{
	//残暴打击
	//参数1-生命偷取百分比
	public class BSViciousStrikes : BSBase
	{
		protected override void DamageInternal( Buff buff, Bio target, float damage )
		{
			this.owner.property.Add( Attr.Hp, damage * this.extra[0] );
		}
	}
}