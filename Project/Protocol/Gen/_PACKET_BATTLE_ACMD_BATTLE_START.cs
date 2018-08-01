using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 104, 32002 )]
	public class _PACKET_BATTLE_ACMD_BATTLE_START : Packet
	{
		

		public _PACKET_BATTLE_ACMD_BATTLE_START() : base( 104, 32002 )
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