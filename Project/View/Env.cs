using System.Diagnostics;
using XLua;

namespace View
{
	public static class Env
	{
		public static int version = 0;

		public static bool isEditor;
		public static int platform;
		public static bool isRunning;

		public static long lag;
		public static long serverTime;
		public static long timeDiff;

		public static LuaEnv LUA_ENV;

		private static readonly Stopwatch STOPWATCH = new Stopwatch();

		public static long elapsed => STOPWATCH.ElapsedMilliseconds;

		internal static void StartTime()
		{
			STOPWATCH.Start();
		}
	}
}