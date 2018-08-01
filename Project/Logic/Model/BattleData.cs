using System.Collections;
using System.Collections.Generic;
using Core.Math;
using Core.Misc;

namespace Logic.Model
{
	public class BattleData
	{
		public string id;
		public string name;
		public string model;
		public Vec3 bornPos1;
		public Vec3 bornPos2;
		public Vec3 bornDir1;
		public Vec3 bornDir2;
		public Vec3 basePoint1;
		public Vec3 basePoint2;
		public float bornRange;
		public Camera camera;
		public Dictionary<string, Structure> structures;
		public Dictionary<string, Neutral> neutrals;
		public string script;

		public void LoadFromDef( string id )
		{
			this.id = id;

			Hashtable def = Defs.GetMap( this.id );
			this.name = def.GetString( "name" );
			this.model = def.GetString( "model" );
			this.bornPos1 = def.GetVec3( "born_pos_1" );
			this.bornPos2 = def.GetVec3( "born_pos_2" );
			this.bornDir1 = Vec3.Normalize( def.GetVec3( "born_dir_1" ) );
			this.bornDir2 = Vec3.Normalize( def.GetVec3( "born_dir_2" ) );
			this.basePoint1 = def.GetVec3( "base_point_1" );
			this.basePoint2 = def.GetVec3( "base_point_2" );
			this.bornRange = def.GetFloat( "born_rnd" );
			this.camera = new Camera( def.GetMap( "camera" ) );
			Hashtable sDefs = def.GetMap( "structures" );
			this.structures = new Dictionary<string, Structure>();
			foreach ( DictionaryEntry de in sDefs )
			{
				this.structures[( string )de.Key] = new Structure( ( Hashtable )de.Value );
			}
			Hashtable nDefs = def.GetMap( "neutrals" );
			this.neutrals = new Dictionary<string, Neutral>();
			foreach ( DictionaryEntry de in nDefs )
			{
				this.neutrals[( string )de.Key] = new Neutral( ( Hashtable )de.Value );
			}
			this.script = def.GetString( "script" );
		}

		public class Camera
		{
			public Vec3 offset;
			public readonly float fov;
			public readonly float smoothTime;

			public Camera( Hashtable def )
			{
				this.offset = def.GetVec3( "offset" );
				this.fov = def.GetFloat( "fov" );
				this.smoothTime = def.GetFloat( "smooth_time" );
			}
		}

		public class Structure : Neutral
		{
			public Structure( Hashtable def ) : base( def )
			{
			}
		}

		public class Neutral
		{
			public readonly string id;
			public readonly int team;
			public Vec3 pos;
			public Vec3 dir;

			public Neutral( Hashtable def )
			{
				this.id = def.GetString( "id" );
				this.team = def.GetInt( "team" );
				this.pos = def.GetVec3( "pos" );
				this.dir = Vec3.Normalize( def.GetVec3( "dir" ) );
			}
		}
	}
}