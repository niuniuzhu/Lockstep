using System.Collections;
using Core.Misc;
using Core.Net.Protocol;
using FairyUGUI.Event;
using FairyUGUI.UI;
using Logic.Model;
using Protocol;
using Protocol.Gen;
using View.Net;

namespace View.UI
{
	public class UIRoom : IUIModule
	{
		private GComponent _root;
		private int _id;

		public UIRoom()
		{
			UIPackage.AddPackage( "UI/room" );
		}

		public void Dispose()
		{
		}

		public void Enter( object roomId )
		{
			NetModule.instance.AddACMDListener( Module.ROOM, Command.ACMD_ROOM_INFO, this.OnRoomInfo );
			NetModule.instance.AddACMDListener( Module.ROOM, Command.ACMD_LEAVE_ROOM, this.OnLeaveRoom );
			NetModule.instance.AddACMDListener( Module.ROOM, Command.ACMD_BEGIN_FIGHT, this.OnBeginFight );

			NetModule.instance.AddQCMDListener( Module.ROOM, Command.QCMD_LEAVE_ROOM, this.OnLeaveRoomResult );
			NetModule.instance.AddQCMDListener( Module.ROOM, Command.QCMD_ROOM_INFO, this.OnRoomInfoResult );
			NetModule.instance.AddQCMDListener( Module.ROOM, Command.QCMD_CHANGE_TEAM, this.OnChangeTeamResult );
			NetModule.instance.AddQCMDListener( Module.ROOM, Command.QCMD_CHANGE_MAP, this.OnChangeMapResult );
			NetModule.instance.AddQCMDListener( Module.ROOM, Command.QCMD_CHANGE_HERO, this.OnChangeHeroResult );
			NetModule.instance.AddQCMDListener( Module.ROOM, Command.QCMD_BEGIN_FIGHT, this.OnBeginFightResult );
			NetModule.instance.AddQCMDListener( Module.ROOM, Command.QCMD_ADD_FIGHT_READY, this.OnAddFightReadyResult );
			NetModule.instance.AddQCMDListener( Module.ROOM, Command.QCMD_REMOVE_FIGHT_READY, this.OnRemoveFightReadyResult );

			this._id = ( int )roomId;

			this._root = UIPackage.CreateObject( "room", "Main" ).asCom;
			GRoot.inst.AddChild( this._root );
			this._root.size = GRoot.inst.size;

			GButton fightBtn = this._root["fightBtn"].asButton;
			//fightBtn.grayed = true;
			//fightBtn.touchable = false;
			fightBtn.onClick.Add( this.OnFightBtnClick );

			GButton readyBtn = this._root["readyBtn"].asButton;
			readyBtn.onClick.Add( this.OnReadyBtnClick );

			GButton leaveBtn = this._root["leaveBtn"].asButton;
			leaveBtn.onClick.Add( this.OnLeaveBtnClick );

			this._root["t1"].onClick.Add( this.OnTeamOneClick );
			this._root["t2"].onClick.Add( this.OnTeamTwoeClick );

			Hashtable mapsDef = Defs.Get( "maps" );
			GComboBox maps = this._root["map"].asComboBox;
			maps.popupDirection = PopupDirection.Downward;
			maps.popupConstraint = PopupConstraint.Any;
			maps.autoSizeDropdown = true;
			maps.onChanged.Add( this.OnMapChangedBtnClick );
			maps.items = new string[mapsDef.Count];
			maps.values = new string[mapsDef.Count];
			int i = 0;
			foreach ( DictionaryEntry de in mapsDef )
			{
				string id = ( string )de.Key;
				maps.SetItem( i, id );
				maps.values[i] = id;
				++i;
			}

			GComboBox heros = this._root["hero"].asComboBox;
			heros.popupDirection = PopupDirection.Right;
			heros.popupConstraint = PopupConstraint.Any;
			//heros.autoSizeDropdown = true;
			heros.onChanged.Add( this.OnHeroChangedBtnClick );
			Hashtable entitiesDef = Defs.Get( "entities" );
			Hashtable heroDef = new Hashtable();
			foreach ( DictionaryEntry de in entitiesDef )
			{
				Hashtable hero = ( Hashtable )de.Value;
				string id = ( string )de.Key;
				if ( !id.StartsWith( "h" ) )
					continue;
				heroDef[id] = hero;
			}
			heros.items = new string[heroDef.Count];
			heros.values = new string[heroDef.Count];
			i = 0;
			foreach ( DictionaryEntry de in heroDef )
			{
				Hashtable hero = ( Hashtable )de.Value;
				string id = ( string )de.Key;
				if ( !id.StartsWith( "h" ) )
					continue;
				heros.SetItem( i, hero.GetString( "name" ) );
				heros.values[i] = id;
				++i;
			}

			this._root.ShowModalWait();
			NetModule.instance.Send( ProtocolManager.PACKET_ROOM_QCMD_ROOM_INFO( -1 ) );
		}

