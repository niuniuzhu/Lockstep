using Core.Net;
using Core.Net.Protocol;

namespace Server
{
	public abstract class AbsHandlerCenter
	{
		/// <summary>
		/// 客户端连接
		/// </summary>
		/// <param name="token">连接的客户端对象</param>
		public abstract void ClientConnect( IUserToken token );
		/// <summary>
		/// 收到客户端消息
		/// </summary>
		/// <param name="token">发送消息的客户端对象</param>
		/// <param name="model">消息内容</param>
		public abstract void ProcessMessage( IUserToken token, Packet model );
		/// <summary>
		/// 客户端断开连接
		/// </summary>
		/// <param name="token">断开的客户端对象</param>
		/// <param name="error">断开的错误信息</param>
		public abstract void ClientClose( IUserToken token ); 
	}
}