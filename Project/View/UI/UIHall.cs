using Core.Net.Protocol;
using FairyUGUI.Event;
using FairyUGUI.UI;
using Protocol;
using Protocol.Gen;
using UnityEngine;
using View.Net;
using View.UI.Wins;

namespace View.UI
{
	public class UIHall : IUIModule
	{
		private GComponent _root;
		private CreateRoomWin _createWin;
		private GComponent _joinPopup;

		public UIHall()
		{
			UIPackage.AddPackage( "UI/hall" );
		}

		public void Dispose()
		{
		}

		public void Enter( object param )
		{
			NetModule.instance.AddACMDListener( Module.HALL, Command.ACMD_BRO_ROOM_CREATED, this.OnRoomCreated );
			NetModule.instance.AddACMDListener( Module.HALL, Command.ACMD_BRO_ROOM_DESTROIED, this.OnRoomDestroy );
			NetModule.instance.AddACMDListener( Module.HALL, Command.ACMD_ROOM_LIST, this.OnRoomList );
			NetModule.instance.AddACMDListener( Module.HALL, Command.ACMD_JOIN_ROOM, this.OnEnterRoom );

			NetModule.instance.AddQCMDListener( Module.HALL, Command.QCMD_CREATE_ROOM, this.OnCreateRoomResult );
			NetModule.instance.AddQCMDListener( Module.HALL, Command.QCMD_JOIN_ROOM, this.OnJoinRoomResult );

			this._root = UIPackage.CreateObject( "hall", "Main" ).asCom;
			GRoot.inst.AddChild( this._root );
			this._root.size = GRoot.inst.size;

			this._createWin = new CreateRoomWin();

			this._joinPopup = UIPackage.CreateObject( "hall", "弹出" ).asCom;
			this._joinPopup["n0"].asButton.onClick.Add( this.OnJoinRoomBtnClick );

			GButton createBtn = this._root["create"].asButton;
			createBtn.onClick.Add( this.OnCreateBtnClick );

			GList list = this._root["list"].asList;
			list.onClickItem.Add( this.OnClickItem );

			this._root.ShowModalWait();

			NetModule.instance.Send( ProtocolManager.PACKET_HALL_QCMD_ROOM_LIST( 255, 0 ) );
		}

		public void Leave()
		{
			NetModule.instance.RemoveACMDListener( Module.HALL, Command.ACMD_BRO_ROOM_CREATED, this.OnRoomCreated );
			NetModule.instance.RemoveACMDListener( Module.HALL, Command.ACMD_BRO_ROOM_DESTROIED, this.OnRoomDestroy );
			NetModule.instance.RemoveACMDListener( Module.HALL, Command.ACMD_ROOM_LIST, this.OnRoomList );
			NetModule.instance.RemoveACMDListener( Module.HALL, Command.ACMD_JOIN_ROOM, this.OnEnterRoom );

			NetModule.instance.RemoveQCMDListener( Module.HALL, Command.QCMD_CREATE_ROOM, this.OnCreateRoomResult );
			NetModule.instance.RemoveQCMDListener( Module.HALL, Command.QCMD_JOIN_ROOM, this.OnJoinRoomResult );

			GList list = this._root["list"].asList;
			list.RemoveChildrenToPool();

			if ( this._createWin != null )
			{
				this._createWin.Dispose();
				this._createWin = null;
			}

			if ( this._joinPopup != null )
			{
				this._joinPopup.Dispose();
				this._joinPopup = null;
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

		private void OnCreateBtnClick( EventContext context )
		{
			this._createWin.Show( this._root );
		}

		private void OnClickItem( EventContext context )
		{
			GRoot.inst.ShowPopup( this._joinPopup, null, PopupDirection.TouchPosition, PopupConstraint.Any );
			PointerEventData lastPointerEventData = EventSystem.instance.pointerInput.GetLastPointerEventData( PointerInput.K_MOUSE_LEFT_ID );
			Vector2 pos = lastPointerEventData.pressPosition;
			this._joinPopup.position = GRoot.inst.ScreenToLocal( pos );

			GObject item = this._root["list"].asList.firstSelectedItem;
			this._joinPopup.data = item.data;
		}

		private void OnJoinRoomBtnClick( EventContext context )
		{
			_DTO_room_info sdto = ( _DTO_room_info )this._joinPopup.data;

			this._root.ShowModalWait();

			NetModule.instance.Send( ProtocolManager.PACKET_HALL_QCMD_JOIN_ROOM( sdto.roomId ) );
		}

		private void OnCreateRoomResult( Packet packet )
		{
			this._createWin.CloseModalWait();

			_DTO_reply dto = ( ( _PACKET_GENERIC_ACMD_REPLY )packet ).dto;
			PResult result = ( PResult )dto.result;
			PResultUtils.ShowAlter( result );
			if ( result == PResult.SUCCESS )
				this._createWin.Hide();
		}

		private void OnJoinRoomResult( Packet packet )
		{
			this._root.CloseModalWait();

			_DTO_reply dto = ( ( _PACKET_GENERIC_ACMD_REPLY )packet ).dto;
			PResult result = ( PResult )dto.result;
			PResultUtils.ShowAlter( result );
		}

		private void OnRoomList( Packet packet )
		{
			this._root.CloseModalWait();

			GRoot.inst.HidePopup();

			GList list = this._root["list"].asList;

			_DTO_room_list dto = ( ( _PACKET_HALL_ACMD_ROOM_LIST )packet ).dto;
			_DTO_room_info[] rooms = dto.rs;
			int count = rooms.Length;
			for ( int i = 0; i < count; i++ )
			{
				GComponent item = list.AddItemFromPool().asCom;
				_DTO_room_info roomDTO = rooms[i];
				item["id"].asTextField.text = string.Empty + roomDTO.roomId;
				item["name"].asTextField.text = roomDTO.name;
				item.data = roomDTO;
			}
		}

		private void OnRoomCreated( Packet packet )
		{
			GList list = this._root["list"].asList;
			GComponent item = list.AddItemFromPoolAt( 0 ).asCom;

			_DTO_room_info roomDTO = ( ( _PACKET_HALL_ACMD_BRO_ROOM_CREATED )packet ).dto;
			item["id"].asTextField.text = string.Empty + roomDTO.roomId;
			item["name"].asTextField.text = roomDTO.name;
			item.data = roomDTO;
		}

		private void OnRoomDestroy( Packet packet )
		{
			_DTO_int sdto = ( ( _PACKET_HALL_ACMD_BRO_ROOM_DESTROIED )packet ).dto;
			if ( this._joinPopup.parent != null )
			{
				_DTO_room_info dto = ( _DTO_room_info )this._joinPopup.data;
				if ( dto.roomId == sdto.value )
					GRoot.inst.HidePopup();
			}
			GList list = this._root["list"].asList;
			int numItems = list.numItems;
			for ( int i = 0; i < numItems; i++ )
			{
				GComponent item = list.GetChildAt( i ).asCom;
				_DTO_room_info roomDTO = ( _DTO_room_info )item.data;
				if ( roomDTO.roomId == sdto.value )
				{
					list.RemoveChildToPoolAt( i );
					break;
				}
			}
		}

		private void OnEnterRoom( Packet packet )
		{
			_DTO_int dto = ( ( _PACKET_HALL_ACMD_JOIN_ROOM )packet ).dto;
			UIManager.EnterRoom( dto.value );
		}
	}
}