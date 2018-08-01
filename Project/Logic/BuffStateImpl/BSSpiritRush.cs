using Logic.Controller;
using Logic.Property;

namespace Logic.BuffStateImpl
{
	//参数extra 在状态内可以连续施放最多{0}次,然后进入冷却,冷却时间{1}秒
	public class BSSpiritRush : BSBase
	{
		private int _count;

		protected override void CreateInternal()
		{
			++this._count;
			Skill skill = this.owner.GetSkill( this.skillData.id );
			skill.property.Add( Attr.AllowUseWhenCooldown, 1 );
		}

		protected override void UniteInternal()
		{
			++this._count;

			if ( this._count == ( int ) this.extra[0] )
				this.owner.DestroyBuffState( this );
		}

		protected override void Reset()
		{
			this._count = 0;
		}

		protected override void DestroyInternal()
		{
			Skill skill = this.owner.GetSkill( this.skillData.id );
			skill.property.Add( Attr.AllowUseWhenCooldown, -1 );
		}
	}
}