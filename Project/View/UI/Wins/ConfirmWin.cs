using FairyUGUI.Event;
using FairyUGUI.UI;

namespace View.UI.Wins
{
	public class ConfirmWin : Window
	{
		public delegate void ClickHandler( int value );

		private string _message;
		private ClickHandler _clickHandler;

		public ConfirmWin()
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
			this.contentPane = UIPackage.CreateObject( "global", "Confirm" ).asCom;
			this.modalType = ModalType.Popup;
			this.Center();
		}

		protected override void InternalOnShown()
		{
			GTextField message = this.contentPane["message"].asTextField;
			message.text = this._message;
			this.contentPane["confirmBtn"].onClick.Add( this.OnConfirmBtnClick );
			this.contentPane["cancelBtn"].onClick.Add( this.OnCancelBtnClick );
		}

		protected override void InternalOnHide()
		{
			this.contentPane["confirmBtn"].onClick.Remove( this.OnConfirmBtnClick );
			this.contentPane["cancelBtn"].onClick.Remove( this.OnCancelBtnClick );
			this._clickHandler = null;
		}

		public void Open( string message, ClickHandler clickHandler = null )
		{
			this._message = message;
			this._clickHandler = clickHandler;
			this.Show( GRoot.inst );
		}

		private void OnConfirmBtnClick( EventContext context )
		{
			ClickHandler clickHandler = this._clickHandler;
			this.Hide();
			if ( clickHandler != null )
				clickHandler.Invoke( 0 );
		}

		private void OnCancelBtnClick( EventContext context )
		{
			ClickHandler clickHandler = this._clickHandler;
			this.Hide();
			if ( clickHandler != null )
				clickHandler.Invoke( 1 );
		}
	}
}