using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 101, 32002 )]
	public class _PACKET_USER_ACMD_USER_ONLINE : Packet
	{
		public _DTO_charactor dto;

		public _PACKET_USER_ACMD_USER_ONLINE() : base( 101, 32002 )
		{
		}

		public _PACKET_USER_ACMD_USER_ONLINE( _DTO_charactor dto ) : base( 101, 32002 )
		{
			this.dto = dto;
		}

		public _PACKET_USER_ACMD_USER_ONLINE( string name,string uid ) : base( 101, 32002 )
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