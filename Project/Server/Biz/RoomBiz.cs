using Core.Net;
using Protocol;
using Server.Cache;
using Server.Dao;
using System.Collections.Generic;
using Core.Net.Protocol;

namespace Server.Biz
{
	public class RoomBiz
	{
		public PResult Create( string userId, string name, out Room room )
		{
			PResult result = CacheFactory.ROOM_CACHE.Create( userId, name, out room );
			return result;
		}

		public PResult Destroy( Room room )
		{
			//把房主踢出就可以了
			return this.Leave( room.hostId, null, out _ );
		}

		public PResult CanEnter( string userId, Room room )
		{
			if ( room.fightLock )
				return PResult.ROOM_LOCKED;

			if ( this.IsUserInRoom( userId ) ) //玩家已在某个房间中
				return PResult.USER_IN_ROOM;

			if ( Room.TEAM_MAX * 2 <= room.teamOne.Count + room.teamTwo.Count ) //满人了
				return PResult.ROOM_FULL;

			return PResult.SUCCESS;
		}

		public PResult Join( string userId, Room room )
		{
			PResult result = this.CanEnter( userId, room );
			if ( result != PResult.SUCCESS )
				return result;

			result = BizFactory.HALL_BIZ.Leave( userId );
			if ( result != PResult.SUCCESS )
				return result;

			User user = BizFactory.USER_BIZ.GetUser( userId );
			Room.Player player = new Room.Player();
			player.id = userId;
			player.name = user.name;
			player.hero = "h0";
			//if ( userId == room.hostId )
			//	player.ready = true;
			result = CacheFactory.ROOM_CACHE.Join( player, room );

			return result;
		}

		public PResult Leave( string userId, List<string> kickedUsers, out int destroiedRoomId )
		{
			destroiedRoomId = -1;

			PResult result = this.GetUserRoom( userId, out Room room );
			if ( result != PResult.SUCCESS )
				return result;

			if ( room.fightLock )
				return PResult.ROOM_LOCKED;

			result = CacheFactory.ROOM_CACHE.Leave( userId, room, out destroiedRoomId );
			if ( result == PResult.SUCCESS )
			{
				BizFactory.HALL_BIZ.Enter( userId );

				if ( room.hostId == userId ) //房主离开房间，则把所有玩家都踢出房间
				{
					int count = room.teamOne.Count;
					for ( int i = 0; i < count; ++i )
					{
						string id = room.teamOne[i].id;
						if ( CacheFactory.ROOM_CACHE.Leave( id, room, out destroiedRoomId ) == PResult.SUCCESS )
						{
							BizFactory.HALL_BIZ.Enter( id );
							kickedUsers?.Add( id );
							--i;
							--count;
						}
					}

					count = room.teamTwo.Count;
					for ( int i = 0; i < count; ++i )
					{
						string id = room.teamTwo[i].id;
						if ( CacheFactory.ROOM_CACHE.Leave( id, room, out destroiedRoomId ) == PResult.SUCCESS )
						{
							BizFactory.HALL_BIZ.Enter( id );
							kickedUsers?.Add( id );
							--i;
							--count;
						}
					}
				}
			}
			return result;
		}

		public bool IsAllMapReady( Room room )
		{
			return CacheFactory.ROOM_CACHE.IsAllMapReady( room );
		}

		public bool IsUserInRoom( string userId )
		{
			return CacheFactory.ROOM_CACHE.IsUserInRoom( userId );
		}

		public PResult Get( int roomId, out Room room )
		{
			return CacheFactory.ROOM_CACHE.Get( roomId, out room );
		}

		public PResult GetUserRoom( IUserToken token, out Room room )
		{
			User user = BizFactory.USER_BIZ.GetUser( token );
			return this.GetUserRoom( user.id, out room );
		}

		public PResult GetUserRoom( string userId, out Room room )
		{
			return CacheFactory.ROOM_CACHE.GetUserRoom( userId, out room );
		}

		public Room[] GetRoomList( int from, int length )
		{
			return CacheFactory.ROOM_CACHE.GetRoomList( from, length );
		}

