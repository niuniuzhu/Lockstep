using Server.Dao;
using Protocol;
using System.Collections.Generic;
using Core.Net;
using Core.Net.Protocol;
using Protocol.Gen;
using Server.Misc;

namespace Server.Logic
{
	public class RoomHandler : AbsHandler, IHandler
	{
		public void ClientClose( IUserToken token )
		{
			User user = this._userBiz.GetUser( token );
			if ( user == null )
				return;

			if ( !this._roomBiz.IsUserInRoom( user.id ) )
				return;

			List<string> kickedUsers = new List<string>();
			this._roomBiz.GetUserRoom( user.id, out Room room );
			PResult result = this._roomBiz.Leave( user.id, kickedUsers, out int destroiedRoomId );
			if ( result == PResult.SUCCESS )
			{
				int count = kickedUsers.Count;
				for ( int i = 0; i < count; i++ )
				{
					IUserToken mToken = this._userBiz.GetToken( kickedUsers[i] );
					mToken.CALL_ROOM_ACMD_LEAVE_ROOM();
				}

				this.BrocastRoomInfo( room );

				if ( destroiedRoomId != -1 )
					this._hallBiz.Brocast( ProtocolManager.PACKET_HALL_ACMD_BRO_ROOM_DESTROIED( destroiedRoomId ) );
			}
		}

		public void ProcessMessage( IUserToken token, Packet packet )
		{
			if ( !this.CheckAndReplyAccountOnline( token, packet ) )
				return;

			if ( !this.CheckAndReplyUserOnline( token, packet ) )
				return;

			switch ( packet.command )
			{
				case Command.QCMD_LEAVE_ROOM:
					this.Leave( token );
					break;

				case Command.QCMD_ROOM_INFO:
					this.RoomInfo( token, ( ( _PACKET_ROOM_QCMD_ROOM_INFO )packet ).dto );
					break;

				case Command.QCMD_CHANGE_MAP:
					this.ChangeMap( token, ( ( _PACKET_ROOM_QCMD_CHANGE_MAP )packet ).dto );
					break;

				case Command.QCMD_CHANGE_HERO:
					this.ChangeHero( token, ( ( _PACKET_ROOM_QCMD_CHANGE_HERO )packet ).dto );
					break;

				case Command.QCMD_CHANGE_TEAM:
					this.ChangeTeam( token, ( ( _PACKET_ROOM_QCMD_CHANGE_TEAM )packet ).dto );
					break;

				case Command.QCMD_CHANGE_MODEL:
					this.ChangeModel( token, ( ( _PACKET_ROOM_QCMD_CHANGE_MODEL )packet ).dto );
					break;

				case Command.QCMD_CHANGE_SKIN:
					this.ChangeSkin( token, ( ( _PACKET_ROOM_QCMD_CHANGE_SKIN )packet ).dto );
					break;

				case Command.QCMD_BEGIN_FIGHT:
					this.BeginFight( token );
					break;

				case Command.QCMD_ADD_FIGHT_READY:
					this.AddFightReady( token );
					break;

				case Command.QCMD_REMOVE_FIGHT_READY:
					this.RemoveFightReady( token );
					break;

				case Command.QCMD_MAP_READY:
					this.MapReady( token );
					break;
			}
		}

		private void Leave( IUserToken token )
		{
			string userId = this._userBiz.GetUser( token ).id;

			List<string> kickedUsers = new List<string>();
			this._roomBiz.GetUserRoom( userId, out Room room );
			PResult result = this._roomBiz.Leave( userId, kickedUsers, out int destroiedRoomId );
			if ( result == PResult.SUCCESS )
			{
				int count = kickedUsers.Count;
				for ( int i = 0; i < count; i++ )
				{
					IUserToken mToken = this._userBiz.GetToken( kickedUsers[i] );
					mToken.CALL_ROOM_ACMD_LEAVE_ROOM();
				}

				this.BrocastRoomInfo( room );
			}

			this.Reply( token, Module.ROOM, Command.QCMD_LEAVE_ROOM, result );

			if ( result == PResult.SUCCESS && destroiedRoomId != -1 )
				this._hallBiz.Brocast( ProtocolManager.PACKET_HALL_ACMD_BRO_ROOM_DESTROIED( destroiedRoomId ) );
		}

		private void RoomInfo( IUserToken token, _DTO_int dto )
		{
			Room room;
			PResult result = dto.value == -1 ? this._roomBiz.GetUserRoom( token, out room ) : this._roomBiz.Get( dto.value, out room );

			if ( result != PResult.SUCCESS )
				this.Reply( token, Module.ROOM, Command.QCMD_ROOM_INFO, result );
			else
				this.RoomInfo( token, room );
		}

		private void RoomInfo( IUserToken token, Room room )
		{
			token.CALL_ROOM_ACMD_ROOM_INFO( DTOHelper.GetRoomInfoDTO( room ) );
		}

		private void ChangeMap( IUserToken token, _DTO_string dto )
		{
			PResult result = this._roomBiz.GetUserRoom( token, out Room room );
			if ( result != PResult.SUCCESS )
			{
				this.Reply( token, Module.ROOM, Command.QCMD_CHANGE_MAP, result );
				return;
			}
			User user = this._userBiz.GetUser( token );
			result = this._roomBiz.ChangeMap( room, user.id, dto.value );
			if ( result != PResult.SUCCESS )
				this.Reply( token, Module.ROOM, Command.QCMD_CHANGE_MAP, result );
			else
				this.BrocastRoomInfo( room );
		}

