using System;
using System.Collections.Generic;

namespace View.BuffStateImpl
{
	public static class VBuffStatePool
	{
		private static readonly Dictionary<Type, Queue<VBSBase>> TYPE_TO_OBJECTS = new Dictionary<Type, Queue<VBSBase>>();

		public static T Pop<T>() where T : VBSBase, new()
		{
			Queue<VBSBase> buffs;
			VBSBase obj;
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

		public static void Push( VBSBase obj )
		{
			Queue<VBSBase> objs;
			if ( !TYPE_TO_OBJECTS.TryGetValue( obj.GetType(), out objs ) )
			{
				objs = new Queue<VBSBase>();
				TYPE_TO_OBJECTS[obj.GetType()] = objs;
			}
			objs.Enqueue( obj );
		}
	}
}