using System;
using System.Collections.Generic;

namespace Logic.BuffImpl
{
	public static class BuffImplPool
	{
		private static readonly Dictionary<Type, Queue<BIBase>> TYPE_TO_OBJECTS = new Dictionary<Type, Queue<BIBase>>();

		public static T Pop<T>() where T : BIBase, new()
		{
			Queue<BIBase> buffs;
			BIBase obj;
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

		public static void Push( BIBase obj )
		{
			Queue<BIBase> objs;
			if ( !TYPE_TO_OBJECTS.TryGetValue( obj.GetType(), out objs ) )
			{
				objs = new Queue<BIBase>();
				TYPE_TO_OBJECTS[obj.GetType()] = objs;
			}
			objs.Enqueue( obj );
		}
	}
}