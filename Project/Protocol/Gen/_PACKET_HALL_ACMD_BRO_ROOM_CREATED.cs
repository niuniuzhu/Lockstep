using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 102, 32001 )]
	public class _PACKET_HALL_ACMD_BRO_ROOM_CREATED : Packet
	{
		public _DTO_room_info dto;

		public _PACKET_HALL_ACMD_BRO_ROOM_CREATED() : base( 102, 32001 )
		{
		}

		public _PACKET_HALL_ACMD_BRO_ROOM_CREATED( _DTO_room_info dto ) : base( 102, 32001 )
		{
			this.dto = dto;
		}

		public _PACKET_HALL_ACMD_BRO_ROOM_CREATED( int ct,string map,string name,int roomId ) : base( 102, 32001 )
		{
			this.dto = new _DTO_room_info( ct,map,name,roomId );
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );
			this.dto.Serialize( buffer );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );
			this.dto = new _DTO_room_info();
			this.dto.Deserialize( buffer );
		}

		public override string ToString()
		{
			return $"module:{this.module}, cmd:{this.command}, dto:{this.dto}";
		}
	}
}