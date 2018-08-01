using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 103, 32001 )]
	public class _PACKET_ROOM_ACMD_ROOM_INFO : Packet
	{
		public _DTO_room_info_detail dto;

		public _PACKET_ROOM_ACMD_ROOM_INFO() : base( 103, 32001 )
		{
		}

		public _PACKET_ROOM_ACMD_ROOM_INFO( _DTO_room_info_detail dto ) : base( 103, 32001 )
		{
			this.dto = dto;
		}

		public _PACKET_ROOM_ACMD_ROOM_INFO( string host,string map,string name,_DTO_player_info[] players,int roomId ) : base( 103, 32001 )
		{
			this.dto = new _DTO_room_info_detail( host,map,name,players,roomId );
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );
			this.dto.Serialize( buffer );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );
			this.dto = new _DTO_room_info_detail();
			this.dto.Deserialize( buffer );
		}

		public override string ToString()
		{
			return $"module:{this.module}, cmd:{this.command}, dto:{this.dto}";
		}
	}
}