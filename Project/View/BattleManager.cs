using System.Diagnostics;
using System.Threading;
using Core.Math;
using Core.Net.Protocol;
using Core.Structure;
using Game.Loader;
using Logic;
using Logic.Event;
using Logic.Model;
using Protocol.Gen;
using UnityEngine;
using View.Controller;
using View.Event;
using View.Misc;
using View.Net;
using Utils = Game.Misc.Utils;

namespace View
{
	public static class BattleManager
	{
		private static bool _init;
		private static bool _shouldSendActions;
		private static int _sendActionFrame;
		private static Thread _logicThread;
		private static int _nextKeyFrame;
		private static float _elapsedSinceLastLogicUpdate;
		private static float _elapsed;
		private static int _frameRate;
		private static int _framesPerKeyFrame;
		private static readonly SwitchQueue<_DTO_frame_info> SERVER_KEYFRAMES = new SwitchQueue<_DTO_frame_info>();

		public static VBattle cBattle { get; private set; }
		public static Battle lBattle { get; private set; }

		public static void Init( BattleParams param )
		{
			_init = true;
			_shouldSendActions = false;

			string model = ModelFactory.GetBattleData( Utils.GetIDFromRID( param.id ) ).model;
			CAIBakedNavmesh navmeshData = AssetsManager.LoadAsset<CAIBakedNavmesh>( "scene/" + model + "_navmesh",
																					"CAIBakedNavmesh" );

			_framesPerKeyFrame = param.framesPerKeyFrame;
			_frameRate = param.frameRate;
			_nextKeyFrame = param.framesPerKeyFrame;

			cBattle = new VBattle( param );
			lBattle = new Battle( param, navmeshData.GetNavmesh(), Env.LUA_ENV );

			_logicThread = new Thread( LogicWorker );
			_logicThread.IsBackground = true;
			_logicThread.Start();

			NetModule.instance.AddACMDListener( Module.BATTLE, Command.ACMD_BATTLE_START, HandleBattleStart );
			NetModule.instance.AddACMDListener( Module.BATTLE, Command.ACMD_FRAME, HandleFrame );
			NetModule.instance.AddACMDListener( Module.BATTLE, Command.ACMD_BATTLE_END, HandleBattleEnd );
			NetModule.instance.Send( ProtocolManager.PACKET_BATTLE_QCMD_BATTLE_CREATED() );

		}

		public static void Dispose()
		{
			if ( !_init )
				return;

			_init = false;

			_elapsed = 0;
			_elapsedSinceLastLogicUpdate = 0;
			_shouldSendActions = false;
			_sendActionFrame = 0;
			SERVER_KEYFRAMES.Clear();

			lBattle.Dispose();
			EventCenter.Sync();
			cBattle.Dispose();

			lBattle = null;
			cBattle = null;

			_logicThread.Join();
			_logicThread = null;

			NetModule.instance.RemoveACMDListener( Module.BATTLE, Command.ACMD_BATTLE_START, HandleBattleStart );
			NetModule.instance.RemoveACMDListener( Module.BATTLE, Command.ACMD_FRAME, HandleFrame );
			NetModule.instance.RemoveACMDListener( Module.BATTLE, Command.ACMD_BATTLE_END, HandleBattleEnd );
		}

		private static void HandleBattleStart( Packet packet )
		{
		}

		private static void HandleBattleEnd( Packet packet )
		{
			int winTeam = ( ( _PACKET_BATTLE_QCMD_END_BATTLE )packet ).dto.value;
			UIEvent.BattleEnd( winTeam );
		}

		private static void HandleFrame( Packet packet )
		{
			SERVER_KEYFRAMES.Push( ( ( _PACKET_BATTLE_ACMD_FRAME )packet ).dto );
		}

		public static void Update( float deltaTime )
		{
			if ( !_init )
				return;
			if ( _shouldSendActions )
			{
				FrameActionManager.SendActions( _sendActionFrame );
				_shouldSendActions = false;
			}
			EventCenter.Sync();
			cBattle.Update( deltaTime );
		}

		public static void LateUpdate()
		{
			if ( !_init )
				return;
			cBattle.LateUpdate();
		}

		public static void OnDrawGizmos()
		{
			if ( !_init )
				return;
			cBattle.OnDrawGizmos();
		}

		private static void LogicWorker( object o )
		{
			long millisecondsPreFrame = 1000 / _frameRate;
			long realCost = 0;
			long lastElapsedMilliseconds = 0;
			Stopwatch sw = new Stopwatch();
			sw.Start();
			while ( lBattle != null )
			{
				sw.Start();
				UpdateLogic( realCost * 0.001f, millisecondsPreFrame * 0.001f );
				Thread.Sleep( 10 );
				long elapsedMilliseconds = sw.ElapsedMilliseconds;
				realCost = elapsedMilliseconds - lastElapsedMilliseconds;
				lastElapsedMilliseconds = elapsedMilliseconds;
			}
			sw.Stop();
		}

		private static void UpdateLogic( float rdt, float fdt )
		{
			_elapsed += rdt;

			//如果本地frame比服务端慢，则需要快速步进追赶服务端帧数
			SERVER_KEYFRAMES.Switch();
			while ( !SERVER_KEYFRAMES.isEmpty )
			{
				_DTO_frame_info dto = SERVER_KEYFRAMES.Pop();
				int length = dto.frameId - lBattle.frame;
				while ( length >= 0 )
				{
					if ( length == 0 )
						HandleAction( dto );
					else
					{
						lBattle.Simulate( _elapsed, fdt );
						_elapsed = 0f;
					}
					--length;
				}
				_nextKeyFrame = dto.frameId + _framesPerKeyFrame;
			}

			if ( lBattle.frame < _nextKeyFrame )
			{
				_elapsedSinceLastLogicUpdate += rdt;

				while ( _elapsedSinceLastLogicUpdate >= fdt )
				{
					if ( lBattle.frame >= _nextKeyFrame )
						break;

					lBattle.Simulate( _elapsed, fdt );

					if ( lBattle.frame == _nextKeyFrame )
					{
						_sendActionFrame = lBattle.frame;
						_shouldSendActions = true;
					}

					_elapsed = 0f;
					_elapsedSinceLastLogicUpdate -= fdt;
				}
			}
		}

		private static void HandleAction( _DTO_frame_info dto )
		{
			int count = dto.actions.Length;
			for ( int i = 0; i < count; i++ )
			{
				_DTO_action_info action = dto.actions[i];

				if ( action.sender == VPlayer.instance.rid )
					SyncEvent.HandleFrameAction();

				FrameActionType type = ( FrameActionType )action.type;
				switch ( type )
				{
					case FrameActionType.Idle:
						break;

					case FrameActionType.Move:
						{
							lBattle.HandleMove( action.sender, new Vec3( action.x, action.y, action.z ) );
						}
						break;

					case FrameActionType.Track:
						{
							lBattle.HandleTrack( action.sender, action.target );
						}
						break;

					case FrameActionType.UseSkill:
						{
							Vector3 targetPoint = new Vector3( action.x, action.y, action.z );
							lBattle.HandleUseSkill( action.sid, action.src, action.target, targetPoint.ToVec3() );
						}
						break;

					case FrameActionType.Relive:
						{
							lBattle.HandleRelive( action.sender );
							break;
						}

					case FrameActionType.UpgradeSkill:
						{
							lBattle.HandleUpgradeSkill( action.sender, action.target );
							break;
						}
				}
			}
		}
	}
}