		public void Leave()
		{
			NetModule.instance.RemoveACMDListener( Module.ROOM, Command.ACMD_ROOM_INFO, this.OnRoomInfo );
			NetModule.instance.RemoveACMDListener( Module.ROOM, Command.ACMD_LEAVE_ROOM, this.OnLeaveRoom );
			NetModule.instance.RemoveACMDListener( Module.ROOM, Command.ACMD_BEGIN_FIGHT, this.OnBeginFight );

			NetModule.instance.RemoveQCMDListener( Module.ROOM, Command.QCMD_LEAVE_ROOM, this.OnLeaveRoomResult );
			NetModule.instance.RemoveQCMDListener( Module.ROOM, Command.QCMD_ROOM_INFO, this.OnRoomInfoResult );
			NetModule.instance.RemoveQCMDListener( Module.ROOM, Command.QCMD_CHANGE_TEAM, this.OnChangeTeamResult );
			NetModule.instance.RemoveQCMDListener( Module.ROOM, Command.QCMD_CHANGE_MAP, this.OnChangeMapResult );
			NetModule.instance.RemoveQCMDListener( Module.ROOM, Command.QCMD_CHANGE_HERO, this.OnChangeHeroResult );
			NetModule.instance.RemoveQCMDListener( Module.ROOM, Command.QCMD_BEGIN_FIGHT, this.OnBeginFightResult );
			NetModule.instance.RemoveQCMDListener( Module.ROOM, Command.QCMD_ADD_FIGHT_READY, this.OnAddFightReadyResult );
			NetModule.instance.RemoveQCMDListener( Module.ROOM, Command.QCMD_REMOVE_FIGHT_READY, this.OnRemoveFightReadyResult );

			this._id = -1;

			if ( this._root != null )
			{
				this._root.Dispose();
				this._root = null;
			}
		}

		public void Update()
		{
		}

		private void OnMapChangedBtnClick( EventContext context )
		{
			this._root.ShowModalWait();
			NetModule.instance.Send( ProtocolManager.PACKET_ROOM_QCMD_CHANGE_MAP( this._root["map"].asComboBox.value ) );
		}

		private void OnHeroChangedBtnClick( EventContext context )
		{
			this._root.ShowModalWait();
			NetModule.instance.Send( ProtocolManager.PACKET_ROOM_QCMD_CHANGE_HERO( this._root["hero"].asComboBox.value ) );
		}

		private void OnModelChangedBtnClick( EventContext context )
		{
			this._root.ShowModalWait();
			NetModule.instance.Send( ProtocolManager.PACKET_ROOM_QCMD_CHANGE_MODEL( byte.Parse( this._root["model"].asComboBox.value ) ) );
		}

		private void OnSkinChangedBtnClick( EventContext context )
		{
			this._root.ShowModalWait();
			NetModule.instance.Send( ProtocolManager.PACKET_ROOM_QCMD_CHANGE_SKIN( byte.Parse( this._root["skin"].asComboBox.value ) ) );
		}

		private void OnFightBtnClick( EventContext context )
		{
			this._root.ShowModalWait();
			NetModule.instance.Send( ProtocolManager.PACKET_ROOM_QCMD_BEGIN_FIGHT() );
		}

		private void OnReadyBtnClick( EventContext context )
		{
			this._root.ShowModalWait();
			if ( this._root["readyBtn"].asButton.selected )
				NetModule.instance.Send( ProtocolManager.PACKET_ROOM_QCMD_ADD_FIGHT_READY() );
			else
				NetModule.instance.Send( ProtocolManager.PACKET_ROOM_QCMD_REMOVE_FIGHT_READY() );
		}

		private void OnLeaveBtnClick( EventContext context )
		{
			this._root.ShowModalWait();
			NetModule.instance.Send( ProtocolManager.PACKET_ROOM_QCMD_LEAVE_ROOM() );
		}

		private void OnTeamOneClick( EventContext context )
		{
			this._root.ShowModalWait();
			NetModule.instance.Send( ProtocolManager.PACKET_ROOM_QCMD_CHANGE_TEAM( 0 ) );
		}

		private void OnTeamTwoeClick( EventContext context )
		{
			this._root.ShowModalWait();
			NetModule.instance.Send( ProtocolManager.PACKET_ROOM_QCMD_CHANGE_TEAM( 1 ) );

		}

