using Core.Misc;
using Protocol;
using Server.Dao;
using Server.Misc;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Server.Cache
{
	public class RoomCache
	{
		private readonly ConcurrentDictionary<int, Room> _idToModel = new ConcurrentDictionary<int, Room>();
		private readonly ConcurrentInteger _index = new ConcurrentInteger();
		private readonly ConcurrentStack<Room> _pool = new ConcurrentStack<Room>();
		private readonly List<Room> _rooms = new List<Room>();
		private readonly ConcurrentDictionary<string, Room> _uidToRoom = new ConcurrentDictionary<string, Room>();
		public readonly object lockObj = new object();
		public readonly object lockObj2 = new object();

		public PResult Create( string userId, string name, out Room room )
		{
			room = this.GetRoomFromPool();
			room.id = this._index.GetAndAdd();

			if ( !this._idToModel.TryAdd( room.id, room ) )
			{
				this.ReturnRoomToPool( room );
				room = null;
				return PResult.ROOM_CREATE_FAILED;
			}

			lock ( this.lockObj )
			{
				this._rooms.Add( room );
			}
			room.hostId = userId;
			room.name = name;
			room.createTime = ( int )TimeUtils.utcTime;

			Logger.Log( $"玩家{userId}创建房间" );

			return PResult.SUCCESS;
		}

		public PResult Join( Room.Player player, Room room )
		{
			lock ( this.lockObj2 )
			{
				if ( room.teamOne.Count <= room.teamTwo.Count )
					room.teamOne.Add( player );
				else
					room.teamTwo.Add( player );
			}

			this._uidToRoom.TryAdd( player.id, room );

			Logger.Log( $"玩家{player.id}进入房间" );

			return PResult.SUCCESS;
		}

		public PResult Leave( string userId, Room room, out int destroiedRoomId )
		{
			destroiedRoomId = -1;

			Room room2;
			if ( !this._uidToRoom.TryGetValue( userId, out room2 ) )
				return PResult.USER_NOT_IN_ROOM;

			if ( room != room2 )
				return PResult.ROOM_NOT_MATCH;

			if ( !this._uidToRoom.TryRemove( userId, out room2 ) )
				return PResult.USER_NOT_IN_ROOM;

			lock ( this.lockObj2 )
			{
				room.RemoveUser( userId );

				if ( room.teamOne.Count + room.teamTwo.Count == 0 )
				{
					if ( this._idToModel.TryRemove( room.id, out room ) )
					{
						destroiedRoomId = room.id;

						lock ( this.lockObj )
						{
							this._rooms.Remove( room );
						}
						this.ReturnRoomToPool( room );
					}
				}
			}

			Logger.Log( $"玩家{userId}离开房间" );

			return PResult.SUCCESS;
		}

		public PResult ChangeMap( Room room, string mapId )
		{
			if ( !this.IsMapIdValid( mapId ) )
				return PResult.CHANGE_MAP_FAILED;

			room.map = mapId;
			return PResult.SUCCESS;
		}

		public PResult ChangeHero( Room room, string userId, string heroId )
		{
			if ( !this.IsHeroIdValid( heroId ) )
				return PResult.CHANGE_MAP_FAILED;

			if ( !room.ContainsUser( userId ) )
				return PResult.USER_NOT_IN_ROOM;

			lock ( this.lockObj2 )
			{
				room.SetHero( userId, heroId );
			}
			return PResult.SUCCESS;
		}

		public PResult ChangeTeam( Room room, string userId, byte teamId )
		{
			int result;
			lock ( this.lockObj2 )
			{
				result = room.ChangeTeam( userId, teamId );
			}
			if ( result == -1 )
				return PResult.USER_IN_TEAM;
			if ( result == -2 )
				return PResult.TEAM_FULL;

			return PResult.SUCCESS;
		}

		public PResult ChangeModel( Room room, string userId, byte modelId )
		{
			room.ChangeModel( userId, modelId );
			return PResult.SUCCESS;
		}

		public PResult ChangeSkin( Room room, string userId, byte skinId )
		{
			room.ChangeSkin( userId, skinId );
			return PResult.SUCCESS;
		}

		private bool IsMapIdValid( string mapId )
		{
			//todo 验证地图id是否有效
			return true;
		}

		private bool IsHeroIdValid( string mapId )
		{
			//todo 验证英雄id是否有效
			return true;
		}

		public bool IsUserReady( Room room, string userId )
		{
			return room.IsUserReady( userId );
		}

		public PResult BeginFight( Room room )
		{
			room.fightLock = true;
			return PResult.SUCCESS;
		}

		public PResult SetReady( Room room, string userId )
		{
			if ( !room.SetReady( userId ) )
				return PResult.USER_READY;

			return PResult.SUCCESS;
		}

		public PResult CancelReady( Room room, string userId )
		{
			if ( !room.CancelReady( userId ) )
				return PResult.USER_NOT_READY;

			return PResult.SUCCESS;
		}

		public PResult MapReady( Room room, string userId )
		{
			++room.mapReadyCount;
			return PResult.SUCCESS;
		}

		public PResult Get( int roomId, out Room room )
		{
			if ( !this._idToModel.TryGetValue( roomId, out room ) )
				return PResult.ROOM_NOR_FOUND;
			return PResult.SUCCESS;
		}

		public bool IsUserInRoom( string userId )
		{
			return this._uidToRoom.ContainsKey( userId );
		}

		public PResult GetUserRoom( string userId, out Room room )
		{
			if ( !this._uidToRoom.TryGetValue( userId, out room ) )
				return PResult.ROOM_NOR_FOUND;
			return PResult.SUCCESS;
		}

		public Room[] GetRoomList( int from, int length )
		{
			Room[] result;
			lock ( this.lockObj )
			{
				int count = this._rooms.Count;
				from = from < 0 ? 0 : ( from > count ? count : from );
				if ( length < 0 || length > count )
					length = count;

				int to = from + length;
				result = new Room[length];
				for ( int i = from; i < to; i++ )
					result[i - from] = this._rooms[count - i - 1];
			}
			return result;
		}

		public bool IsAllUserReady( Room room )
		{
			return room.IsAllUserReady();
		}

		public bool IsAllMapReady( Room room )
		{
			//todo 可能在载入途中断开了,检查连接是否还存在
			return room.teamOne.Count + room.teamTwo.Count == room.mapReadyCount;
		}

		private Room GetRoomFromPool()
		{
			if ( this._pool.Count == 0 )
				return new Room();
			Room room;
			this._pool.TryPop( out room );
			return room;
		}

		private void ReturnRoomToPool( Room room )
		{
			room.Reset();
			this._pool.Push( room );
		}
	}
}