using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 104, 0 )]
	public class _PACKET_BATTLE_QCMD_BATTLE_CREATED : Packet
	{
		

		public _PACKET_BATTLE_QCMD_BATTLE_CREATED() : base( 104, 0 )
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