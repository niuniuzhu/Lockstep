using System;
using System.Collections.Generic;

namespace Logic.Misc
{
	public static class Utils
	{
		public static string GetIDFromRID( string rid )
		{
			int pos = rid.IndexOf( "@", StringComparison.Ordinal );
			string id = pos != -1 ? rid.Substring( 0, pos ) : rid;
			return id;
		}

		public static void Copy<T1, T2>( this Dictionary<T1, T2> self, Dictionary<T1, T2> other )
		{
			foreach ( KeyValuePair<T1, T2> kv in other )
				self[kv.Key] = kv.Value;
		}
	}
}