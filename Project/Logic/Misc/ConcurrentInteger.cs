using System;
using System.Threading;

namespace Logic.Misc
{
	public class ConcurrentInteger
	{
		private int _value;
		private readonly Mutex _tex = new Mutex();
		private readonly object _lockObj = new Object();

		public ConcurrentInteger()
		{
		}

		public ConcurrentInteger( int value )
		{
			lock ( this._lockObj )
			{
				this._value = value;
			}
		}

		public int Get()
		{
			return this._value;
		}

		public int GetAndAdd()
		{
			lock ( this._lockObj )
			{
				this._tex.WaitOne();
				this._value++;
				this._tex.ReleaseMutex();
				return this._value;
			}
		}

		public int GetAndReduce()
		{
			lock ( this._lockObj )
			{
				this._tex.WaitOne();
				this._value--;
				this._tex.ReleaseMutex();
				return this._value;
			}
		}

		public void Reset()
		{
			lock ( this._lockObj )
			{
				this._tex.WaitOne();
				this._value = 0;
				this._tex.ReleaseMutex();
			}
		}
	}
}
