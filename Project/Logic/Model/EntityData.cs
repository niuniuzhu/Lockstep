using System.Collections;
using Core.Math;
using Core.Misc;

namespace Logic.Model
{
	public class EntityData
	{
		public class Level
		{
			public float mhp;
			public float mmana;
			public float hpRegen;
			public float manaRegen;
			public float ad;
			public float armor;
			public float armorPenFlat;
			public float armorPen;
			public float ap;
			public float magicResist;
			public float magicPenFlat;
			public float magicPen;
			public int reliveTime;
			public int reliveGold;
			public int goldBountyAwarded;
			public int expBountyAwarded;
			public int upgradeExpNeeded;
			public int upgradeSkillPointObtained;
			public int upgradeGoldObtained;
		}

		public string id;
		public string name;
		public string model;
		public bool noShadow;
		public EntityFlag flag;
		public Vec3 size;
		public float mass;
		public bool volumetric;
		public float fov;
		public float speed;
		public float rotSpeed;
		public float scale;
		public float trackDistance;
		public int goldBase;
		public int skillPointBase;
		public Vec3? firingPoint;
		public Vec3? hitPoint;
		public bool destructImmediately;
		public float lifeTime;

		//bio
		public Level[] levels;
		public string[] skills;

		public AIData[] aiDatas;
		public string script;

		//missile
		public FlightType flightType;
		public float duration;
		public float arc;
		public string hitFx;

		//effect
		public string shaderName;
		public bool shadowVisible;
		public EffectPositionType positionType;
		public EffectRotationType rotationType;
		public Spare spare;

		internal void LoadFromDef( string id )
		{
			this.id = id;
			Hashtable def = Defs.GetEntity( this.id );
			string reference = def.GetString( "ref" );
			while ( !string.IsNullOrEmpty( reference ) )
			{
				Hashtable refdef = Defs.GetEntity( reference );
				reference = refdef.GetString( "ref" );
				def.Concat( refdef );
			}
			this.name = def.GetString( "name" );
			this.model = def.GetString( "model" );
			this.noShadow = def.GetBoolean( "no_shadow" );
			this.flag = ( EntityFlag ) def.GetInt( "flag" );
			this.size = def.GetVec3( "size" );
			this.mass = def.GetFloat( "mass" );
			this.volumetric = def.GetBoolean( "volumetric" );
			this.fov = def.GetFloat( "fov" );
			this.speed = def.GetFloat( "speed" );
			this.rotSpeed = def.GetFloat( "rot_speed" );
			this.scale = def.GetFloat( "scale" );
			if ( this.scale <= 0 )
				this.scale = 1;
			this.trackDistance = def.GetFloat( "track_distance" );
			this.goldBase = def.GetInt( "gold_base" );
			this.skillPointBase = def.GetInt( "skill_point_base" );
			if ( def.ContainsKey( "firing_point" ) )
				this.firingPoint = def.GetVec3( "firing_point" );
			if ( def.ContainsKey( "hit_point" ) )
				this.hitPoint = def.GetVec3( "hit_point" );
			this.destructImmediately = def.GetBoolean( "destruct_immediately" );
			this.lifeTime = def.GetFloat( "life_time" );

			this.skills = def.GetStringArray( "skills" );

			ArrayList lvls = def.GetList( "level" );
			if ( lvls != null )
			{
				int count = lvls.Count;
				this.levels = new Level[count];
				for ( int i = 0; i < count; i++ )
				{
					Level lvl = this.levels[i] = new Level();
					Hashtable ldef = ( Hashtable ) lvls[i];
					lvl.mhp = ldef.GetFloat( "hp" );
					lvl.mmana = ldef.GetFloat( "mana" );
					lvl.hpRegen = ldef.GetFloat( "hp_regen" );
					lvl.manaRegen = ldef.GetFloat( "mana_regen" );
					lvl.ad = ldef.GetFloat( "ad" );
					lvl.armor = ldef.GetFloat( "armor" );
					lvl.armorPenFlat = ldef.GetFloat( "armor_pen_flat" );
					lvl.armorPen = ldef.GetFloat( "armor_pen" );
					lvl.ap = ldef.GetFloat( "ap" );
					lvl.magicResist = ldef.GetFloat( "magic_resist" );
					lvl.magicPenFlat = ldef.GetFloat( "magic_pen_flat" );
					lvl.magicPen = ldef.GetFloat( "magic_pen" );
					lvl.reliveTime = ldef.GetInt( "relive_time" );
					lvl.reliveGold = ldef.GetInt( "relive_gold" );
					lvl.goldBountyAwarded = ldef.GetInt( "gold_bounty_awarded" );
					lvl.expBountyAwarded = ldef.GetInt( "exp_bounty_awarded" );
					lvl.upgradeExpNeeded = ldef.GetInt( "upgrade_exp_needed" );
					lvl.upgradeSkillPointObtained = ldef.GetInt( "upgrade_skill_point_obtained" );
					lvl.upgradeGoldObtained = ldef.GetInt( "upgrade_gold_obtained" );
				}
			}

			this.aiDatas = AIData.Parse( def.GetList( "ai" ) );
			this.script = def.GetString( "script" );

			//missile
			this.flightType = ( FlightType ) def.GetInt( "flight_type" );
			this.duration = def.GetFloat( "duration" );
			this.arc = def.GetFloat( "arc" );
			this.hitFx = def.GetString( "hit_fx" );

			//effect
			this.shaderName = def.GetString( "shader_name" );
			this.shadowVisible = def.GetBoolean( "shadow_visible" );
			this.positionType = ( EffectPositionType ) def.GetInt( "position_type" );
			this.rotationType = ( EffectRotationType ) def.GetInt( "rotation_type" );
			this.spare = ( Spare ) def.GetInt( "spare" );
		}
	}
}