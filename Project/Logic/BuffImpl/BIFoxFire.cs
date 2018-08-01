using Core.Math;
using Logic.Controller;
using Logic.Model;

namespace Logic.BuffImpl
{
	public class BIFoxFire : BIBase
	{
		protected override void CreateSummons( int triggerIndex )
		{
			BuffData.Summon[][] summonss = this._buff.trigger.summons;

			int index = summonss.Length - 1;
			index = triggerIndex <= index ? triggerIndex : index;

			BuffData.Summon[] summons = summonss[index];

			Battle battle = this._buff.battle;
			int count = summons.Length;
			for ( int i = 0; i < count; i++ )
			{
				BuffData.Summon summon = summons[i];

				Vec3 buffPos = this._buff.property.position;

				float distance = battle.random.NextFloat( this._buff.radius - 0.5f, this._buff.radius + 0.5f );
				Quat q = Quat.Euler( 0f, battle.random.NextFloat( 0f, 360f ), 0f );
				Vec3 offset = q * Vec3.right * distance;
				offset.y = this._buff.caster.size.y * 0.5f;
				FoxFire foxFire = battle.CreateBio<FoxFire>( summon.id, buffPos + offset, this._buff.property.direction, this._buff.caster.property.team );
				foxFire.Setup( this._buff.property.lvl, this._buff.caster, distance );
			}
		}
	}
}