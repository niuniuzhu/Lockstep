using System;
using System.Collections.Generic;

namespace Logic.Misc
{
	public class StackPool<T> where T : new()
	{
		private readonly Stack<T> _pool = new Stack<T>();

		public int length => this._pool.Count;

		public T Get()
		{
			return this._pool.Count > 0 ? this._pool.Pop() : new T();
		}

		public void Release( T element )
		{
			if ( this._pool.Count > 0 && ReferenceEquals( this._pool.Peek(), element ) )
				throw new Exception( "Internal error. Trying to destroy object that is already released to pool." );
			this._pool.Push( element );
		}

		public void Clear()
		{
			this._pool.Clear();
		}
	}
}