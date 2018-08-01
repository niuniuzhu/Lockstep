using System;
using Core.Math;

namespace Logic.Property
{
	public class AttrDescAttribute : Attribute
	{
		public readonly Type type;
		public readonly string[] names;
		public readonly string[] defaultVals;
		public readonly string defaultVal;
		public readonly string field;

		public AttrDescAttribute( Type type, string[] names, string[] defaultVals )
		{
			this.type = type;
			this.names = names;
			this.defaultVals = defaultVals;
		}

		public AttrDescAttribute( Type type, string[] names, string defaultVal )
		{
			this.type = type;
			this.names = names;
			this.defaultVal = defaultVal;
		}

		public AttrDescAttribute( Type type, string[] names )
		{
			this.type = type;
			this.names = names;
			this.field = string.Empty;
		}

		public AttrDescAttribute( Type type, string[] names, string defaultVal, string field )
		{
			this.type = type;
			this.names = names;
			this.defaultVal = defaultVal;
			this.field = field;
		}
	}

	public enum Attr
	{
		[AttrDesc( typeof( int ), new[] { "EntityProperty", "SkillProperty", "BuffProperty" }, new[] { "0", "-1", "0" } )] Lvl = 0,
		[AttrDesc( typeof( Vec3 ), new[] { "EntityProperty", "BuffProperty" }, "Core.Math.Vec3.zero" )] Position = 1,
		[AttrDesc( typeof( Vec3 ), new[] { "EntityProperty", "BuffProperty" }, "Core.Math.Vec3.zero" )] Direction = 2,
		[AttrDesc( typeof( Vec3 ), new[] { "EntityProperty" }, "Core.Math.Vec3.zero" )] Velocity = 3,
		[AttrDesc( typeof( float ), new[] { "EntityProperty"}, "0f" )] Speed = 4,
		[AttrDesc( typeof( int ), new[] { "EntityProperty" }, new[] { "-1" } )] Team = 5,
		[AttrDesc( typeof( float ), new[] { "EntityProperty" }, new[] { "1f" } )] MoveSpeedFactor = 6,
		[AttrDesc( typeof( float ), new[] { "EntityProperty" }, new[] { "1f" } )] AttackSpeedFactor = 7,
		[AttrDesc( typeof( float ), new[] { "EntityProperty" }, new[] { "1f" } )] Scale = 8,
		[AttrDesc( typeof( float ), new[] { "EntityProperty" }, "0f" )] Hp = 101,
		[AttrDesc( typeof( float ), new[] { "EntityProperty" }, "0f" )] Mhp = 102,
		[AttrDesc( typeof( float ), new[] { "EntityProperty" }, "0f" )] HpRegen = 103,
		[AttrDesc( typeof( float ), new[] { "EntityProperty" }, "0f" )] Mana = 104,
		[AttrDesc( typeof( float ), new[] { "EntityProperty" }, "0f" )] Mmana = 105,
		[AttrDesc( typeof( float ), new[] { "EntityProperty" }, "0f" )] ManaRegen = 106,
		[AttrDesc( typeof( float ), new[] { "EntityProperty" }, "0f" )] Ad = 107,
		[AttrDesc( typeof( float ), new[] { "EntityProperty" }, "0f" )] Armor = 108,
		[AttrDesc( typeof( float ), new[] { "EntityProperty" }, "0f" )] ArmorPenFlat = 109,
		[AttrDesc( typeof( float ), new[] { "EntityProperty" }, "0f" )] ArmorPen = 110,
		[AttrDesc( typeof( float ), new[] { "EntityProperty" }, "0f" )] Ap = 111,
		[AttrDesc( typeof( float ), new[] { "EntityProperty" }, "0f" )] MagicResist = 112,
		[AttrDesc( typeof( float ), new[] { "EntityProperty" }, "0f" )] MagicPenFlat = 113,
		[AttrDesc( typeof( float ), new[] { "EntityProperty" }, "0f" )] MagicPen = 114,
		[AttrDesc( typeof( int ), new[] { "EntityProperty" }, "0" )] Gold = 121,
		[AttrDesc( typeof( int ), new[] { "EntityProperty" }, "0" )] Exp = 122,
		[AttrDesc( typeof( int ), new[] { "EntityProperty" }, "0" )] SkillPoint = 123,
		[AttrDesc( typeof( float ), new[] { "EntityProperty" }, "0f" )] InterruptTime = 130,
		[AttrDesc( typeof( int ), new[] { "EntityProperty" }, "0" )] Dashing = 150,
		[AttrDesc( typeof( int ), new[] { "EntityProperty" }, "0" )] Repelling = 151,
		[AttrDesc( typeof( int ), new[] { "EntityProperty" }, "0" )] FreezeFactor = 152,
		[AttrDesc( typeof( int ), new[] { "EntityProperty" }, "0" )] Stunned = 153,
		[AttrDesc( typeof( int ), new[] { "EntityProperty" }, "0" )] Stealth = 154,
		[AttrDesc( typeof( int ), new[] { "EntityProperty" }, "0" )] ImmuneDisables = 155,
		[AttrDesc( typeof( int ), new[] { "EntityProperty" }, "0" )] Charmed = 156,
		[AttrDesc( typeof( int ), new[] { "EntityProperty" }, "1" )] IgnoreVolumetric = 157,

		[AttrDesc( typeof( float ), new[] { "SkillProperty" }, "0f" )] Cooldown = 213,
		[AttrDesc( typeof( int ), new[] { "SkillProperty" }, "0" )] AllowUseWhenCooldown = 215,
	}
}