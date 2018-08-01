using FairyUGUI.Event;
using FairyUGUI.UI;
using Protocol.Gen;
using View.Net;

namespace View.UI.Wins
{
	public class CreateRoomWin : Window
	{
		public CreateRoomWin()
		{
			this.showAnimation = new WindowScaleAnimation();
			this.showAnimation.duration = 0.1f;
		}

		protected override void InternalOnInit()
		{
			this.contentPane = UIPackage.CreateObject( "hall", "创建房间窗口" ).asCom;
			this.modalType = ModalType.Popup;
			this.Center();
		}

		protected override void InternalOnShown()
		{
			GButton createBtn = this.contentPane["create"].asButton;
			createBtn.onClick.Add( this.OnCreateBtnClick );
		}

		private void OnCreateBtnClick( EventContext context )
		{
			this.ShowModalWait();

			string mName = this.contentPane["name"].asTextField.text;
			NetModule.instance.Send( ProtocolManager.PACKET_HALL_QCMD_CREATE_ROOM( mName ) );
		}
	}
}