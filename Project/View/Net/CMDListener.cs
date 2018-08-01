using System.Collections.Generic;
using Core.Net.Protocol;

namespace View.Net
{
	internal sealed class CMDListener
	{
		private readonly Dictionary<int, CMDHandler> _listenerTable = new Dictionary<int, CMDHandler>();

		internal void Add( int key, CMDHandler handler )
		{
			if ( this._listenerTable.TryGetValue( key, out CMDHandler handler2 ) )
			{
				handler2 -= handler;
				handler2 += handler;
			}
			else
				handler2 = handler;
			this._listenerTable[key] = handler2;
		}

		internal bool Remove( int key )
		{
			if ( !this._listenerTable.ContainsKey( key ) )
				return false;
			this._listenerTable.Remove( key );
			return true;
		}

		internal bool Remove( int key, CMDHandler handler )
		{
			if ( this._listenerTable.TryGetValue( key, out CMDHandler handler2 ) )
			{
				handler2 -= handler;
				if ( handler2 == null )
					this._listenerTable.Remove( key );
				else
					this._listenerTable[key] = handler2;
				return true;
			}
			return false;
		}

		internal void Clear()
		{
			this._listenerTable.Clear();
		}

		internal void Invoke( int key, Packet packet )
		{
			if ( this._listenerTable.TryGetValue( key, out CMDHandler handler2 ) )
				handler2.Invoke( packet );
		}
	}
}