using System;
using System.Collections;
using Core.Misc;
using Logic.Property;

namespace Logic.Model
{
	public class BuffStateData
	{
		public class Level
		{
			public Attr[] attrs;
			public float[] values;
			public Trigger trigger;
			public float[] extra;
			public string[] extra_s;
			public float duration;//-2-不计时,-1-随buff消失
			public string[] fxs;
		}

		public class Trigger
		{
			public readonly float interval;
			public readonly Attr[][] attrs;
			public readonly float[][] values;
			public readonly string[] fxs;

			public Trigger( Hashtable def )
			{
				this.interval = def.GetFloat( "interval" );
				this.fxs = def.GetStringArray( "fxs" );

				ArrayList ar = def.GetList( "attrs" );
				if ( ar != null )
				{
					int count = ar.Count;
					this.attrs = new Attr[count][];
					for ( int i = 0; i < count; i++ )
					{
						ArrayList ari = ( ArrayList ) ar[i];
						int c2 = ari.Count;
						Attr[] attri = this.attrs[i] = new Attr[c2];
						for ( int j = 0; j < c2; j++ )
							attri[j] = ( Attr ) ari[j];
					}
				}

				ar = def.GetList( "values" );
				if ( ar != null )
				{
					int count = ar.Count;
					this.values = new float[count][];
					for ( int i = 0; i < count; i++ )
					{
						ArrayList ari = ( ArrayList ) ar[i];
						int c2 = ari.Count;
						float[] valuei = this.values[i] = new float[c2];
						for ( int j = 0; j < c2; j++ )
							valuei[j] = Convert.ToSingle( ari[j] );
					}
				}
			}
		}

		public string id;
		public BuffStateType type;
		public BuffStateOverlapType overlapType;
		public BeneficialType beneficialType;
		public Level[] levels;

		internal void LoadFromDef( string id )
		{
			this.id = id;
			Hashtable def = Defs.GetBuffState( this.id );
			this.type = ( BuffStateType ) def.GetInt( "type" );
			this.overlapType = ( BuffStateOverlapType ) def.GetInt( "overlap_type" );
			this.beneficialType = ( BeneficialType ) def.GetInt( "beneficial_type" );

			ArrayList lvls = def.GetList( "level" );
			if ( lvls != null )
			{
				int count = lvls.Count;
				this.levels = new Level[count];
				for ( int i = 0; i < count; i++ )
				{
					Level lvl = this.levels[i] = new Level();
					Hashtable ldef = ( Hashtable ) lvls[i];

					int[] attri = ldef.GetIntArray( "attrs" );
					if ( attri != null )
					{
						int c2 = attri.Length;
						lvl.attrs = new Attr[c2];
						for ( int j = 0; j < c2; j++ )
							lvl.attrs[j] = ( Attr ) attri[j];
					}
					lvl.values = ldef.GetFloatArray( "values" );
					if ( ldef.ContainsKey( "trigger" ) )
						lvl.trigger = new Trigger( ldef.GetMap( "trigger" ) );
					lvl.extra = ldef.GetFloatArray( "extra" );
					lvl.extra_s = ldef.GetStringArray( "extra_s" );
					lvl.duration = ldef.GetFloat( "duration" );
					lvl.fxs = ldef.GetStringArray( "fxs" );
				}
			}
		}
	}
}