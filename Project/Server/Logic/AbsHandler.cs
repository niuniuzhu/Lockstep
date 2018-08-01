using Core.Net;
using Core.Net.Protocol;
using Protocol;
using Protocol.Gen;
using Server.Biz;

namespace Server.Logic
{
	public class AbsHandler
	{
		protected readonly UserBiz _userBiz = BizFactory.USER_BIZ;
		protected readonly RoomBiz _roomBiz = BizFactory.ROOM_BIZ;
		protected readonly HallBiz _hallBiz = BizFactory.HALL_BIZ;
		protected readonly BattleBiz _battleBiz = BizFactory.BATTLE_BIZ;

		protected bool CheckAndReplyAccountOnline( IUserToken token, Packet model )
		{
			if ( !BizFactory.USER_BIZ.IsLogin( token ) ) //账号还没有登录
			{
				this.Reply( token, model.module, model.command, PResult.ACCOUNT_OFFLINE );
				return false;
			}
			return true;
		}

		protected bool CheckAndReplyUserOnline( IUserToken token, Packet model )
		{
			if ( !BizFactory.USER_BIZ.IsOnline( token ) )//角色还没有登录
			{
				this.Reply( token, model.module, model.command, PResult.USER_OFFLINE );
				return false;
			}
			return true;
		}

		protected void Reply( IUserToken token, byte module, ushort command, PResult result )
		{
			token.CALL_GENERIC_ACMD_REPLY( ( ushort ) result, command, module );
		}
	}
}