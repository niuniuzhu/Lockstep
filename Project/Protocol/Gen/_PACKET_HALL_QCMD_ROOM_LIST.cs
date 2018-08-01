using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 102, 0 )]
	public class _PACKET_HALL_QCMD_ROOM_LIST : Packet
	{
		public _DTO_request_room_list dto;

		public _PACKET_HALL_QCMD_ROOM_LIST() : base( 102, 0 )
		{
		}

		public _PACKET_HALL_QCMD_ROOM_LIST( _DTO_request_room_list dto ) : base( 102, 0 )
		{
			this.dto = dto;
		}

		public _PACKET_HALL_QCMD_ROOM_LIST( byte count,byte from ) : base( 102, 0 )
		{
			this.dto = new _DTO_request_room_list( count,from );
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );
			this.dto.Serialize( buffer );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );
			this.dto = new _DTO_request_room_list();
			this.dto.Deserialize( buffer );
		}

		public override string ToString()
		{
			return $"module:{this.module}, cmd:{this.command}, dto:{this.dto}";
		}
	}
}