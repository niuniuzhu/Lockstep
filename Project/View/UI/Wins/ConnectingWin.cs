using FairyUGUI.UI;
using Game.Task;

namespace View.UI.Wins
{
	public class ConnectingWin : Window
	{
		private float _duration;
		private GProgressBar _bar;

		public ConnectingWin()
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
			this.contentPane = UIPackage.CreateObject( "global", "Connecting" ).asCom;
			this.modalType = ModalType.Modal;
			this.Center();

			this._bar = this.contentPane["n3"].asProgress;
		}

		protected override void InternalOnShown()
		{
			this._bar.value = 0f;
			this._bar.maxValue = this._duration;
			TaskManager.instance.RegisterTimer( 0.02f, 0, true, this.OnTimer, null );
		}

		protected override void InternalOnHide()
		{
			TaskManager.instance.UnregisterTimer( this.OnTimer );
		}

		private void OnTimer( int index, float dt, object param )
		{
			this._bar.value = 0.02f * index;
		}

		public void Open( float duration )
		{
			this._duration = duration;
			this.Show( GRoot.inst );
		}
	}
}