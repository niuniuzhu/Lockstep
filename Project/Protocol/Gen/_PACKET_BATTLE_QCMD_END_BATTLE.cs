using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 104, 3 )]
	public class _PACKET_BATTLE_QCMD_END_BATTLE : Packet
	{
		public _DTO_byte dto;

		public _PACKET_BATTLE_QCMD_END_BATTLE() : base( 104, 3 )
		{
		}

		public _PACKET_BATTLE_QCMD_END_BATTLE( _DTO_byte dto ) : base( 104, 3 )
		{
			this.dto = dto;
		}

		public _PACKET_BATTLE_QCMD_END_BATTLE( byte value ) : base( 104, 3 )
		{
			this.dto = new _DTO_byte( value );
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );
			this.dto.Serialize( buffer );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );
			this.dto = new _DTO_byte();
			this.dto.Deserialize( buffer );
		}

		public override string ToString()
		{
			return $"module:{this.module}, cmd:{this.command}, dto:{this.dto}";
		}
	}
}