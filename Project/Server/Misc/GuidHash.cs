using System;
using ICSharpCode.SharpZipLib.Checksums;

namespace Server.Misc
{
	public static class GuidHash
	{
		private static readonly Crc32 CRC32 = new Crc32();
		private static readonly ConcurrentInteger INDEX = new ConcurrentInteger();

		public static string Get()
		{
			return Next();
		}

		private static string Next()
		{
			byte[] guid = GetGuid();
			long crc = Hash( guid );
			return string.Format( "{0}_{1}", crc.ToString( "x" ), INDEX.GetAndAdd() );
		}

		private static byte[] GetGuid()
		{
			Guid guid = Guid.NewGuid();
			return guid.ToByteArray();
		}

		private static long Hash( byte[] bytes )
		{
			CRC32.Reset();
			CRC32.Update( bytes );
			return CRC32.Value;
		}
	}
}