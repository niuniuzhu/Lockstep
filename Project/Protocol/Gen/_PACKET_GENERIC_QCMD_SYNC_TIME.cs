using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 100, 0 )]
	public class _PACKET_GENERIC_QCMD_SYNC_TIME : Packet
	{
		public _DTO_long dto;

		public _PACKET_GENERIC_QCMD_SYNC_TIME() : base( 100, 0 )
		{
		}

		public _PACKET_GENERIC_QCMD_SYNC_TIME( _DTO_long dto ) : base( 100, 0 )
		{
			this.dto = dto;
		}

		public _PACKET_GENERIC_QCMD_SYNC_TIME( long value ) : base( 100, 0 )
		{
			this.dto = new _DTO_long( value );
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );
			this.dto.Serialize( buffer );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );
			this.dto = new _DTO_long();
			this.dto.Deserialize( buffer );
		}

		public override string ToString()
		{
			return $"module:{this.module}, cmd:{this.command}, dto:{this.dto}";
		}
	}
}