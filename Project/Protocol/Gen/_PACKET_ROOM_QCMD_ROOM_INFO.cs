using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 103, 1 )]
	public class _PACKET_ROOM_QCMD_ROOM_INFO : Packet
	{
		public _DTO_int dto;

		public _PACKET_ROOM_QCMD_ROOM_INFO() : base( 103, 1 )
		{
		}

		public _PACKET_ROOM_QCMD_ROOM_INFO( _DTO_int dto ) : base( 103, 1 )
		{
			this.dto = dto;
		}

		public _PACKET_ROOM_QCMD_ROOM_INFO( int value ) : base( 103, 1 )
		{
			this.dto = new _DTO_int( value );
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );
			this.dto.Serialize( buffer );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );
			this.dto = new _DTO_int();
			this.dto.Deserialize( buffer );
		}

		public override string ToString()
		{
			return $"module:{this.module}, cmd:{this.command}, dto:{this.dto}";
		}
	}
}