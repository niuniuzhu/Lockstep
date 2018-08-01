using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 100, 32001 )]
	public class _PACKET_GENERIC_ACMD_SYNC_TIME : Packet
	{
		public _DTO_sync_time dto;

		public _PACKET_GENERIC_ACMD_SYNC_TIME() : base( 100, 32001 )
		{
		}

		public _PACKET_GENERIC_ACMD_SYNC_TIME( _DTO_sync_time dto ) : base( 100, 32001 )
		{
			this.dto = dto;
		}

		public _PACKET_GENERIC_ACMD_SYNC_TIME( long clientTime,long serverTime ) : base( 100, 32001 )
		{
			this.dto = new _DTO_sync_time( clientTime,serverTime );
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );
			this.dto.Serialize( buffer );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );
			this.dto = new _DTO_sync_time();
			this.dto.Deserialize( buffer );
		}

		public override string ToString()
		{
			return $"module:{this.module}, cmd:{this.command}, dto:{this.dto}";
		}
	}
}