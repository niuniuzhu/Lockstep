using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 103, 2 )]
	public class _PACKET_ROOM_QCMD_CHANGE_MAP : Packet
	{
		public _DTO_string dto;

		public _PACKET_ROOM_QCMD_CHANGE_MAP() : base( 103, 2 )
		{
		}

		public _PACKET_ROOM_QCMD_CHANGE_MAP( _DTO_string dto ) : base( 103, 2 )
		{
			this.dto = dto;
		}

		public _PACKET_ROOM_QCMD_CHANGE_MAP( string value ) : base( 103, 2 )
		{
			this.dto = new _DTO_string( value );
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );
			this.dto.Serialize( buffer );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );
			this.dto = new _DTO_string();
			this.dto.Deserialize( buffer );
		}

		public override string ToString()
		{
			return $"module:{this.module}, cmd:{this.command}, dto:{this.dto}";
		}
	}
}