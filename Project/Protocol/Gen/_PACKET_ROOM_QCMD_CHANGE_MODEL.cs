using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 103, 4 )]
	public class _PACKET_ROOM_QCMD_CHANGE_MODEL : Packet
	{
		public _DTO_byte dto;

		public _PACKET_ROOM_QCMD_CHANGE_MODEL() : base( 103, 4 )
		{
		}

		public _PACKET_ROOM_QCMD_CHANGE_MODEL( _DTO_byte dto ) : base( 103, 4 )
		{
			this.dto = dto;
		}

		public _PACKET_ROOM_QCMD_CHANGE_MODEL( byte value ) : base( 103, 4 )
		{
			this.dto = new _DTO_byte( value );
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );
			this.dto.Serialize( buffer );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );
			this.dto = new _DTO_byte();
			this.dto.Deserialize( buffer );
		}

		public override string ToString()
		{
			return $"module:{this.module}, cmd:{this.command}, dto:{this.dto}";
		}
	}
}