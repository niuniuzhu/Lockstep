using Core.Misc;
using Core.Net;
using Core.Structure;
using Server.Misc;
using Server.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;

namespace Server
{
	static class Program
	{
		private const string NETWORK_NAME = "server";
		private static HandlerCenter _handlerCenter;
		private static int _port;
		private static int _useUDP;
		private static bool _disposed;
		private static readonly SwitchQueue<string> INPUT_QUEUE = new SwitchQueue<string>();

		static int Main( string[] args )
		{
			return Start( args );
		}

		static int Start( string[] args )
		{
			AssemblyName[] assemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies();
			foreach ( AssemblyName assembly in assemblies )
				Assembly.Load( assembly );

			LoggerProxy.Init( Resources.log4net_config );

			CommandArgs commandArg = CommandLine.Parse( args );
			Dictionary<string, string> argPairs = commandArg.argPairs;

			int maxClient;
			if ( !argPairs.ContainsKey( "c" ) )
				maxClient = 10;
			else
			{
				if ( !int.TryParse( argPairs["c"], out maxClient ) )
				{
					Logger.Log( "最大连接数解析错误" );
					return 1;
				}
			}
			if ( !argPairs.ContainsKey( "p" ) )
			{
				Logger.Log( "没有指定端口" );
				return 1;
			}
			if ( !int.TryParse( argPairs["p"], out int _port ) )
			{
				Logger.Log( "端口解析错误" );
				return 1;
			}
			int useUDP;
			if ( !argPairs.ContainsKey( "t" ) )
				useUDP = 0;
			else
			{
				if ( !int.TryParse( argPairs["t"], out useUDP ) )
				{
					Logger.Log( "协议类型解析错误" );
					return 1;
				}
			}
			if ( !argPairs.ContainsKey( "d" ) )
			{
				Logger.Log( "无效的配置文件路径" );
				return 1;
			}
			string defFile = argPairs["d"];
			string json;
			try
			{
				json = File.ReadAllText( defFile );
			}
			catch ( Exception e )
			{
				Logger.Log( e );
				return 1;
			}

			Defs.Init( json );

			_handlerCenter = new HandlerCenter();
			NetworkManager.PType protocolType = useUDP > 0 ? NetworkManager.PType.Kcp : NetworkManager.PType.Tcp;
			if ( protocolType == NetworkManager.PType.Kcp )
				NetworkManager.SetupKCP();
			NetworkManager.AddPacketTypes();
			NetworkManager.CreateServer( NETWORK_NAME, protocolType, maxClient );
			NetworkManager.AddServerEventHandler( NETWORK_NAME, ProcessServerEvent );
			NetworkManager.StartServer( NETWORK_NAME, _port );
			Logger.Log( $"Server started, listening port: {_port}" );

			Thread inputThread = new Thread( InputWorker );
			inputThread.IsBackground = true;
			inputThread.Start();

			System.Diagnostics.Stopwatch sp = new System.Diagnostics.Stopwatch();
			sp.Start();
			long lastElapsed = 0;
			while ( !_disposed )
			{
				long elapsed = sp.ElapsedMilliseconds;
				long dt = elapsed - lastElapsed;
				NetworkManager.Update( dt );
				lastElapsed = elapsed;
				ProcessInput();
				Thread.Sleep( 10 );
			}
			sp.Stop();

			inputThread.Join();
			LoggerProxy.Dispose();
			return 0;
		}

		private static void ProcessServerEvent( SocketEvent e )
		{
			switch ( e.type )
			{
				case SocketEvent.Type.Accept:
					if ( e.errorCode == SocketError.Success )
					{
						Logger.Log( $"有客户端连接了, code:{e.errorCode}, msg:{e.msg}" );
						_handlerCenter.ClientConnect( e.userToken );
					}
					else
						Logger.Log( $"Socket error, type:{e.type}, code:{e.errorCode}, msg:{e.msg}" );

					break;

				case SocketEvent.Type.Disconnect:
					_handlerCenter.ClientClose( e.userToken );
					Logger.Log( $"有客户端断开连接了, code:{e.errorCode}, msg:{e.msg}" );
					break;

				case SocketEvent.Type.Receive:
					//Logger.Log( $"Received: {e.packet}" );
					_handlerCenter.ProcessMessage( e.userToken, e.packet );
					break;

				default:
					if ( e.errorCode != SocketError.Success )
					{
						Logger.Log( $"Socket error, type:{e.type}, code:{e.errorCode}, msg:{e.msg}" );
					}
					break;
			}
		}

		private static void ProcessInput()
		{
			INPUT_QUEUE.Switch();
			while ( !INPUT_QUEUE.isEmpty )
			{
				string cmd = INPUT_QUEUE.Pop();
				if ( cmd == "exit" )
				{
					Dispose();
				}
				else if ( cmd == "cls" )
				{
					Console.Clear();
				}
				else if ( cmd == "stop" )
				{
					NetworkManager.StopServer( NETWORK_NAME );
				}
				else if ( cmd == "start" )
				{
					NetworkManager.StartServer( NETWORK_NAME, _port );
				}
			}
		}

		private static void Dispose()
		{
			_disposed = true;
			NetworkManager.Dispose();
			Logger.Log( "Server stoped" );
#if KCP_NATIVE
			if ( _useUDP > 0 )
				Core.Net.Kcp.KCPInterface.ikcp_release_allocator();
#endif
		}

		private static void InputWorker()
		{
			while ( !_disposed )
			{
				string cmd = Console.ReadLine();
				INPUT_QUEUE.Push( cmd );
				Thread.Sleep( 10 );
			}
		}
	}
}
