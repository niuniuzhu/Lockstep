using Logic.Model;

namespace Logic.Property
{
	public partial class EntityProperty : PropertyBase
	{
		private EntityData _data;

		public void Init( EntityData data )
		{
			this._data = data;
			this.SetDefault();
		}

		public void ApplyLevel( int level )
		{
			this.Equal( Attr.Lvl, level );
			EntityData.Level lvlDef = this._data.levels[level];
			this.Add( Attr.SkillPoint, lvlDef.upgradeSkillPointObtained );
			this.Add( Attr.Gold, lvlDef.upgradeGoldObtained );
			this.Add( Attr.Exp, -lvlDef.upgradeExpNeeded );
			this.Equal( Attr.Mhp, lvlDef.mhp );
			this.Equal( Attr.Hp, lvlDef.mhp );
			this.Equal( Attr.Mmana, lvlDef.mmana );
			this.Equal( Attr.Mana, lvlDef.mmana );
			this.Equal( Attr.HpRegen, lvlDef.hpRegen );
			this.Equal( Attr.ManaRegen, lvlDef.manaRegen );
			this.Equal( Attr.Ad, lvlDef.ad );
			this.Equal( Attr.Armor, lvlDef.armor );
			this.Equal( Attr.ArmorPenFlat, lvlDef.armorPenFlat );
			this.Equal( Attr.ArmorPen, lvlDef.armorPen );
			this.Equal( Attr.Ap, lvlDef.ap );
			this.Equal( Attr.MagicResist, lvlDef.magicResist );
			this.Equal( Attr.MagicPenFlat, lvlDef.magicPenFlat );
			this.Equal( Attr.MagicPen, lvlDef.magicPen );
		}
	}
}