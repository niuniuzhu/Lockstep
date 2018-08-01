using Core.Net;
using Core.Net.Protocol;
using Protocol;
using Protocol.Gen;
using Server.Biz;
using Server.Dao;
using Server.Misc;

namespace Server.Logic
{
	public class HallHandler : AbsHandler, IHandler
	{
		public void ClientClose( IUserToken token )
		{
			User user = this._userBiz.GetUser( token );
			if ( user == null )
				return;

			if ( !this._hallBiz.Exist( user.id ) )
				return;

			this._hallBiz.Leave( user.id );
		}

		public void ProcessMessage( IUserToken token, Packet packet )
		{
			if ( !this.CheckAndReplyAccountOnline( token, packet ) ) //账号还没有登录，忽略所有数据
				return;

			if ( !this.CheckAndReplyUserOnline( token, packet ) ) //账号还没有登录，忽略所有数据
				return;

			switch ( packet.command )
			{
				case Command.QCMD_ROOM_LIST:
					{
						_DTO_request_room_list sdto = ( ( _PACKET_HALL_QCMD_ROOM_LIST )packet ).dto;
						_DTO_room_info[] rooms = this.GetRoomDTOList( sdto.from, sdto.count );
						token.CALL_HALL_ACMD_ROOM_LIST( rooms );
					}
					break;

				case Command.QCMD_CREATE_ROOM:
					{
						_DTO_string sdto = ( ( _PACKET_HALL_QCMD_CREATE_ROOM )packet ).dto;
						this.CreateRoom( token, sdto.value );
					}
					break;

				case Command.QCMD_JOIN_ROOM:
					{
						_DTO_int sdto = ( ( _PACKET_HALL_QCMD_JOIN_ROOM )packet ).dto;
						this.JoinRoom( token, sdto.value );
					}
					break;
			}
		}

		private _DTO_room_info[] GetRoomDTOList( int from, int length )
		{
			Room[] rooms = this._hallBiz.GetRoomList( from, length );
			int count = rooms.Length;
			var roomDTOs = new _DTO_room_info[count];
			for ( int i = 0; i < count; i++ )
				roomDTOs[i] = DTOHelper.GetRoomDTO( rooms[i] );
			return roomDTOs;
		}

		private void CreateRoom( IUserToken token, string name )
		{
			string userId = this._userBiz.GetUser( token ).id;
			PResult result = this._roomBiz.Create( userId, name, out Room room );
			this.Reply( token, Module.HALL, Command.QCMD_CREATE_ROOM, result );
			if ( result == PResult.SUCCESS )
			{
				BizFactory.HALL_BIZ.Brocast( ProtocolManager.PACKET_HALL_ACMD_BRO_ROOM_CREATED( DTOHelper.GetRoomDTO( room ) ) );
				//把玩家放进房间
				result = this._roomBiz.Join( userId, room );
				if ( result != PResult.SUCCESS )
					this.Reply( token, Module.HALL, Command.QCMD_CREATE_ROOM, result );
				else
					token.CALL_HALL_ACMD_JOIN_ROOM( room.id );
			}
		}

		private void JoinRoom( IUserToken token, int roomId )
		{
			string userId = this._userBiz.GetUser( token ).id;
			PResult result = this._roomBiz.Get( roomId, out Room room );
			if ( result != PResult.SUCCESS )
				this.Reply( token, Module.HALL, Command.QCMD_JOIN_ROOM, result );
			else
			{
				result = this._roomBiz.Join( userId, room );
				if ( result == PResult.SUCCESS )
				{
					token.CALL_HALL_ACMD_JOIN_ROOM( room.id );
					this._roomBiz.Brocast( room, ProtocolManager.PACKET_ROOM_ACMD_ROOM_INFO( DTOHelper.GetRoomInfoDTO( room ) ), token );
				}
			}
		}
	}
}