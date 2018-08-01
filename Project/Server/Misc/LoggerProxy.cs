using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Server.Misc
{
	public static class LoggerProxy
	{
		private static ILog _log;

		public static void Init( string config )
		{
			Core.Misc.Logger.logAction = Log;
			Core.Misc.Logger.debugAction = Debug;
			Core.Misc.Logger.netAction = Log;
			Core.Misc.Logger.infoAction = Info;
			Core.Misc.Logger.warnAction = Warn;
			Core.Misc.Logger.errorAction = Error;
			Core.Misc.Logger.factalAction = Fatal;

			ILoggerRepository repository = LogManager.CreateRepository( "NETCoreRepository" );
			using ( var stream = GenerateStreamFromString( config ) )
			{
				XmlConfigurator.Configure( repository, stream );
			}
			_log = LogManager.GetLogger( repository.Name, "Server" );
		}

		public static void Dispose()
		{
			LogManager.Shutdown();
		}

		public static void Debug( object obj )
		{
			_log.Debug( obj + Environment.NewLine + GetStacks() );
		}

		public static void Log( object obj )
		{
			_log.Debug( obj );
		}

		public static void Warn( object obj )
		{
			_log.Warn( obj );
		}

		public static void Error( object obj )
		{
			_log.Error( obj );
		}

		public static void Info( object obj )
		{
			_log.Info( obj );
		}

		public static void Fatal( object obj )
		{
			_log.Fatal( obj );
		}

		private static Stream GenerateStreamFromString( string s )
		{
			var stream = new MemoryStream();
			var writer = new StreamWriter( stream );
			writer.Write( s );
			writer.Flush();
			stream.Position = 0;
			return stream;
		}

		private static string GetStacks()
		{
			StackTrace st = new StackTrace( true );
			if ( st.FrameCount < 2 )
				return string.Empty;

			StringBuilder sb = new StringBuilder();
			int count = Math.Min( st.FrameCount, 5 );
			for ( int i = 2; i < count; i++ )
			{
				StackFrame sf = st.GetFrame( i );
				string fn = sf.GetFileName();
				int pos = fn.LastIndexOf( '\\' ) + 1;
				fn = fn.Substring( pos, fn.Length - pos );
				sb.Append( $" M:{sf.GetMethod()} in {fn}:{sf.GetFileLineNumber()},{sf.GetFileColumnNumber()}" );
				if ( i != count - 1 )
					sb.AppendLine();
			}
			return sb.ToString();
		}
	}
}