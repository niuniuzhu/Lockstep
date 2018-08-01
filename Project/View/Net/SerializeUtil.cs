using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace View.Net
{
	public static class SerializeUtil
	{
		public static byte[] Encode( object value )
		{
			MemoryStream ms = new MemoryStream();//创建编码解码的内存流对象
			BinaryFormatter bw = new BinaryFormatter();//二进制流序列化对象
			//将obj对象序列化成二进制数据 写入到 内存流
			bw.Serialize( ms, value );
			byte[] result = new byte[ms.Length];
			//将流数据 拷贝到结果数组
			Buffer.BlockCopy( ms.GetBuffer(), 0, result, 0, ( int )ms.Length );
			ms.Close();
			return result;
		}

		public static object Decode( byte[] value )
		{
			MemoryStream ms = new MemoryStream( value );//创建编码解码的内存流对象 并将需要反序列化的数据写入其中
			BinaryFormatter bw = new BinaryFormatter();//二进制流序列化对象
			//将流数据反序列化为obj对象
			object result = bw.Deserialize( ms );
			ms.Close();
			return result;
		}
	}
}