using Core.Net;
using Core.Net.Protocol;
using Protocol;
using Protocol.Gen;
using Server.Dao;
using Server.Misc;

namespace Server.Logic
{
	public class UserHandler : AbsHandler, IHandler
	{
		public void ClientClose( IUserToken token )
		{
			this._userBiz.Offline( token );
			this._userBiz.Logout( token );
		}

		public void ProcessMessage( IUserToken token, Packet packet )
		{
			switch ( packet.command )
			{
				case Command.QCMD_LOGIN:
					this.Login( token, ( ( _PACKET_USER_QCMD_LOGIN )packet ).dto );
					break;

				case Command.QCMD_REG:
					this.Reg( token, ( ( _PACKET_USER_QCMD_REG )packet ).dto );
					break;

				case Command.QCMD_CREATE_USER:
					if ( !this.CheckAndReplyAccountOnline( token, packet ) )
						return;
					this.Create( token, ( ( _PACKET_USER_QCMD_CREATE_USER )packet ).dto );
					break;

				case Command.QCMD_USER_INFOS:
					if ( !this.CheckAndReplyAccountOnline( token, packet ) )
						return;
					this.Info( token );
					break;

				case Command.QCMD_USER_ONLINE:
					if ( !this.CheckAndReplyAccountOnline( token, packet ) )
						return;
					this.Online( token );
					break;
			}
		}

		private void Login( IUserToken token, _DTO_account dto )
		{
			ExecutorPool.instance.Execute( () =>
			{
				PResult result = this._userBiz.Login( token, dto.account, dto.password );
				this.Reply( token, Module.USER, Command.QCMD_LOGIN, result );
			} );
		}

		private void Reg( IUserToken token, _DTO_account dto )
		{
			ExecutorPool.instance.Execute( () =>
			{
				PResult result = this._userBiz.Reg( token, dto.account, dto.password );
				this.Reply( token, Module.USER, Command.QCMD_REG, result );
			} );
		}

		private void Create( IUserToken token, _DTO_string dto )
		{
			ExecutorPool.instance.Execute(
				() =>
				{
					PResult result = this._userBiz.Create( token, dto.value, out _ );
					this.Reply( token, Module.USER, Command.QCMD_CREATE_USER, result );
				} );
		}

		private void Info( IUserToken token )
		{
			ExecutorPool.instance.Execute(
				() =>
				{
					PResult e = this._userBiz.GetByAccount( token, out User user );
					if ( e != PResult.SUCCESS )
						this.Reply( token, Module.USER, Command.QCMD_USER_INFOS, e );
					else
						token.CALL_USER_ACMD_USER_INFOS( DTOHelper.GetUserInfo( user ) );
				} );
		}

		private void Online( IUserToken token )
		{
			ExecutorPool.instance.Execute(
				() =>
				{
					PResult result = this._userBiz.Online( token );
					if ( result != PResult.SUCCESS )
						this.Reply( token, Module.USER, Command.QCMD_USER_ONLINE, result );
					else
					{
						result = this._userBiz.GetByAccount( token, out User user );
						if ( result != PResult.SUCCESS )
							this.Reply( token, Module.USER, Command.QCMD_USER_INFOS, result );
						else
							token.CALL_USER_ACMD_USER_ONLINE( DTOHelper.GetUserInfo( user ) );
					}
				} );
		}
	}
}