using System;
using System.Collections;
using Core.Misc;

namespace Logic.Model
{
	public class BuffData
	{
		public class Level
		{
			public float radius;
			public float[] extra;
			public string[] extra_s;
			public string fx;//跟随buff的特效
			public string areaFx;//区域特效
			public float duration;
			public float speed;
			public int maxTriggerTargets;
			public int perTargetTriggerCount;
			public int maxTriggerCount;
			public Trigger trigger;
		}

		public string id;
		public string name;

		public CampType campType;
		public EntityFlag targetFlag;
		public RangeType rangeType;
		public SpawnPoint spawnPoint;
		public DeadType deadType;
		public Orbit orbit;
		public bool autoScaleAreaFx;
		public bool canInterrupt;
		public string[] enterStates;
		public string[] triggerStates;

		public Level[] levels;

		public void LoadFromDef( string id )
		{
			this.id = id;

			Hashtable def = Defs.GetBuff( this.id );
			this.name = def.GetString( "name" );

			this.campType = ( CampType ) def.GetInt( "camp_type" );
			this.targetFlag = ( EntityFlag ) def.GetInt( "target_flag" );
			this.rangeType = ( RangeType ) def.GetInt( "range_type" );
			this.spawnPoint = ( SpawnPoint ) def.GetInt( "spawn_point" );
			this.deadType = ( DeadType ) def.GetInt( "dead_type" );
			this.orbit = ( Orbit ) def.GetInt( "orbit" );
			this.autoScaleAreaFx = def.GetBoolean( "auto_scale_area_fx" );
			this.canInterrupt = def.GetBoolean( "can_interrupt" );
			this.enterStates = def.GetStringArray( "enter_states" );
			this.triggerStates = def.GetStringArray( "trigger_states" );

			ArrayList lvls = def.GetList( "level" );
			if ( lvls != null )
			{
				int count = lvls.Count;
				this.levels = new Level[count];
				for ( int i = 0; i < count; i++ )
				{
					Level lvl = this.levels[i] = new Level();
					Hashtable ldef = ( Hashtable ) lvls[i];
					lvl.radius = ldef.GetFloat( "radius" );
					lvl.areaFx = ldef.GetString( "area_fx" );
					lvl.extra = ldef.GetFloatArray( "extra" );
					lvl.extra_s = ldef.GetStringArray( "extra_s" );
					lvl.duration = ldef.GetFloat( "duration" );
					lvl.speed = ldef.GetFloat( "speed" );
					int n = ldef.GetInt( "max_trigger_targets" );
					lvl.maxTriggerTargets = n == 0 ? int.MaxValue : n;
					n = ldef.GetInt( "per_target_trigger_count" );
					lvl.perTargetTriggerCount = n == 0 ? int.MaxValue : n;
					lvl.maxTriggerCount = ldef.GetInt( "max_trigger_count" );
					if ( ldef.ContainsKey( "trigger" ) )
						lvl.trigger = new Trigger( ldef.GetMap( "trigger" ) );
					else if ( this.triggerStates != null )
						lvl.trigger = new Trigger();
				}
			}
		}

		public class Trigger
		{
			public readonly float interval;
			public readonly float[] times;
			public readonly float[] distances;
			public readonly bool[] damaged;
			public readonly float[] td;
			public readonly float[] tpadp;
			public readonly float[] tpapp;
			public readonly float[] ad;
			public readonly float[] ap;
			public readonly float[] padp;
			public readonly float[] papp;
			public readonly float[][] extras;
			public readonly string[] fxs;//命中目标时才会出现的特效
			public readonly string[] tfxs;//只要触发就出现的特效
			public readonly Summon[][] summons;

			public Trigger()
			{
			}

			public Trigger( Hashtable def )
			{
				this.interval = def.GetFloat( "interval" );
				this.times = def.GetFloatArray( "times" );
				this.distances = def.GetFloatArray( "distances" );

				this.damaged = def.GetBooleanArray( "damaged" );
				this.td = def.GetFloatArray( "td" );
				this.tpadp = def.GetFloatArray( "tpadp" );
				this.tpapp = def.GetFloatArray( "tpapp" );
				this.ad = def.GetFloatArray( "ad" );
				this.padp = def.GetFloatArray( "padp" );
				this.ap = def.GetFloatArray( "ap" );
				this.papp = def.GetFloatArray( "papp" );

				ArrayList ar = def.GetList( "extras" );
				if ( ar != null )
				{
					int count = ar.Count;
					this.extras = new float[count][];
					for ( int i = 0; i < count; i++ )
					{
						ArrayList ari = ( ArrayList ) ar[i];
						int c2 = ari.Count;
						float[] valuei = this.extras[i] = new float[c2];
						for ( int j = 0; j < c2; j++ )
							valuei[j] = Convert.ToSingle( ari[j] );
					}
				}

				this.fxs = def.GetStringArray( "fxs" );
				this.tfxs = def.GetStringArray( "tfxs" );

				ar = def.GetList( "summons" );
				if ( ar != null )
				{
					int count = ar.Count;
					this.summons = new Summon[count][];
					for ( int i = 0; i < count; i++ )
					{
						ArrayList ari = ( ArrayList ) ar[i];
						int c2 = ari.Count;
						Summon[] summonss = this.summons[i] = new Summon[c2];
						for ( int j = 0; j < c2; j++ )
							summonss[j] = new Summon( ( Hashtable ) ari[j] );
					}
				}
			}
		}

		public class Summon
		{
			public enum Fill
			{
				Base,
				BaseShell
			}

			public enum Direction
			{
				Random,
				FollowCaster
			}

			public readonly string id;
			public readonly int count;
			public readonly Fill fill;
			public readonly Direction direction;

			public Summon( Hashtable def )
			{
				this.id = def.GetString( "id" );
				this.count = def.GetInt( "count" );
				this.fill = ( Fill ) def.GetInt( "fill" );
				this.direction = ( Direction ) def.GetInt( "direction" );
			}
		}
	}
}