using System.Collections;
using Core.Misc;
using Logic.Misc;

namespace Logic.Model
{
	public class SkillData
	{
		public class Level
		{
			public float atkTime;
			public float sufTime;
			public string action;
			public float actionLength;
			public float dashStartSpeed;
			public AnimationCurve dashSpeedCurve;
			public float distance;
			public float cooldown;
			public float manaCost;
			public string atkFx;
			public float atkFxTime;
			public string atkSound;
			public string missile;
			public float firingTime;
		}

		public string id;
		public string name;
		public string desc;
		public bool isCommon;
		public CastType castType;
		public CampType campType;
		public RangeType rangeType;
		public EntityFlag targetFlag;
		public bool canInterrupt;
		public bool ignoreObstacles;
		public string icon;
		public string[] passiveBuffs;
		public string[] buffs;
		public Level[] levels;

		public void LoadFromDef( string id )
		{
			this.id = id;

			Hashtable def = Defs.GetSkill( this.id );
			this.name = def.GetString( "name" );
			this.desc = def.GetString( "desc" );
			this.isCommon = def.GetBoolean( "is_common" );
			this.castType = ( CastType ) def.GetInt( "cast_type" );
			this.campType = ( CampType ) def.GetInt( "camp_type" );
			this.rangeType = ( RangeType ) def.GetInt( "range_type" );
			this.targetFlag = ( EntityFlag ) def.GetInt( "target_flag" );
			this.canInterrupt = def.GetBoolean( "can_interrupt" );
			this.ignoreObstacles = def.GetBoolean( "ignore_obstacles" );
			this.icon = def.GetString( "icon" );
			this.passiveBuffs = def.GetStringArray( "passive_buffs" );
			this.buffs = def.GetStringArray( "buffs" );

			ArrayList lvls = def.GetList( "level" );
			if ( lvls != null )
			{
				int count = lvls.Count;
				this.levels = new Level[count];
				for ( int i = 0; i < count; i++ )
				{
					Level lvl = this.levels[i] = new Level();
					Hashtable ldef = ( Hashtable ) lvls[i];
					lvl.atkTime = ldef.GetFloat( "atk_time" );
					lvl.sufTime = ldef.GetFloat( "suf_time" );
					lvl.action = ldef.GetString( "action" );
					lvl.actionLength = ldef.GetFloat( "action_length" );
					lvl.dashStartSpeed = ldef.GetFloat( "dash_start_speed" );
					float[] c = ldef.GetFloatArray( "dash_speed_curve" );
					if ( c != null && c.Length > 0 )
					{
						AnimationCurve curve = new AnimationCurve();
						for ( int j = 0; j < c.Length; j += 4 )
							curve.AddKey( new Keyframe( c[j + 0], c[j + 1], c[j + 2], c[j + 3] ) );
						lvl.dashSpeedCurve = curve;
					}
					lvl.distance = ldef.GetFloat( "distance" );
					lvl.cooldown = ldef.GetFloat( "cooldown" );
					lvl.manaCost = ldef.GetFloat( "mana" );
					lvl.atkFx = ldef.GetString( "atk_fx" );
					lvl.atkFxTime = ldef.GetFloat( "atk_fx_time" );
					lvl.atkSound = ldef.GetString( "atk_sound" );
					lvl.missile = ldef.GetString( "missile" );
					lvl.firingTime = ldef.GetFloat( "firing_time" );
				}
			}
		}
	}
}