		public PResult ChangeMap( Room room, string userId, string mapId )
		{
			if ( room.fightLock )
				return PResult.ROOM_LOCKED;

			if ( room.hostId != userId )
				return PResult.ROOM_NO_PERMISSIONS;

			return CacheFactory.ROOM_CACHE.ChangeMap( room, mapId );
		}

		public PResult ChangeHero( Room room, string userId, string mapId )
		{
			if ( room.fightLock )
				return PResult.ROOM_LOCKED;

			if ( CacheFactory.ROOM_CACHE.IsUserReady( room, userId ) )
				return PResult.USER_READY;

			return CacheFactory.ROOM_CACHE.ChangeHero( room, userId, mapId );
		}

		public PResult ChangeTeam( Room room, string userId, byte teamId )
		{
			if ( room.fightLock )
				return PResult.ROOM_LOCKED;

			if ( CacheFactory.ROOM_CACHE.IsUserReady( room, userId ) )
				return PResult.USER_READY;

			return CacheFactory.ROOM_CACHE.ChangeTeam( room, userId, teamId );
		}

		public PResult ChangeModel( Room room, string userId, byte modelId )
		{
			if ( room.fightLock )
				return PResult.ROOM_LOCKED;

			if ( CacheFactory.ROOM_CACHE.IsUserReady( room, userId ) )
				return PResult.USER_READY;

			return CacheFactory.ROOM_CACHE.ChangeModel( room, userId, modelId );
		}

		public PResult ChangeSkin( Room room, string userId, byte skinId )
		{
			if ( room.fightLock )
				return PResult.ROOM_LOCKED;

			if ( CacheFactory.ROOM_CACHE.IsUserReady( room, userId ) )
				return PResult.USER_READY;

			return CacheFactory.ROOM_CACHE.ChangeSkin( room, userId, skinId );
		}

		public PResult SetReady( Room room, string userId )
		{
			if ( room.fightLock )
				return PResult.ROOM_LOCKED;

			return CacheFactory.ROOM_CACHE.SetReady( room, userId );
		}

		public PResult CancelReady( Room room, string userId )
		{
			if ( room.fightLock )
				return PResult.ROOM_LOCKED;

			return CacheFactory.ROOM_CACHE.CancelReady( room, userId );
		}

		public PResult MapReady( Room room, string userId )
		{
			return CacheFactory.ROOM_CACHE.MapReady( room, userId );
		}

		public PResult BeginFight( Room room, string userId )
		{
			if ( room.fightLock )
				return PResult.ROOM_LOCKED;

			if ( room.hostId != userId )
				return PResult.ROOM_NO_PERMISSIONS;

			//if ( room.teamOne.Count == 0 || room.teamTwo.Count == 0 )
			//	return PResult.NOT_ENOUGTH_USERS;

			if ( !CacheFactory.ROOM_CACHE.IsAllUserReady( room ) )
				return PResult.NOT_ALL_USER_READY;

			return CacheFactory.ROOM_CACHE.BeginFight( room );
		}

		public void EnterBattle( Room room )
		{
			if ( !this.IsAllMapReady( room ) )
				return;

			PResult result = BizFactory.BATTLE_BIZ.Create( room );
			if ( result != PResult.SUCCESS )
				return;
			room.fightLock = false;
			this.Destroy( room );
		}

		private void BrocastTeam( Room room, int teamIndex, Packet packet,
								  IUserToken except = null )
		{
			List<Room.Player> team = teamIndex == 0 ? room.teamOne : room.teamTwo;
			if ( team.Count == 0 )
				return;

			List<string> users = new List<string>();
			lock ( CacheFactory.ROOM_CACHE.lockObj2 )
			{
				int count = team.Count;
				for ( int i = 0; i < count; i++ )
					users.Add( team[i].id );
			}
			SendHelper.Brocast( users, packet, except );
		}

		public void Brocast( Room room, Packet packet, IUserToken except = null )
		{
			this.BrocastTeam( room, 0, packet, except );
			this.BrocastTeam( room, 1, packet, except );
		}
	}
}