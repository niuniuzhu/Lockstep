using System.Collections;
using UnityEngine;
using WorldEditor.Attributes;

namespace WorldEditor.DataImpl
{
	public class MapData : DataNode
	{
		public Map

		public MapData( string key )
			: base( key )
		{
		}

		public override void FromJson( Core.Misc.Map map )
		{
			foreach ( DictionaryEntry de in map )
			{
				string key = ( string )de.Key;
				Map item = new Map( key );
				item.FromJson( ( Core.Misc.Map )de.Value );
				item.parent = this;
				this.children[key] = item;
			}
		}

		public class Map : PropertyDataNode
		{
			[DataProperty( name = "名字", valueType = ValueType.String )]
			public string name { get; set; }

			[DataProperty( name = "模型", valueType = ValueType.String, desc = "测试说明测试说明测试说明测试说明测试说明测试说明测试说明测试说明" )]
			public string model { get; private set; }

			[DataProperty( name = "队伍1的出生坐标", valueType = ValueType.Vector3 )]
			public Vector3 bornPos1 { get; private set; }

			[DataProperty( name = "队伍1的出生朝向", valueType = ValueType.Vector3 )]
			public Vector3 bornDir1 { get; private set; }

			[DataProperty( name = "队伍2的出生坐标", valueType = ValueType.Vector3 )]
			public Vector3 bornPos2 { get; private set; }

			[DataProperty( name = "队伍2的出生朝向", valueType = ValueType.Vector3 )]
			public Vector3 bornDir2 { get; private set; }

			[DataProperty( name = "出生坐标随机范围", valueType = ValueType.Float )]
			public float bornRnd { get; private set; }

			[DataProperty( name = "中立单位", valueType = ValueType.List, metaType = typeof( Neutral ) )]
			public Neutral[] neutrals { get; private set; }

			public Map( string key )
				: base( key )
			{
			}

			public override void FromJson( Core.Misc.Map data )
			{
				this.name = data.GetString( "name" );
				this.model = data.GetString( "model" );
				this.bornPos1 = data.GetVector3( "bornPos1" );
				this.bornDir1 = data.GetVector3( "bornDir1" );
				this.bornPos2 = data.GetVector3( "bornPos2" );
				this.bornDir2 = data.GetVector3( "bornDir2" );
				this.bornRnd = data.GetFloat( "bornRnd" );
				ArrayList al = data.GetList( "neutrals" );
				int count = al.Count;
				this.neutrals = new Neutral[count];
				for ( int i = 0; i < count; i++ )
				{
					Core.Misc.Map m = ( Core.Misc.Map )al[i];
					Neutral neutral = new Neutral();
					neutral.FromJson( m );
					neutral.parent = this;
					this.neutrals[i] = neutral;
				}
				this.MakePropertyNode( ref data );
			}

			public override Core.Misc.Map ToJson()
			{
				Core.Misc.Map data = new Core.Misc.Map();
				data["name"] = this.name;
				data["model"] = this.model;
				data.SetVector3( "bornPos1", this.bornPos1 );
				data.SetVector3( "bornDir1", this.bornDir1 );
				data.SetVector3( "bornPos2", this.bornPos2 );
				data.SetVector3( "bornDir2", this.bornDir2 );
				data["bornRnd"] = this.bornRnd;
				int count = this.neutrals.Length;
				Core.Misc.Map[] maps = new Core.Misc.Map[count];
				for ( int i = 0; i < count; i++ )
				{
					Neutral neutral = this.neutrals[i];
					maps[i] = neutral.ToJson();
				}
				data["neutrals"] = maps;
				return data;
			}
		}

		public class Neutral : PropertyDataNode
		{
			[DataProperty( name = "ID", valueType = ValueType.String )]
			public string nid;

			[DataProperty( name = "出生坐标", valueType = ValueType.Vector3 )]
			public Vector3 pos;

			[DataProperty( name = "出生坐标", valueType = ValueType.Vector3 )]
			public Vector3 dir;

			public override void FromJson( Core.Misc.Map data )
			{
				this.nid = data.GetString( "id" );
				this.pos = data.GetVector3( "pos" );
				this.dir = data.GetVector3( "dir" );
				this.MakePropertyNode( ref data );
			}

			public override Core.Misc.Map ToJson()
			{
				Core.Misc.Map data = new Core.Misc.Map();
				data["id"] = this.nid;
				data.SetVector3( "pos", this.pos );
				data.SetVector3( "dir", this.dir );
				return data;
			}
		}
	}
}