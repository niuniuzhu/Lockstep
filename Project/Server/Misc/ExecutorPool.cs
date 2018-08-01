using System.Threading;

namespace Server.Misc
{
	/// <summary>
	/// 单线程事件委托
	/// </summary>
	public delegate void ExecutorDelegate();
	/// <summary>
	/// 单线程处理对象 将所有事物处理调用 同过此处调用
	/// </summary>
	public class ExecutorPool
	{
		private static ExecutorPool _instance;
		/// <summary>
		/// 线程同步锁
		/// </summary>
		readonly Mutex _tex = new Mutex();

		/// <summary>
		/// 单例对象
		/// </summary>
		public static ExecutorPool instance => _instance ?? ( _instance = new ExecutorPool() );

		/// <summary>
		/// 单线程处理逻辑
		/// </summary>
		/// <param name="d"></param>
		public void Execute( ExecutorDelegate d )
		{
			lock ( this )
			{
				this._tex.WaitOne();
				d();
				this._tex.ReleaseMutex();
			}
		}
	}
}
