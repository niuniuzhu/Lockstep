using Core.Misc;
using Core.Net;
using Core.Net.Protocol;
using Protocol.Gen;

namespace Server.Logic
{
	public class SyncTimeHandler : AbsHandler, IHandler
	{
		public void ClientClose( IUserToken token )
		{
		}

		public void ProcessMessage( IUserToken token, Packet packet )
		{
			switch ( packet.command )
			{
				case Command.QCMD_SYNC_TIME:
					long clientTime = ( ( _PACKET_GENERIC_QCMD_SYNC_TIME )packet ).dto.value;
					token.CALL_GENERIC_ACMD_SYNC_TIME( clientTime, TimeUtils.utcTime );
					break;
			}
		}
	}
}
