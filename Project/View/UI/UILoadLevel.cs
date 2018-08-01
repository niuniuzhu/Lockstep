using System.Collections.Generic;
using Core.Misc;
using Core.Net.Protocol;
using FairyUGUI.UI;
using Game.Loader;
using Logic;
using Logic.Model;
using Protocol;
using Protocol.Gen;
using View.Net;
using View.UI.Wins;
using PResultUtils = View.Misc.PResultUtils;

namespace View.UI
{
	public class UILoadLevel : IUIModule
	{
		private LoadBatch _lb;
		private SceneLoader _loader;
		private GComponent _root;

		public UILoadLevel()
		{
			UIPackage.AddPackage( "UI/loading" );
		}

		public void Dispose()
		{
		}

		public void Enter( object param )
		{
			NetModule.instance.AddACMDListener( Module.BATTLE, Command.ACMD_ENTER_BATTLE, this.OnEnterBattle );
			NetModule.instance.AddQCMDListener( Module.ROOM, Command.QCMD_MAP_READY, this.OnMapReadyResult );

			this._root = UIPackage.CreateObject( "cutscene", "Main" ).asCom;
			GRoot.inst.AddChild( this._root );

			GProgressBar bar = this._root["bar"].asProgress;
			bar.value = 0;
			bar.minValue = 0;
			bar.maxValue = 1;

			_DTO_begin_fight dto = ( _DTO_begin_fight )param;
			this.StartLoad( dto );
		}

		public void Leave()
		{
			NetModule.instance.RemoveACMDListener( Module.BATTLE, Command.ACMD_ENTER_BATTLE, this.OnEnterBattle );
			NetModule.instance.RemoveQCMDListener( Module.ROOM, Command.QCMD_MAP_READY, this.OnMapReadyResult );

			if ( this._lb != null )
			{
				this._lb.Cancel();
				this._lb = null;
			}

			if ( this._loader != null )
			{
				this._loader.Cancel();
				this._loader = null;
			}

			if ( this._root != null )
			{
				this._root.Dispose();
				this._root = null;
			}
		}

		public void Update()
		{
		}

		private void StartLoad( _DTO_begin_fight dto )
		{
			int count = dto.players.Length;
			string[] players = new string[count];
			for ( int i = 0; i < count; i++ )
				players[i] = dto.players[i].cid;

			BattleData battleData = ModelFactory.GetBattleData( dto.map );
			count = battleData.neutrals.Count;
			string[] neutrals = new string[count];
			int j = 0;
			foreach ( KeyValuePair<string, BattleData.Neutral> kv in battleData.neutrals )
				neutrals[j++] = kv.Value.id;

			count = battleData.structures.Count;
			string[] structures = new string[count];
			j = 0;
			foreach ( KeyValuePair<string, BattleData.Structure> kv in battleData.structures )
				structures[j++] = kv.Value.id;

			MapLoadHelper.Preload( dto.map, players, neutrals, structures, this.OnLoadComplete, this.OnLoadProgress, this.OnLoadError );
		}

		private void OnLoadProgress( object sender, float progress )
		{
			GProgressBar bar = this._root["bar"].asProgress;
			bar.value = progress;
		}

		private void OnLoadComplete( object sender, object o )
		{
			NetModule.instance.Send( ProtocolManager.PACKET_ROOM_QCMD_MAP_READY() );
		}

		private void OnLoadError( object sender, string msg, object data )
		{
			Logger.Warn( msg );
			this._loader = null;
		}

		private void OnEnterBattle( Packet packet )
		{
			MapLoadHelper.BeginSceneActivation( this.OnSceneActived, packet );
		}

		private void OnSceneActived( object sender, object data )
		{
			_DTO_enter_battle dto = ( ( _PACKET_BATTLE_ACMD_ENTER_BATTLE )data ).dto;
			this._loader = null;


			BattleParams param;
			param.frameRate = dto.frameRate;
			param.framesPerKeyFrame = dto.framesPerKeyFrame;
			param.uid = dto.uid;
			param.id = dto.mapId;
			param.rndSeed = dto.rndSeed;
			int count = dto.players.Length;
			param.players = new BattleParams.Player[count];
			for ( int i = 0; i < count; i++ )
			{
				_DTO_player_info playerInfoDTO = dto.players[i];
				BattleParams.Player p;
				p.id = playerInfoDTO.uid;
				p.cid = playerInfoDTO.cid;
				p.name = playerInfoDTO.name;
				p.skin = playerInfoDTO.skin;
				p.team = playerInfoDTO.team;
				param.players[i] = p;
			}

			UIManager.EnterBattle( param );
		}

		private void OnMapReadyResult( Packet packet )
		{
			_DTO_reply dto = ( ( _PACKET_GENERIC_ACMD_REPLY )packet ).dto;
			PResult result = ( PResult )dto.result;
			PResultUtils.ShowAlter( result );
		}
	}
}