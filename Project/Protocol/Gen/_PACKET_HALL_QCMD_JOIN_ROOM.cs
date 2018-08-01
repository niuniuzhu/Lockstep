using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 102, 2 )]
	public class _PACKET_HALL_QCMD_JOIN_ROOM : Packet
	{
		public _DTO_int dto;

		public _PACKET_HALL_QCMD_JOIN_ROOM() : base( 102, 2 )
		{
		}

		public _PACKET_HALL_QCMD_JOIN_ROOM( _DTO_int dto ) : base( 102, 2 )
		{
			this.dto = dto;
		}

		public _PACKET_HALL_QCMD_JOIN_ROOM( int value ) : base( 102, 2 )
		{
			this.dto = new _DTO_int( value );
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );
			this.dto.Serialize( buffer );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );
			this.dto = new _DTO_int();
			this.dto.Deserialize( buffer );
		}

		public override string ToString()
		{
			return $"module:{this.module}, cmd:{this.command}, dto:{this.dto}";
		}
	}
}