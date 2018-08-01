using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 103, 9 )]
	public class _PACKET_ROOM_QCMD_REMOVE_FIGHT_READY : Packet
	{
		

		public _PACKET_ROOM_QCMD_REMOVE_FIGHT_READY() : base( 103, 9 )
		{
		}

		

		protected override void InternalSerialize( StreamBuffer buffer )
		{
			base.InternalSerialize( buffer );
			
		}

		protected override void InternalDeserialize( StreamBuffer buffer )
		{
			base.InternalDeserialize( buffer );
			
		}

		public override string ToString()
		{
			return $"module:{this.module}, cmd:{this.command}";
		}
	}
}