using Core.Net;
using Core.Net.Protocol;

namespace Server.Logic
{
	public interface IHandler
	{
		void ClientClose( IUserToken token );

		void ProcessMessage( IUserToken token, Packet packet );
	}
}