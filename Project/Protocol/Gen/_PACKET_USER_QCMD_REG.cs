using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 101, 1 )]
	public class _PACKET_USER_QCMD_REG : Packet
	{
		public _DTO_account dto;

		public _PACKET_USER_QCMD_REG() : base( 101, 1 )
		{
		}

		public _PACKET_USER_QCMD_REG( _DTO_account dto ) : base( 101, 1 )
		{
			this.dto = dto;
		}

		public _PACKET_USER_QCMD_REG( string account,string password ) : base( 101, 1 )
		{
			this.dto = new _DTO_account( account,password );
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );
			this.dto.Serialize( buffer );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );
			this.dto = new _DTO_account();
			this.dto.Deserialize( buffer );
		}

		public override string ToString()
		{
			return $"module:{this.module}, cmd:{this.command}, dto:{this.dto}";
		}
	}
}