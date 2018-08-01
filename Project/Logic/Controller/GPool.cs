using System;
using System.Collections.Generic;

namespace Logic.Controller
{
	public class GPool
	{
		private readonly Dictionary<Type, Queue<GPoolObject>> _typeToObjects = new Dictionary<Type, Queue<GPoolObject>>();
		private readonly Dictionary<Type, List<GPoolObject>> _referencing = new Dictionary<Type, List<GPoolObject>>();

		public bool Pop<T>( out T obj ) where T : GPoolObject, new()
		{
			Type type = typeof( T );
			if ( !this._typeToObjects.TryGetValue( type, out Queue<GPoolObject> objs ) )
			{
				objs = new Queue<GPoolObject>();
				this._typeToObjects[type] = objs;
				this._referencing[type] = new List<GPoolObject>();
			}
			if ( objs.Count == 0 )
			{
				List<GPoolObject> referencing = this._referencing[type];
				int count = referencing.Count;
				for ( int i = 0; i < count; i++ )
				{
					GPoolObject o = referencing[i];
					if ( o.reference <= 0 )
					{
						objs.Enqueue( o );
						referencing.RemoveAt( i );
						--i;
						--count;
					}
				}
			}
			bool isNew = objs.Count == 0;
			obj = isNew ? new T() : ( T )objs.Dequeue();
			return isNew;
		}

		public void Push( GPoolObject obj )
		{
			if ( obj.reference <= 0 )
			{
				this._typeToObjects[obj.GetType()].Enqueue( obj );
			}
			else
			{
				this._referencing[obj.GetType()].Add( obj );
			}
		}

		public void Dispose()
		{
			foreach ( KeyValuePair<Type, Queue<GPoolObject>> kv in this._typeToObjects )
			{
				Queue<GPoolObject> queue = kv.Value;
				foreach ( GPoolObject obj in queue )
					obj.Dispose();
			}
			this._typeToObjects.Clear();
		}
	}
}