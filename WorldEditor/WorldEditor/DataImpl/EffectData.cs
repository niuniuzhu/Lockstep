using System.Collections;
using WorldEditor.Attributes;

namespace WorldEditor.DataImpl
{
	[DataClass( name = "特效" )]
	public class EffectData : DataMajorNode
	{
		public EffectData( string id, string name )
			: base( id, name )
		{
		}

		public override void FromJson( Core.Misc.Map map )
		{
			foreach ( DictionaryEntry de in map )
			{
				string key = ( string )de.Key;
				Effect item = new Effect( key );
				item.FromJson( ( Core.Misc.Map )de.Value );
				item.parent = this;
				this.datas[key] = item;
			}
		}

		public class Effect : DataMinorNode
		{
			[DataProperty( name = "有效时间", valueType = ValueType.Float )]
			public float liftTime { get; private set; }

			[DataProperty( name = "程序控制允许", valueType = ValueType.Boolean )]
			public bool programmatic { get; private set; }

			public Effect( string id )
				: base( id )
			{
			}

			public override void FromJson( Core.Misc.Map data )
			{
				this.name = data.GetString( "name" );
				this.liftTime = data.GetFloat( "life_time" );
				this.programmatic = data.GetBoolean( "programmatic" );
			}

			public override Core.Misc.Map ToJson()
			{
				Core.Misc.Map data = new Core.Misc.Map();
				data["name"] = this.name;
				data["life_time"] = this.liftTime;
				data["programmatic"] = this.programmatic;
				return data;
			}
		}
	}
}