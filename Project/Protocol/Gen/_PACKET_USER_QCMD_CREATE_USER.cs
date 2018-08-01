using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 101, 2 )]
	public class _PACKET_USER_QCMD_CREATE_USER : Packet
	{
		public _DTO_string dto;

		public _PACKET_USER_QCMD_CREATE_USER() : base( 101, 2 )
		{
		}

		public _PACKET_USER_QCMD_CREATE_USER( _DTO_string dto ) : base( 101, 2 )
		{
			this.dto = dto;
		}

		public _PACKET_USER_QCMD_CREATE_USER( string value ) : base( 101, 2 )
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