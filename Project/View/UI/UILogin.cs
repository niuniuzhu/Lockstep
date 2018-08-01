using Core.Misc;
using Core.Net.Protocol;
using Protocol;
using Protocol.Gen;
using View.Net;

namespace View.UI
{
	public class UILogin : IUIModule
	{
		private string _account;
		private string _password;
		private string _username;

		public string id { get; private set; }

		public UILogin()
		{
			this._account = GuidHash.GetString();
			this._password = "123";
			this._username = this._account;
		}

		public void Dispose()
		{
		}

		public void Enter( object param )
		{
			NetModule.instance.AddQCMDListener( Module.USER, Command.QCMD_REG, this.OnRegResult );
			NetModule.instance.AddQCMDListener( Module.USER, Command.QCMD_LOGIN, this.OnLoginResult );
			NetModule.instance.AddQCMDListener( Module.USER, Command.QCMD_CREATE_USER, this.OnCreateResult );
			NetModule.instance.AddQCMDListener( Module.USER, Command.QCMD_USER_ONLINE, this.OnOnlineResult );

			NetModule.instance.AddACMDListener( Module.USER, Command.ACMD_USER_ONLINE, this.OnOnline );

			this.SendReg();
		}

		public void Leave()
		{
			NetModule.instance.RemoveQCMDListener( Module.USER, Command.QCMD_REG, this.OnRegResult );
			NetModule.instance.RemoveQCMDListener( Module.USER, Command.QCMD_LOGIN, this.OnLoginResult );
			NetModule.instance.RemoveQCMDListener( Module.USER, Command.QCMD_CREATE_USER, this.OnCreateResult );
			NetModule.instance.RemoveQCMDListener( Module.USER, Command.QCMD_USER_ONLINE, this.OnOnlineResult );

			NetModule.instance.RemoveACMDListener( Module.USER, Command.ACMD_USER_ONLINE, this.OnOnline );
		}

		public void Update()
		{
		}

		private void SendReg()
		{
			NetModule.instance.Send( ProtocolManager.PACKET_USER_QCMD_REG( this._account, this._password ) );
		}

		private void SendLogin()
		{
			NetModule.instance.Send( ProtocolManager.PACKET_USER_QCMD_LOGIN( this._account, this._password ) );
		}

		private void SendCreate()
		{
			NetModule.instance.Send( ProtocolManager.PACKET_USER_QCMD_CREATE_USER( this._username ) );
		}

		private void SendOnline()
		{
			NetModule.instance.Send( ProtocolManager.PACKET_USER_QCMD_USER_ONLINE() );
		}

		private void OnRegResult( Packet packet )
		{
			//可能掉线,已经注册过了
			//ReplyDTO dto = model.GetContent<ReplyDTO>();
			//PResultUtils.ShowAlter( dto.result );
			//if ( dto.result == PResult.SUCCESS )
			this.SendLogin();
		}

		private void OnLoginResult( Packet packet )
		{
			_DTO_reply dto = ( ( _PACKET_GENERIC_ACMD_REPLY )packet ).dto;
			PResult result = ( PResult )dto.result;
			PResultUtils.ShowAlter( result );
			if ( result == PResult.SUCCESS )
				this.SendCreate();
		}

		private void OnCreateResult( Packet packet )
		{
			//ReplyDTO dto = model.GetContent<ReplyDTO>();
			//PResultUtils.ShowAlter( dto.result );
			//if ( dto.result == PResult.SUCCESS )
			this.SendOnline();
		}

		private void OnOnline( Packet packet )
		{
			_DTO_charactor dto = ( ( _PACKET_USER_ACMD_USER_ONLINE )packet ).dto;
			CUser.id = this.id = dto.uid;

			UIManager.EnterHall();
		}

		private void OnOnlineResult( Packet packet )
		{
			_DTO_reply dto = ( ( _PACKET_GENERIC_ACMD_REPLY )packet ).dto;
			PResult result = ( PResult )dto.result;
			PResultUtils.ShowAlter( result );
		}
	}
}