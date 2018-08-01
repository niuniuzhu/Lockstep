using Protocol.Gen;
using Server.Dao;

namespace Server.Misc
{
	public static class DTOHelper
	{
		public static _DTO_charactor GetUserInfo( User user )
		{
			_DTO_charactor dto = ProtocolManager.DTO_charactor( user.name, user.id );
			return dto;
		}

		public static _DTO_begin_fight GetRoomInfoDTO2( Room room )
		{
			int count = room.teamOne.Count;
			int count2 = room.teamTwo.Count;
			var players = new _DTO_player_info[count + count2];
			for ( int i = 0; i < count; i++ )
			{
				Room.Player player = room.teamOne[i];
				players[i] = ProtocolManager.DTO_player_info( player.hero, player.name, player.ready, ( byte )( player.model << 4 | player.skin ), 0, player.id );
			}
			for ( int i = 0; i < count2; i++ )
			{
				Room.Player player = room.teamTwo[i];
				players[count + i] = ProtocolManager.DTO_player_info( player.hero, player.name, player.ready, ( byte )( player.model << 4 | player.skin ), 1, player.id );
			}
			return ProtocolManager.DTO_begin_fight( room.hostId, room.map, room.name, players, room.id );
		}

		public static _DTO_room_info_detail GetRoomInfoDTO( Room room )
		{
			int count = room.teamOne.Count;
			int count2 = room.teamTwo.Count;
			_DTO_player_info[] players = new _DTO_player_info[count + count2];
			for ( int i = 0; i < count; i++ )
			{
				Room.Player player = room.teamOne[i];
				players[i] = ProtocolManager.DTO_player_info( player.hero, player.name, player.ready, ( byte )( player.model << 4 | player.skin ), 0, player.id );
			}
			for ( int i = 0; i < count2; i++ )
			{
				Room.Player player = room.teamTwo[i];
				players[count + i] = ProtocolManager.DTO_player_info( player.hero, player.name, player.ready, ( byte )( player.model << 4 | player.skin ), 1, player.id );
			}
			return ProtocolManager.DTO_room_info_detail( room.hostId, room.map, room.name, players, room.id );
		}

		public static _DTO_room_info GetRoomDTO( Room room )
		{
			return ProtocolManager.DTO_room_info( room.createTime, room.map, room.name, room.id );
		}

		public static _DTO_player_info[] GetPlayerInfoInRoom( Room room )
		{
			int count = room.teamOne.Count;
			int count2 = room.teamTwo.Count;
			var dtos = new _DTO_player_info[count + count2];
			for ( int i = 0; i < count; i++ )
			{
				Room.Player player = room.teamOne[i];
				dtos[i] = ProtocolManager.DTO_player_info( player.hero, player.name, player.ready, ( byte )( player.model << 4 | player.skin ), 0, player.id );
			}
			for ( int i = 0; i < count2; i++ )
			{
				Room.Player player = room.teamTwo[i];
				dtos[count + i] = ProtocolManager.DTO_player_info( player.hero, player.name, player.ready, ( byte )( player.model << 4 | player.skin ), 1, player.id );
			}
			return dtos;
		}
	}
}