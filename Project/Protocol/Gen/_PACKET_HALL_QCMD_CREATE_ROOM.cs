using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 102, 1 )]
	public class _PACKET_HALL_QCMD_CREATE_ROOM : Packet
	{
		public _DTO_string dto;

		public _PACKET_HALL_QCMD_CREATE_ROOM() : base( 102, 1 )
		{
		}

		public _PACKET_HALL_QCMD_CREATE_ROOM( _DTO_string dto ) : base( 102, 1 )
		{
			this.dto = dto;
		}

		public _PACKET_HALL_QCMD_CREATE_ROOM( string value ) : base( 102, 1 )
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