using System;

namespace Server.Misc
{
	internal static class Utils
	{
		public static string MakeRidFromID( string id )
		{
			return id + "@" + GuidHash.Get();
		}

		public static string GetIDFromRID( string rid )
		{
			int pos = rid.IndexOf( "@", StringComparison.Ordinal );
			string id = pos != -1 ? rid.Substring( 0, pos ) : rid;
			return id;
		}
	}
}