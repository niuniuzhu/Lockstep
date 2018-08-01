namespace Logic.Property
{
	public partial class EntityProperty
	{
		public System.Int32 lvl => ( System.Int32 )this._attrMap[Attr.Lvl];
		public Core.Math.Vec3 position => ( Core.Math.Vec3 )this._attrMap[Attr.Position];
		public Core.Math.Vec3 direction => ( Core.Math.Vec3 )this._attrMap[Attr.Direction];
		public Core.Math.Vec3 velocity => ( Core.Math.Vec3 )this._attrMap[Attr.Velocity];
		public System.Single speed => ( System.Single )this._attrMap[Attr.Speed];
		public System.Int32 team => ( System.Int32 )this._attrMap[Attr.Team];
		public System.Single moveSpeedFactor => ( System.Single )this._attrMap[Attr.MoveSpeedFactor];
		public System.Single attackSpeedFactor => ( System.Single )this._attrMap[Attr.AttackSpeedFactor];
		public System.Single scale => ( System.Single )this._attrMap[Attr.Scale];
		public System.Single hp => ( System.Single )this._attrMap[Attr.Hp];
		public System.Single mhp => ( System.Single )this._attrMap[Attr.Mhp];
		public System.Single hpRegen => ( System.Single )this._attrMap[Attr.HpRegen];
		public System.Single mana => ( System.Single )this._attrMap[Attr.Mana];
		public System.Single mmana => ( System.Single )this._attrMap[Attr.Mmana];
		public System.Single manaRegen => ( System.Single )this._attrMap[Attr.ManaRegen];
		public System.Single ad => ( System.Single )this._attrMap[Attr.Ad];
		public System.Single armor => ( System.Single )this._attrMap[Attr.Armor];
		public System.Single armorPenFlat => ( System.Single )this._attrMap[Attr.ArmorPenFlat];
		public System.Single armorPen => ( System.Single )this._attrMap[Attr.ArmorPen];
		public System.Single ap => ( System.Single )this._attrMap[Attr.Ap];
		public System.Single magicResist => ( System.Single )this._attrMap[Attr.MagicResist];
		public System.Single magicPenFlat => ( System.Single )this._attrMap[Attr.MagicPenFlat];
		public System.Single magicPen => ( System.Single )this._attrMap[Attr.MagicPen];
		public System.Int32 gold => ( System.Int32 )this._attrMap[Attr.Gold];
		public System.Int32 exp => ( System.Int32 )this._attrMap[Attr.Exp];
		public System.Int32 skillPoint => ( System.Int32 )this._attrMap[Attr.SkillPoint];
		public System.Single interruptTime => ( System.Single )this._attrMap[Attr.InterruptTime];
		public System.Int32 dashing => ( System.Int32 )this._attrMap[Attr.Dashing];
		public System.Int32 repelling => ( System.Int32 )this._attrMap[Attr.Repelling];
		public System.Int32 freezeFactor => ( System.Int32 )this._attrMap[Attr.FreezeFactor];
		public System.Int32 stunned => ( System.Int32 )this._attrMap[Attr.Stunned];
		public System.Int32 stealth => ( System.Int32 )this._attrMap[Attr.Stealth];
		public System.Int32 immuneDisables => ( System.Int32 )this._attrMap[Attr.ImmuneDisables];
		public System.Int32 charmed => ( System.Int32 )this._attrMap[Attr.Charmed];
		public System.Int32 ignoreVolumetric => ( System.Int32 )this._attrMap[Attr.IgnoreVolumetric];

		public void SetDefault()
		{
			this._attrMap[Attr.Lvl] = 0;
			this._attrMap[Attr.Position] = Core.Math.Vec3.zero;
			this._attrMap[Attr.Direction] = Core.Math.Vec3.zero;
			this._attrMap[Attr.Velocity] = Core.Math.Vec3.zero;
			this._attrMap[Attr.Speed] = 0f;
			this._attrMap[Attr.Team] = -1;
			this._attrMap[Attr.MoveSpeedFactor] = 1f;
			this._attrMap[Attr.AttackSpeedFactor] = 1f;
			this._attrMap[Attr.Scale] = 1f;
			this._attrMap[Attr.Hp] = 0f;
			this._attrMap[Attr.Mhp] = 0f;
			this._attrMap[Attr.HpRegen] = 0f;
			this._attrMap[Attr.Mana] = 0f;
			this._attrMap[Attr.Mmana] = 0f;
			this._attrMap[Attr.ManaRegen] = 0f;
			this._attrMap[Attr.Ad] = 0f;
			this._attrMap[Attr.Armor] = 0f;
			this._attrMap[Attr.ArmorPenFlat] = 0f;
			this._attrMap[Attr.ArmorPen] = 0f;
			this._attrMap[Attr.Ap] = 0f;
			this._attrMap[Attr.MagicResist] = 0f;
			this._attrMap[Attr.MagicPenFlat] = 0f;
			this._attrMap[Attr.MagicPen] = 0f;
			this._attrMap[Attr.Gold] = 0;
			this._attrMap[Attr.Exp] = 0;
			this._attrMap[Attr.SkillPoint] = 0;
			this._attrMap[Attr.InterruptTime] = 0f;
			this._attrMap[Attr.Dashing] = 0;
			this._attrMap[Attr.Repelling] = 0;
			this._attrMap[Attr.FreezeFactor] = 0;
			this._attrMap[Attr.Stunned] = 0;
			this._attrMap[Attr.Stealth] = 0;
			this._attrMap[Attr.ImmuneDisables] = 0;
			this._attrMap[Attr.Charmed] = 0;
			this._attrMap[Attr.IgnoreVolumetric] = 1;
		}
	}
	public partial class SkillProperty
	{
		public System.Int32 lvl => ( System.Int32 )this._attrMap[Attr.Lvl];
		public System.Single cooldown => ( System.Single )this._attrMap[Attr.Cooldown];
		public System.Int32 allowUseWhenCooldown => ( System.Int32 )this._attrMap[Attr.AllowUseWhenCooldown];

		public void SetDefault()
		{
			this._attrMap[Attr.Lvl] = -1;
			this._attrMap[Attr.Cooldown] = 0f;
			this._attrMap[Attr.AllowUseWhenCooldown] = 0;
		}
	}
	public partial class BuffProperty
	{
		public System.Int32 lvl => ( System.Int32 )this._attrMap[Attr.Lvl];
		public Core.Math.Vec3 position => ( Core.Math.Vec3 )this._attrMap[Attr.Position];
		public Core.Math.Vec3 direction => ( Core.Math.Vec3 )this._attrMap[Attr.Direction];

		public void SetDefault()
		{
			this._attrMap[Attr.Lvl] = 0;
			this._attrMap[Attr.Position] = Core.Math.Vec3.zero;
			this._attrMap[Attr.Direction] = Core.Math.Vec3.zero;
		}
	}

}
