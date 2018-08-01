using System.Collections;
using WorldEditor.Attributes;

namespace WorldEditor.DataImpl
{
	[DataClass( name = "技能" )]
	public class SkillData : DataMajorNode
	{
		public SkillData( string id, string name )
			: base( id, name )
		{
		}

		public override void FromJson( Core.Misc.Map map )
		{
			foreach ( DictionaryEntry de in map )
			{
				string key = ( string )de.Key;
				Skill item = new Skill( key );
				item.FromJson( ( Core.Misc.Map )de.Value );
				item.parent = this;
				this.datas[key] = item;
			}
		}

		public class Skill : DataMinorNode
		{
			public Skill( string id )
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