		private void OnRoomInfo( Packet packet )
		{
			this._root.CloseModalWait();

			_DTO_room_info_detail dto = ( ( _PACKET_ROOM_ACMD_ROOM_INFO )packet ).dto;

			GComboBox maps = this._root["map"].asComboBox;
			maps.value = dto.map;

			GComboBox heros = this._root["hero"].asComboBox;

			_DTO_player_info[] players = dto.players;
			GList t1 = this._root["t1"].asCom["list"].asList;
			GList t2 = this._root["t2"].asCom["list"].asList;
			t1.RemoveChildrenToPool();
			t2.RemoveChildrenToPool();
			for ( int i = 0; i < players.Length; i++ )
			{
				_DTO_player_info infoDto = players[i];
				GComponent item = infoDto.team == 0 ? t1.AddItemFromPool().asCom : t2.AddItemFromPool().asCom;
				item.GetController( "c1" ).selectedIndex = 0;
				item["name"].asTextField.text = infoDto.name;
				item.GetController( "c1" ).selectedIndex = infoDto.ready ? 1 : 0;
				if ( infoDto.uid == CUser.id )
					heros.value = infoDto.cid;
			}
			this._root["name"].asTextField.text = dto.name;

			GButton fightBtn = this._root["fightBtn"].asButton;
			if ( dto.host == CUser.id )
			{
				this._root.GetController( "c1" ).selectedIndex = 0;
				bool isAllPlayerReady = this.IsAllPlayerReady( dto );
				if ( isAllPlayerReady )
				{
					fightBtn.grayed = false;
					fightBtn.touchable = true;
				}
				else
				{
					fightBtn.grayed = true;
					fightBtn.touchable = false;
				}
			}
			else
				this._root.GetController( "c1" ).selectedIndex = 1;
		}

		private bool IsAllPlayerReady( _DTO_room_info_detail dto )
		{
			_DTO_player_info[] players = dto.players;
			int count = players.Length;
			for ( int i = 0; i < count; i++ )
			{
				if ( players[i].uid == dto.host )
					continue;
				if ( !players[i].ready )
					return false;
			}
			return true;
		}

		private void OnLeaveRoom( Packet packet )
		{
			this._root.CloseModalWait();
			UIManager.EnterHall();
		}

		private void OnBeginFight( Packet packet )
		{
			this._root.CloseModalWait();
			_DTO_begin_fight dto = ( ( _PACKET_ROOM_ACMD_BEGIN_FIGHT )packet ).dto;
			UIManager.EnterLoading( dto );
		}

		private void OnLeaveRoomResult( Packet packet )
		{
			this._root.CloseModalWait();
			_DTO_reply dto = ( ( _PACKET_GENERIC_ACMD_REPLY )packet ).dto;
			PResult result = ( PResult )dto.result;
			PResultUtils.ShowAlter( result );
			if ( result == PResult.SUCCESS )
				UIManager.EnterHall();
		}

		private void OnChangeTeamResult( Packet packet )
		{
			this._root.CloseModalWait();
			_DTO_reply dto = ( ( _PACKET_GENERIC_ACMD_REPLY )packet ).dto;
			PResult result = ( PResult )dto.result;
			PResultUtils.ShowAlter( result );
		}

		private void OnChangeMapResult( Packet packet )
		{
			this._root.CloseModalWait();
			_DTO_reply dto = ( ( _PACKET_GENERIC_ACMD_REPLY )packet ).dto;
			PResult result = ( PResult )dto.result;
			PResultUtils.ShowAlter( result );
		}

		private void OnChangeHeroResult( Packet packet )
		{
			this._root.CloseModalWait();
			_DTO_reply dto = ( ( _PACKET_GENERIC_ACMD_REPLY )packet ).dto;
			PResult result = ( PResult )dto.result;
			PResultUtils.ShowAlter( result );
		}

		private void OnBeginFightResult( Packet packet )
		{
			this._root.CloseModalWait();
			_DTO_reply dto = ( ( _PACKET_GENERIC_ACMD_REPLY )packet ).dto;
			PResult result = ( PResult )dto.result;
			PResultUtils.ShowAlter( result );
		}

		private void OnAddFightReadyResult( Packet packet )
		{
			this._root.CloseModalWait();
			_DTO_reply dto = ( ( _PACKET_GENERIC_ACMD_REPLY )packet ).dto;
			PResult result = ( PResult )dto.result;
			PResultUtils.ShowAlter( result );
			if ( result != PResult.SUCCESS )
				this._root["readyBtn"].asButton.selected = false;
		}

		private void OnRemoveFightReadyResult( Packet packet )
		{
			this._root.CloseModalWait();
			_DTO_reply dto = ( ( _PACKET_GENERIC_ACMD_REPLY )packet ).dto;
			PResult result = ( PResult )dto.result;
			PResultUtils.ShowAlter( result );
			if ( result != PResult.SUCCESS )
				this._root["readyBtn"].asButton.selected = true;
		}

		private void OnRoomInfoResult( Packet packet )
		{
			this._root.CloseModalWait();
			_DTO_reply dto = ( ( _PACKET_GENERIC_ACMD_REPLY )packet ).dto;
			PResult result = ( PResult )dto.result;
			PResultUtils.ShowAlter( result );
		}
	}
}