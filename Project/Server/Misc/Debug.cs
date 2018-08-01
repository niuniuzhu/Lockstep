using System;

namespace Server.Misc
{
	public static class Debug
	{
		private static string GetStacks()
		{
			//StackTrace st = new StackTrace( false );
			//if ( st.FrameCount < 2 )
			//	return string.Empty;

			//StringBuilder sb = new StringBuilder();
			//for ( int i = 2; i < st.FrameCount; i++ )
			//{
			//	StackFrame sf = st.GetFrame( i );
			//	if ( i == 2 )
			//		sb.AppendLine( "Content:{0}" );
			//	sb.Append( sf );
			//}
			//return sb.ToString();
			return "{0}";
		}

		public static void Log( object message )
		{
			Console.WriteLine( GetStacks(), message );
		}

		public static void Log( string format, string arg0 )
		{
			Console.WriteLine( GetStacks(), string.Format( format, arg0 ) );
		}

		public static void Log( string format, string arg0, string arg1 )
		{
			Console.WriteLine( GetStacks(), string.Format( format, arg0, arg1 ) );
		}

		public static void Log( string format, string arg0, string arg1, string arg2 )
		{
			Console.WriteLine( GetStacks(), string.Format( format, arg0, arg1, arg2 ) );
		}

		public static void Log( string format, params object[] args )
		{
			Console.WriteLine( GetStacks(), string.Format( format, args ) );
		}
	}
}