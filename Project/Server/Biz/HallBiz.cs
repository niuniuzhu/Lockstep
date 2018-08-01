using Core.Net;
using Protocol;
using Server.Cache;
using Server.Dao;
using System.Collections.Generic;
using Core.Net.Protocol;

namespace Server.Biz
{
	public class HallBiz
	{
		public PResult Enter( string userId )
		{
			PResult result = CacheFactory.HALL_CACHE.Enter( userId );
			return result;
		}

		public PResult Leave( string userId )
		{
			PResult result = CacheFactory.HALL_CACHE.Leave( userId );
			return result;
		}

		public bool Exist( string userId )
		{
			return CacheFactory.HALL_CACHE.Exist( userId );
		}

		public Room[] GetRoomList( int from, int length )
		{
			return BizFactory.ROOM_BIZ.GetRoomList( from, length );
		}

		public void Brocast( Packet packet, IUserToken except = null )
		{
			List<string> users = new List<string>();
			CacheFactory.HALL_CACHE.ForeachUser( user =>
			{
				users.Add( user );
			} );
			SendHelper.Brocast( users, packet, except );
		}
	}
}