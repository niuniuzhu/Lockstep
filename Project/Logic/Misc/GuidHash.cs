using System;
using ICSharpCode.SharpZipLib.Checksums;

namespace Logic.Misc
{
	public static class GuidHash
	{
		private static readonly Crc32 CRC32 = new Crc32();
		private static readonly ConcurrentInteger INDEX = new ConcurrentInteger();

		public static string NextS()
		{
			long crc = NextL();
			return $"{crc.ToString( "x" )}_{INDEX.GetAndAdd()}";
		}

		public static long NextL()
		{
			byte[] guid = GetGuid();
			return Hash( guid );
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