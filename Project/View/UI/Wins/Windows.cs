namespace View.UI.Wins
{
	public static class Windows
	{
		public static readonly AlertWin ALERT_WIN = new AlertWin();
		public static readonly ConnectingWin CONNECTING_WIN = new ConnectingWin();
		public static readonly ConfirmWin CONFIRM_WIN = new ConfirmWin();

		public static void CloseAll()
		{
			ALERT_WIN.Hide( true );
			CONNECTING_WIN.Hide( true );
			CONFIRM_WIN.Hide( true );
		}
	}
}