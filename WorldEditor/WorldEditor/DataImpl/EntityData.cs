using System.Collections;
using WorldEditor.Attributes;

namespace WorldEditor.DataImpl
{
	[DataClass( name = "单位" )]
	public class EntityData : DataMajorNode
	{
		public EntityData( string id, string name )
			: base( id, name )
		{
		}

		public override void FromJson( Core.Misc.Map map )
		{
			foreach ( DictionaryEntry de in map )
			{
				string key = ( string )de.Key;
				Entity item = new Entity( key );
				item.FromJson( ( Core.Misc.Map )de.Value );
				item.parent = this;
				this.datas[key] = item;
			}
		}
		public class Entity : DataMinorNode
		{
			[DataProperty( name = "模型", valueType = ValueType.String )]
			public string model;

			public Entity( string id )
				: base( id )
			{
			}

			public override void FromJson( Core.Misc.Map data )
			{
				this.name = data.GetString( "name" );
				this.model = data.GetString( "model" );
			}

			public override Core.Misc.Map ToJson()
			{
				Core.Misc.Map data = new Core.Misc.Map();
				data["name"] = this.name;
				data["model"] = this.model;
				return data;
			}
		}
	}
}