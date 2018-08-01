using Core.Misc;
using Core.Net;
using Core.Net.Protocol;
using Protocol.Gen;
using Server.Logic;
using System;

namespace Server
{
	public class HandlerCenter : AbsHandlerCenter
	{
		readonly IHandler _syncTime;
		readonly IHandler _user;
		readonly IHandler _hall;
		readonly IHandler _room;
		readonly IHandler _battle;

		public HandlerCenter()
		{
			this._syncTime = new SyncTimeHandler();
			this._user = new UserHandler();
			this._hall = new HallHandler();
			this._room = new RoomHandler();
			this._battle = new BattleHandler();
		}

		public override void ClientConnect( IUserToken token )
		{
		}

		public override void ClientClose( IUserToken token )
		{
			this._battle.ClientClose( token );
			this._room.ClientClose( token );
			this._hall.ClientClose( token );
			//user的连接关闭方法 一定要放在逻辑处理单元后面
			//其他逻辑单元需要通过user绑定数据来进行内存清理 
			//如果先清除了绑定关系 其他模块无法获取角色数据会导致无法清理
			this._user.ClientClose( token );
		}

		public override void ProcessMessage( IUserToken token, Packet model )
		{
			try
			{
				switch ( model.module )
				{
					case Module.GENERIC:
						this._syncTime.ProcessMessage( token, model );
						break;

					case Module.USER:
						this._user.ProcessMessage( token, model );
						break;

					case Module.HALL:
						this._hall.ProcessMessage( token, model );
						break;

					case Module.ROOM:
						this._room.ProcessMessage( token, model );
						break;

					case Module.BATTLE:
						this._battle.ProcessMessage( token, model );
						break;
				}
			}
			catch ( Exception e )
			{
				Logger.Debug( e );
			}
		}
	}
}