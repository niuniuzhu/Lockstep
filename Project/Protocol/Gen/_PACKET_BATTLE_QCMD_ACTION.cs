using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 104, 1 )]
	public class _PACKET_BATTLE_QCMD_ACTION : Packet
	{
		public _DTO_frame_info dto;

		public _PACKET_BATTLE_QCMD_ACTION() : base( 104, 1 )
		{
		}

		public _PACKET_BATTLE_QCMD_ACTION( _DTO_frame_info dto ) : base( 104, 1 )
		{
			this.dto = dto;
		}

		public _PACKET_BATTLE_QCMD_ACTION( _DTO_action_info[] actions,int frameId ) : base( 104, 1 )
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