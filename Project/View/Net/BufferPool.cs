using System.Collections.Generic;
using Core.Net;

namespace View.Net
{
	public static class BufferPool
	{
		private static readonly Stack<StreamBuffer> POOL = new Stack<StreamBuffer>();

		public static StreamBuffer Pop()
		{
			if ( POOL.Count == 0 )
				return new StreamBuffer();
			return POOL.Pop();
		}

		public static void Push( StreamBuffer buffer )
		{
			buffer.Clear();
			POOL.Push( buffer );
		}
	}
}