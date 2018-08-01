using System;
using System.Collections.Generic;

namespace Logic.BuffStateImpl
{
	public static class BuffStatePool
	{
		private static readonly Dictionary<Type, Queue<BSBase>> TYPE_TO_OBJECTS = new Dictionary<Type, Queue<BSBase>>();

		public static T Pop<T>() where T : BSBase, new()
		{
			Queue<BSBase> buffs;
			BSBase obj;
			Type type = typeof( T );
			if ( !TYPE_TO_OBJECTS.TryGetValue( type, out buffs ) )
				obj = new T();
			else
			{
				obj = buffs.Dequeue();
				if ( buffs.Count == 0 )
					TYPE_TO_OBJECTS.Remove( type );
			}
			return ( T ) obj;
		}

		public static void Push( BSBase obj )
		{
			Queue<BSBase> objs;
			if ( !TYPE_TO_OBJECTS.TryGetValue( obj.GetType(), out objs ) )
			{
				objs = new Queue<BSBase>();
				TYPE_TO_OBJECTS[obj.GetType()] = objs;
			}
			objs.Enqueue( obj );
		}
	}
}