using Core.Net;
using Server.Biz;
using System.Collections.Generic;
using Core.Net.Protocol;

namespace Server
{
	public static class SendHelper
	{
		public static void Brocast( List<string> users, Packet packet, IUserToken except = null )
		{
			int count = users.Count;
			if ( count <= 0 )
				return;

			for ( int i = 0; i < count; i++ )
			{
				string user = users[i];
				IUserToken token = BizFactory.USER_BIZ.GetToken( user );
				if ( token == null || token == except )
					continue;
				token.Send( packet );//todo
			}
		}
	}
}