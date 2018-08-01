using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 100, 32000 )]
	public class _PACKET_GENERIC_ACMD_REPLY : Packet
	{
		public _DTO_reply dto;

		public _PACKET_GENERIC_ACMD_REPLY() : base( 100, 32000 )
		{
		}

		public _PACKET_GENERIC_ACMD_REPLY( _DTO_reply dto ) : base( 100, 32000 )
		{
			this.dto = dto;
		}

		public _PACKET_GENERIC_ACMD_REPLY( ushort result,ushort src_cmd,byte src_module ) : base( 100, 32000 )
		{
			this.dto = new _DTO_reply( result,src_cmd,src_module );
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );
			this.dto.Serialize( buffer );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );
			this.dto = new _DTO_reply();
			this.dto.Deserialize( buffer );
		}

		public override string ToString()
		{
			return $"module:{this.module}, cmd:{this.command}, dto:{this.dto}";
		}
	}
}