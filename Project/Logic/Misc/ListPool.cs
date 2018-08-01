using System.Collections.Generic;

namespace Logic.Misc
{
	public static class ListPool<T>
	{
		private static readonly StackPool<List<T>> POOL = new StackPool<List<T>>();

		public static List<T> Get()
		{
			return POOL.Get();
		}

		public static void Release( List<T> toRelease )
		{
			POOL.Release( toRelease );
		}
	}
}