		private void ChangeHero( IUserToken token, _DTO_string dto )
		{
			PResult result = this._roomBiz.GetUserRoom( token, out Room room );
			if ( result != PResult.SUCCESS )
			{
				this.Reply( token, Module.ROOM, Command.QCMD_CHANGE_HERO, result );
				return;
			}
			User user = this._userBiz.GetUser( token );
			result = this._roomBiz.ChangeHero( room, user.id, dto.value );
			this.Reply( token, Module.ROOM, Command.QCMD_CHANGE_HERO, result );
		}

		private void ChangeTeam( IUserToken token, _DTO_byte dto )
		{
			PResult result = this._roomBiz.GetUserRoom( token, out Room room );
			if ( result != PResult.SUCCESS )
			{
				this.Reply( token, Module.ROOM, Command.QCMD_CHANGE_TEAM, result );
				return;
			}
			User user = this._userBiz.GetUser( token );
			result = this._roomBiz.ChangeTeam( room, user.id, dto.value );
			if ( result != PResult.SUCCESS )
				this.Reply( token, Module.ROOM, Command.QCMD_CHANGE_TEAM, result );
			else
				this.BrocastRoomInfo( room );
		}

		private void ChangeModel( IUserToken token, _DTO_byte dto )
		{
			PResult result = this._roomBiz.GetUserRoom( token, out Room room );
			if ( result != PResult.SUCCESS )
			{
				this.Reply( token, Module.ROOM, Command.QCMD_CHANGE_MODEL, result );
				return;
			}
			User user = this._userBiz.GetUser( token );
			result = this._roomBiz.ChangeModel( room, user.id, dto.value );
			if ( result != PResult.SUCCESS )
				this.Reply( token, Module.ROOM, Command.QCMD_CHANGE_MODEL, result );
			else
				this.BrocastRoomInfo( room );
		}

		private void ChangeSkin( IUserToken token, _DTO_byte dto )
		{
			PResult result = this._roomBiz.GetUserRoom( token, out Room room );
			if ( result != PResult.SUCCESS )
			{
				this.Reply( token, Module.ROOM, Command.QCMD_CHANGE_SKIN, result );
				return;
			}
			User user = this._userBiz.GetUser( token );
			result = this._roomBiz.ChangeSkin( room, user.id, dto.value );
			if ( result != PResult.SUCCESS )
				this.Reply( token, Module.ROOM, Command.QCMD_CHANGE_SKIN, result );
			else
				this.BrocastRoomInfo( room );
		}

		private void BrocastRoomInfo( Room room, IUserToken except = null )
		{
			this._roomBiz.Brocast( room, ProtocolManager.PACKET_ROOM_ACMD_ROOM_INFO( DTOHelper.GetRoomInfoDTO( room ) ), except );
		}

		private void BeginFight( IUserToken token )
		{
			PResult result = this._roomBiz.GetUserRoom( token, out Room room );
			if ( result != PResult.SUCCESS )
			{
				this.Reply( token, Module.ROOM, Command.QCMD_BEGIN_FIGHT, result );
				return;
			}
			result = this._roomBiz.BeginFight( room, this._userBiz.GetUser( token ).id );
			if ( result == PResult.SUCCESS )
				this._roomBiz.Brocast( room, ProtocolManager.PACKET_ROOM_ACMD_BEGIN_FIGHT( DTOHelper.GetRoomInfoDTO2( room ) ) );
			else
				this.Reply( token, Module.ROOM, Command.QCMD_BEGIN_FIGHT, result );
		}

		private void AddFightReady( IUserToken token )
		{
			PResult result = this._roomBiz.GetUserRoom( token, out Room room );
			if ( result != PResult.SUCCESS )
			{
				this.Reply( token, Module.ROOM, Command.QCMD_ADD_FIGHT_READY, result );
				return;
			}
			result = this._roomBiz.SetReady( room, this._userBiz.GetUser( token ).id );
			if ( result != PResult.SUCCESS )
			{
				this.Reply( token, Module.ROOM, Command.QCMD_ADD_FIGHT_READY, result );
				return;
			}
			this.BrocastRoomInfo( room );
		}

		private void RemoveFightReady( IUserToken token )
		{
			PResult result = this._roomBiz.GetUserRoom( token, out Room room );
			if ( result != PResult.SUCCESS )
			{
				this.Reply( token, Module.ROOM, Command.QCMD_REMOVE_FIGHT_READY, result );
				return;
			}
			result = this._roomBiz.CancelReady( room, this._userBiz.GetUser( token ).id );
			if ( result != PResult.SUCCESS )
			{
				this.Reply( token, Module.ROOM, Command.QCMD_REMOVE_FIGHT_READY, result );
				return;
			}
			this.BrocastRoomInfo( room );
		}

		private void MapReady( IUserToken token )
		{
			PResult result = this._roomBiz.GetUserRoom( token, out Room room );
			if ( result != PResult.SUCCESS )
			{
				this.Reply( token, Module.ROOM, Command.QCMD_MAP_READY, result );
				return;
			}

			result = this._roomBiz.MapReady( room, this._userBiz.GetUser( token ).id );
			if ( result != PResult.SUCCESS )
			{
				this.Reply( token, Module.ROOM, Command.QCMD_MAP_READY, result );
				return;
			}
			this.BrocastRoomInfo( room );
			this._roomBiz.EnterBattle( room );
		}
	}
}