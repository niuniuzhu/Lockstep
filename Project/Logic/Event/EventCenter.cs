using System.Collections.Generic;
using Core.Structure;

namespace Logic.Event
{
	public static class EventCenter
	{
		public delegate void EventHandler( BaseEvent e );

		private static readonly Dictionary<int, List<EventHandler>> HANDLERS = new Dictionary<int, List<EventHandler>>();
		private static readonly SwitchQueue<BaseEvent> PENDING_LIST = new SwitchQueue<BaseEvent>();

		public static void AddListener( int type, EventHandler handler )
		{
			if ( !HANDLERS.ContainsKey( type ) )
				HANDLERS.Add( type, new List<EventHandler>() );
			List<EventHandler> list = HANDLERS[type];
			list.Add( handler );
		}

		public static void RemoveListener( int type, EventHandler handler )
		{
			if ( !HANDLERS.ContainsKey( type ) )
				return;
			List<EventHandler> list = HANDLERS[type];
			bool result = list.Remove( handler );
			if ( !result )
				return;
			if ( list.Count == 0 )
				HANDLERS.Remove( type );
		}

		public static void BeginInvoke( BaseEvent e )
		{
			PENDING_LIST.Push( e );
		}

		public static void Invoke( BaseEvent e )
		{
			if ( HANDLERS.TryGetValue( e.type, out List<EventHandler> notifyHandlers ) )
			{
				int count = notifyHandlers.Count;
				for ( int i = 0; i < count; i++ )
					notifyHandlers[i].Invoke( e ); //注意调用时可能会再有事件加入列表，所以要使用while(PENDING_LIST.Count>0)
			}
			e.Release();
		}

		public static void Sync()
		{
			PENDING_LIST.Switch();
			while ( !PENDING_LIST.isEmpty )
			{
				BaseEvent e = PENDING_LIST.Pop();
				Invoke( e );
			}
		}

		public static void Clear()
		{
			PENDING_LIST.Clear();
		}
	}
}