using Core.Net;
using Core.Net.Protocol;

namespace Protocol.Gen
{
	[Packet( 101, 3 )]
	public class _PACKET_USER_QCMD_USER_INFOS : Packet
	{
		

		public _PACKET_USER_QCMD_USER_INFOS() : base( 101, 3 )
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