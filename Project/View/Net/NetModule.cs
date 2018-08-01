using Core.Misc;
using Core.Net;
using Core.Net.Protocol;
using Protocol.Gen;
using Logger = Core.Misc.Logger;

namespace View.Net
{
	public delegate void CMDHandler( Packet packet );

	public class NetModule
	{
		private static NetModule _instance;
		public static NetModule instance => _instance;

		private const string NETWORK_NAME = "client";

		public static NetModule Initialize( NetworkManager.PType protocolType )
		{
			if ( _instance != null )
				return _instance;
			_instance = new NetModule( protocolType );
			return _instance;
		}

		public event SocketEventHandler OnSocketEvent;

		private readonly CMDListener _qcmdListener = new CMDListener();
		private readonly CMDListener _acmdListener = new CMDListener();

		private NetModule( NetworkManager.PType protocolType )
		{
			if ( protocolType == NetworkManager.PType.Kcp )
				NetworkManager.SetupKCP();
			NetworkManager.CreateClient( NETWORK_NAME, protocolType );
			NetworkManager.AddClientEventHandler( NETWORK_NAME, this.ProcessClientEvent );
		}

		private void ProcessClientEvent( SocketEvent e )
		{
			switch ( e.type )
			{
				case SocketEvent.Type.Close:
					this.HandleClientClosed( e.msg );
					break;

				case SocketEvent.Type.Receive:
					this.ProcessDataReceived( e.packet );
					break;
			}
			this.OnSocketEvent?.Invoke( e );
		}

		public void Connect( string ip, int port )
		{
			NetworkManager.Connect( NETWORK_NAME, ip, port );
		}

		public void Dispose()
		{
			NetworkManager.Dispose();
		}

		private void HandleClientClosed( string message )
		{
		}

		private void ProcessDataReceived( Packet packet )
		{
			Logger.Net( $"<color=\"#00FFFF\">Received [packet:{packet}], time:{TimeUtils.GetLocalTime( TimeUtils.utcTime )}</color>" );

			if ( packet == null )
				return;

			bool isQCMD = packet.module == Module.GENERIC && packet.command == Command.ACMD_REPLY;
			if ( isQCMD )
			{
				_DTO_reply dto = ( ( _PACKET_GENERIC_ACMD_REPLY )packet ).dto;
				this._qcmdListener.Invoke( NetworkHelper.EncodePacketID( dto.src_module, dto.src_cmd ), packet );
			}
			else
				this._acmdListener.Invoke( NetworkHelper.EncodePacketID( packet.module, packet.command ), packet );
		}

		public void Update( long deltaTime )
		{
			NetworkManager.Update( deltaTime );
		}

		public void Send( Packet packet )
		{
			Logger.Net( $"<color=\"#CCFF33\">Send [packet:{packet}], time:{TimeUtils.utcTime}</color>" );

			NetworkManager.Send( NETWORK_NAME, packet );
		}

		public void AddQCMDListener( byte module, ushort cmd, CMDHandler handler )
		{
			this._qcmdListener.Add( NetworkHelper.EncodePacketID( module, cmd ), handler );
		}

		public void RemoveQCMDListener( byte module, ushort cmd, CMDHandler handler )
		{
			this._qcmdListener.Remove( NetworkHelper.EncodePacketID( module, cmd ), handler );
		}

		public void AddACMDListener( byte module, ushort cmd, CMDHandler handler )
		{
			this._acmdListener.Add( NetworkHelper.EncodePacketID( module, cmd ), handler );
		}

		public void RemoveACMDListener( byte module, ushort cmd, CMDHandler handler )
		{
			this._acmdListener.Remove( NetworkHelper.EncodePacketID( module, cmd ), handler );
		}
	}
}
