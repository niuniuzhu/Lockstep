namespace Logic.Misc
{
	public delegate void LogAction( object obj );

	public static class LLogger
	{
		public static LogAction logAction;
		public static LogAction warningAction;
		public static LogAction errorAction;
		public static LogAction infoAction;

		public static void Log( object obj )
		{
			logAction?.Invoke( obj );
		}

		public static void Log( string format, string arg0 )
		{
			logAction?.Invoke( string.Format( format, arg0 ) );
		}

		public static void Log( string format, string arg0, string arg1 )
		{
			logAction?.Invoke( string.Format( format, arg0, arg1 ) );
		}

		public static void Log( string format, string arg0, string arg1, string arg2 )
		{
			logAction?.Invoke( string.Format( format, arg0, arg1, arg2 ) );
		}

		public static void Log( string format, params object[] arg0 )
		{
			logAction?.Invoke( string.Format( format, arg0 ) );
		}

		public static void Warning( object obj )
		{
			warningAction?.Invoke( obj );
		}

		public static void Warning( string format, string arg0 )
		{
			warningAction?.Invoke( string.Format( format, arg0 ) );
		}

		public static void Warning( string format, string arg0, string arg1 )
		{
			warningAction?.Invoke( string.Format( format, arg0, arg1 ) );
		}

		public static void Warning( string format, string arg0, string arg1, string arg2 )
		{
			warningAction?.Invoke( string.Format( format, arg0, arg1, arg2 ) );
		}

		public static void Warning( string format, params object[] arg0 )
		{
			warningAction?.Invoke( string.Format( format, arg0 ) );
		}

		public static void Error( object obj )
		{
			errorAction?.Invoke( obj );
		}

		public static void Error( string format, string arg0 )
		{
			errorAction?.Invoke( string.Format( format, arg0 ) );
		}

		public static void Error( string format, string arg0, string arg1 )
		{
			errorAction?.Invoke( string.Format( format, arg0, arg1 ) );
		}

		public static void Error( string format, string arg0, string arg1, string arg2 )
		{
			errorAction?.Invoke( string.Format( format, arg0, arg1, arg2 ) );
		}

		public static void Error( string format, params object[] arg0 )
		{
			errorAction?.Invoke( string.Format( format, arg0 ) );
		}

		public static void Info( string format, string arg0 )
		{
			infoAction?.Invoke( string.Format( format, arg0 ) );
		}

		public static void Info( string format, string arg0, string arg1 )
		{
			infoAction?.Invoke( string.Format( format, arg0, arg1 ) );
		}

		public static void Info( string format, string arg0, string arg1, string arg2 )
		{
			infoAction?.Invoke( string.Format( format, arg0, arg1, arg2 ) );
		}

		public static void Info( string format, params object[] arg0 )
		{
			infoAction?.Invoke( string.Format( format, arg0 ) );
		}
	}
}