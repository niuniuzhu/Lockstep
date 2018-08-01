using FairyUGUI.UI;
using Protocol;
using View.Misc;

namespace View.UI.Wins
{
	public class AlertWin : Window
	{
		private string _message;

		public AlertWin()
		{
			this.showAnimation = new WindowScaleAnimation();
			this.hideAnimation = new WindowScaleAnimation();
			this.showAnimation.duration = 0.1f;
			this.hideAnimation.duration = 0.1f;
			this.hideAnimation.keepOriginal = true;
			this.hideAnimation.reverse = true;
		}

		protected override void InternalOnInit()
		{
			this.contentPane = UIPackage.CreateObject( "global", "Alert" ).asCom;
			this.modalType = ModalType.Popup;
			this.Center();
		}

		protected override void InternalOnShown()
		{
			GTextField message = this.contentPane["message"].asTextField;
			message.text = this._message;
		}

		public void Open( string message, HideHandler hideHandler = null )
		{
			this._message = message;
			this.OnHide += hideHandler;
			this.Show( GRoot.inst );
		}

		public void ProcessPResult( PResult result )
		{
			if ( result != PResult.SUCCESS )
				this.Open( PResultUtils.GetErrorMsg( result ) );
		}
	}
}