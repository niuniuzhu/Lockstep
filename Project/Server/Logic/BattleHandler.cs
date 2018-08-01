using Core.Net;
using Core.Net.Protocol;
using Protocol;
using Protocol.Gen;
using Server.Dao;
using Server.Logic.Battle;

namespace Server.Logic
{
	public class BattleHandler : AbsHandler, IHandler
	{
		public void ClientClose( IUserToken token )
		{
			User user = this._userBiz.GetUser( token );
			if ( user == null )
				return;

			if ( !this._battleBiz.UserInBattle( user.id ) )
				return;

			this._battleBiz.Leave( user.id );
		}

		public void ProcessMessage( IUserToken token, Packet packet )
		{
			if ( !this.CheckAndReplyAccountOnline( token, packet ) ) //账号还没有登录，忽略所有数据
				return;

			if ( !this.CheckAndReplyUserOnline( token, packet ) ) //账号还没有登录，忽略所有数据
				return;

			switch ( packet.command )
			{
				case Command.QCMD_BATTLE_CREATED:
					this.HandleBattleCreated( token );
					break;

				case Command.QCMD_ACTION:
					this.HandleAction( token, ( ( _PACKET_BATTLE_QCMD_ACTION )packet ).dto );
					break;

				case Command.QCMD_LEAVE_BATTLE:
					this.HandleLeaveBattle( token, packet );
					break;

				case Command.QCMD_END_BATTLE:
					this.HandleEndBattle( token, ( ( _PACKET_BATTLE_QCMD_END_BATTLE )packet ).dto );
					break;
			}
		}

		private void HandleBattleCreated( IUserToken token )
		{
			User user = this._userBiz.GetUser( token );
			if ( user == null )
				return;

			if ( !this._battleBiz.UserInBattle( user.id ) )
				return;

			PResult result = this._battleBiz.HandleBattleCreated( user.id );
			if ( result == PResult.SUCCESS )
			{
				StepLocker stepLocker = this._battleBiz.GetFromUserId( user.id );
				stepLocker.Brocast( ProtocolManager.PACKET_BATTLE_ACMD_BATTLE_START() );
			}
		}

		private void HandleAction( IUserToken token, _DTO_frame_info dto )
		{
			User user = this._userBiz.GetUser( token );
			if ( user == null )
				return;

			if ( !this._battleBiz.UserInBattle( user.id ) )
				return;

			PResult result = this._battleBiz.HandleAction( user.id, dto );
			if ( result != PResult.SUCCESS )
				this.Reply( token, Module.BATTLE, Command.QCMD_ACTION, result );
		}

		private void HandleLeaveBattle( IUserToken token, Packet model )
		{
			User user = this._userBiz.GetUser( token );
			if ( user == null )
				return;

			if ( !this._battleBiz.UserInBattle( user.id ) )
				return;

			PResult result = this._battleBiz.Leave( user.id );
			if ( result == PResult.SUCCESS )
			{
				this._hallBiz.Enter( user.id );
			}
			this.Reply( token, Module.BATTLE, Command.QCMD_LEAVE_BATTLE, result );
		}

		private void HandleEndBattle( IUserToken token, _DTO_byte dto )
		{
			User user = this._userBiz.GetUser( token );
			if ( user == null )
				return;

			if ( !this._battleBiz.UserInBattle( user.id ) )
				return;

			PResult result = this._battleBiz.HandleEndBattle( user.id );
			if ( result == PResult.SUCCESS )
			{
				StepLocker stepLocker = this._battleBiz.GetFromUserId( user.id );
				stepLocker.Brocast( ProtocolManager.PACKET_BATTLE_ACMD_BATTLE_END( dto.value ) );//todo 信任任何一个客户端带来的胜利消息
			}
		}
	}
}