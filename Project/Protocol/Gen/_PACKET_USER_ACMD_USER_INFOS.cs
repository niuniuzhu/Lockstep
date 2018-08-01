using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 101, 32001 )]
	public class _PACKET_USER_ACMD_USER_INFOS : Packet
	{
		public _DTO_charactor dto;

		public _PACKET_USER_ACMD_USER_INFOS() : base( 101, 32001 )
		{
		}

		public _PACKET_USER_ACMD_USER_INFOS( _DTO_charactor dto ) : base( 101, 32001 )
		{
			this.dto = dto;
		}

		public _PACKET_USER_ACMD_USER_INFOS( string name,string uid ) : base( 101, 32001 )
		{
			this.dto = new _DTO_charactor( name,uid );
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );
			this.dto.Serialize( buffer );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );
			this.dto = new _DTO_charactor();
			this.dto.Deserialize( buffer );
		}

		public override string ToString()
		{
			return $"module:{this.module}, cmd:{this.command}, dto:{this.dto}";
		}
	}
}