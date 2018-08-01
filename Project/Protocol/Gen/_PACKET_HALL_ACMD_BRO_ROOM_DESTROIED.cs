using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 102, 32002 )]
	public class _PACKET_HALL_ACMD_BRO_ROOM_DESTROIED : Packet
	{
		public _DTO_int dto;

		public _PACKET_HALL_ACMD_BRO_ROOM_DESTROIED() : base( 102, 32002 )
		{
		}

		public _PACKET_HALL_ACMD_BRO_ROOM_DESTROIED( _DTO_int dto ) : base( 102, 32002 )
		{
			this.dto = dto;
		}

		public _PACKET_HALL_ACMD_BRO_ROOM_DESTROIED( int value ) : base( 102, 32002 )
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