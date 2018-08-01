using System.Collections.Generic;
using View.Controller;

namespace View.Graphics
{
	public class GraphicManager
	{
		private readonly Dictionary<string, GraphicPool> _pool = new Dictionary<string, GraphicPool>();

		public Graphic Get( VEntity owner, string id )
		{
			if ( !this._pool.TryGetValue( id, out GraphicPool pool ) )
			{
				pool = new GraphicPool( id );
				this._pool[id] = pool;
			}
			return pool.Get( owner );
		}

		public void Release( Graphic graphic )
		{
			GraphicPool pool = this._pool[graphic.id];
			pool.Release( graphic );
		}

		public void Dispose()
		{
			foreach ( KeyValuePair<string, GraphicPool> kv in this._pool )
				kv.Value.Dispose();
			this._pool.Clear();
		}

		class GraphicPool
		{
			public string id { get; private set; }

			private readonly Queue<Graphic> _queue = new Queue<Graphic>();

			public GraphicPool( string id )
			{
				this.id = id;
			}

			public Graphic Get( VEntity owner )
			{
				Graphic graphic = this._queue.Count > 0 ? this._queue.Dequeue() : new Graphic( this.id );
				graphic.OnSpawn( owner );
				return graphic;
			}

			public void Release( Graphic graphic )
			{
				graphic.OnDespawn();
				this._queue.Enqueue( graphic );
			}

			public void Dispose()
			{
				foreach ( Graphic graphic in this._queue )
					graphic.Dispose();
				this._queue.Clear();
			}
		}
	}
}