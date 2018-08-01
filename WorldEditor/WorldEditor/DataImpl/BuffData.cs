using System.Collections;
using WorldEditor.Attributes;

namespace WorldEditor.DataImpl
{
	[DataClass( name = "效果/Buff" )]
	public class BuffData : DataMajorNode
	{
		public BuffData( string id, string name )
			: base( id, name )
		{
		}

		public override void FromJson( Core.Misc.Map map )
		{
			foreach ( DictionaryEntry de in map )
			{
				string key = ( string )de.Key;
				Buff item = new Buff( key );
				item.FromJson( ( Core.Misc.Map )de.Value );
				item.parent = this;
				this.datas[key] = item;
			}
		}

		public class Buff : DataMinorNode
		{
			public Buff( string id )
				: base( id )
			{
			}

			public override void FromJson( Core.Misc.Map data )
			{
				this.name = data.GetString( "name" );
			}

			public override Core.Misc.Map ToJson()
			{
				Core.Misc.Map data = new Core.Misc.Map();
				data["name"] = this.name;
				return data;
			}
		}
	}
}