using Core.Net;
using Game.Loader;
using Game.Misc;
using Game.Task;
using Logic.Model;
using System;
using System.Net.Sockets;
using UnityEngine;
using View.Net;
using View.UI;
using View.UI.Wins;
using XLua;
using Logger = Core.Misc.Logger;

namespace View
{
	public class Entry : MonoBehaviour
	{
		[HideInInspector]
		public bool useKCP;
		[HideInInspector]
		public string ip = "127.0.0.1";
		[HideInInspector]
		public int port = 2551;

		[HideInInspector]
		public string logServerIp = "127.0.0.1";
		[HideInInspector]
		public int logServerPort = 23000;
		[HideInInspector]
		public LoggerProxy.LogLevel logLevel = LoggerProxy.LogLevel.All;

		private bool _ready;

		private void Start()
		{
			AppDomain.CurrentDomain.UnhandledException += this.OnUnhandledException;

			Application.runInBackground = true;
			Application.targetFrameRate = 60;
			DontDestroyOnLoad( this.gameObject );

			Env.isEditor = Application.isEditor;
			Env.isRunning = true;
			Env.platform = ( int )Application.platform;
			Env.StartTime();

			LoggerProxy.Init( Application.dataPath.Replace( "\\", "/" ) + "/../Log/", this.logServerIp, this.logServerPort );
			LoggerProxy.logLevel = this.logLevel;


			AssetsManager.Init( Application.streamingAssetsPath );
			AssetsManager.LoadManifest( this.OnManifestLoadComplete, this.OnManifestLoadError, false, Application.productName );
		}

		private void OnManifestLoadError( object sender, string msg, object data )
		{
			Logger.Error( msg );
		}

		private void OnManifestLoadComplete( object sender, object data )
		{
			//this.PreloadShader();
			this.PreloadMaterial();
		}

		private void PreloadShader()
		{
			AssetsLoader loader = new AssetsLoader( "shader", string.Empty );
			loader.Load( this.OnLoadShaderComplete, null, this.OnLoadShaderError );
		}

		private void OnLoadShaderError( object sender, string msg, object data )
		{
			Logger.Error( msg );
		}

		private void OnLoadShaderComplete( object sender, AssetsProxy assetsproxy, object data )
		{
			this.PreloadMaterial();
		}

		private void PreloadMaterial()
		{
			AssetsLoader loader = new AssetsLoader( "material", string.Empty );
			loader.Load( this.OnLoadMaterialComplete, null, this.OnLoadSMaterialError );
		}

		private void OnLoadSMaterialError( object sender, string msg, object data )
		{
			Logger.Error( msg );
		}

		private void OnLoadMaterialComplete( object sender, AssetsProxy assetsproxy, object data )
		{
			this.InitLua();
		}

		private void InitLua()
		{
			AssetsLoader loader = new AssetsLoader( "lua", string.Empty );
			loader.Load( this.OnLuaPreloadComplete, null, this.OnLuaPreloadError );
		}

		private void OnLuaPreloadError( object sender, string msg, object data )
		{
			Logger.Error( msg );
		}

		private void OnLuaPreloadComplete( object sender, AssetsProxy assetsproxy, object data )
		{
			string luaPath;
			bool binary;
			//if ( Env.isEditor )
			//{
			//	binary = false;
			//	luaPath = Application.dataPath + "/Scripts/Lua";
			//}
			//else
			//{
				binary = true;
				luaPath = "Assets/Sources/lua/";
			//}

			//LuaEnv.Log = Logger.Log;
			XLuaGenIniterRegister.Init();
			WrapPusher.Init();
			DelegatesGensBridge.Init();
			Env.LUA_ENV = new LuaEnv();
			Env.LUA_ENV.AddLoader( ( ref string filepath ) => LuaLoader.Load( ref luaPath, ref filepath, binary ) );
			Env.LUA_ENV.DoString( "print(\"lua start\");" );
			Env.LUA_ENV.DoString( "require \"global\"" );
			TaskManager.instance.RegisterTimer( 5, 0, true, this.OnLuaTick, null );

			this.Init();
		}

		private void Init()
		{
			this._ready = true;

			Defs.Init( Resources.Load<TextAsset>( "Defs/b_defs" ).text, Resources.Load<TextAsset>( "Defs/lscript" ).text );

			UIManager.Init();

			NetworkManager.SetupKCP();
			NetworkManager.AddPacketTypes();
			Windows.CONNECTING_WIN.Open( NetworkConfig.CONNECTION_TIMEOUT / 1000f );
			NetModule.Initialize( this.useKCP ? NetworkManager.PType.Kcp : NetworkManager.PType.Tcp );
			NetModule.instance.OnSocketEvent += this.OnSocketEvent;
			NetModule.instance.Connect( this.ip, this.port );

			Windows.CONNECTING_WIN.Open( Core.Net.NetworkConfig.CONNECTION_TIMEOUT / 1000f );
		}
		private void OnSocketEvent( SocketEvent e )
		{
			switch ( e.type )
			{
				case SocketEvent.Type.Connect:
					if ( e.errorCode == SocketError.Success )
					{
						Logger.Log( $"Connected to server {this.ip}:{this.port}" );
						Windows.CONNECTING_WIN.Hide();
						UIManager.EnterLogin();
					}
					else
					{
						string msg = $"Socket error, type:{e.type}, code:{e.errorCode}, msg:{e.msg}";
						Logger.Warn( msg );
						Windows.ALERT_WIN.Open( msg, this.OnConfirmDisconnected );
					}
					break;

				case SocketEvent.Type.Close:
					{
						string msg = $"Socket error, type:{e.type}, code:{e.errorCode}, msg:{e.msg}";
						Logger.Warn( msg );
						Windows.ALERT_WIN.Open( msg, this.OnConfirmDisconnected );
					}
					break;
			}
		}


		private void OnConfirmDisconnected()
		{
			if ( Env.isRunning )
			{
				Windows.CloseAll();
				UIManager.LeaveModule();
				NetModule.instance.Connect( this.ip, this.port );
				Windows.CONNECTING_WIN.Open( Core.Net.NetworkConfig.CONNECTION_TIMEOUT / 1000f );
			}
		}

		private void OnLuaTick( int index, float dt, object param )
		{
			Env.LUA_ENV.Tick();
		}

		private void Update()
		{
			if ( !this._ready )
				return;
			NetModule.instance.Update( ( long )( Time.deltaTime * 1000 ) );
			UIManager.Update();
			BattleManager.Update( Time.deltaTime );
		}

		private void LateUpdate()
		{
			if ( !this._ready )
				return;
			UIManager.LateUpdate();
			BattleManager.LateUpdate();
		}

		private void OnDrawGizmos()
		{
			if ( !this._ready || !Env.isEditor )
				return;

			if ( Env.isEditor )
				BattleManager.OnDrawGizmos();
		}

		void OnApplicationQuit()
		{
			Env.isRunning = false;
			if ( NetModule.instance != null )
			{
				NetModule.instance.OnSocketEvent -= this.OnSocketEvent;
				NetModule.instance.Dispose();
			}
			UIManager.Dispose();
			LoggerProxy.Dispose();
			AppDomain.CurrentDomain.UnhandledException -= this.OnUnhandledException;
		}

		private void OnUnhandledException( object sender, UnhandledExceptionEventArgs e )
		{
			Logger.Error( e.ExceptionObject.ToString() );
		}
	}
}