using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 104, 32001 )]
	public class _PACKET_BATTLE_ACMD_FRAME : Packet
	{
		public _DTO_frame_info dto;

		public _PACKET_BATTLE_ACMD_FRAME() : base( 104, 32001 )
		{
		}

		public _PACKET_BATTLE_ACMD_FRAME( _DTO_frame_info dto ) : base( 104, 32001 )
		{
			this.dto = dto;
		}

		public _PACKET_BATTLE_ACMD_FRAME( _DTO_action_info[] actions,int frameId ) : base( 104, 32001 )
		{
			this.dto = new _DTO_frame_info( actions,frameId );
		}

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );
			this.dto.Serialize( buffer );
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );
			this.dto = new _DTO_frame_info();
			this.dto.Deserialize( buffer );
		}

		public override string ToString()
		{
			return $"module:{this.module}, cmd:{this.command}, dto:{this.dto}";
		}
	}
}