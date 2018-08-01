using Protocol;
using View.UI.Wins;

namespace View.Misc
{
	public static class PResultUtils
	{
		public static string GetErrorMsg( PResult result )
		{
			return result.ToString();//todo
		}

		public static void ShowAlter( PResult result )
		{
			if ( result != PResult.SUCCESS )
				Windows.ALERT_WIN.Open( GetErrorMsg( result ) );
		}
	}
}