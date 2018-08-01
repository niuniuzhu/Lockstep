using Core.Misc;
using WorldEditor.Attributes;

namespace WorldEditor.DataImpl
{
	public class RootData : DataNode
	{
		[DataProperty( name = "ID", valueType = ValueType.Map )]
		public MapData maps { get; private set; }

		public RootData()
			: base( string.Empty )
		{
		}

		public override void FromJson( Map data )
		{
			this.maps = new MapData( "maps" );
			this.maps.FromJson( data.GetMap( "maps" ) );
		}
	}
}