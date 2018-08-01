using Logic.Misc;

namespace Logic
{
	public class BTest
	{
		public virtual void Foo1( string s )
		{
			LLogger.Log( "s:" + s );
		}

		public virtual void Foo( int i )
		{
			LLogger.Log( "i:" + i );
		}
	}

	public class Test : BTest
	{
		public void TestDel( TimerEntry.TimerHandler fun )
		{
			fun.Invoke( 123, 2f, null );
		}

		public void Foo( bool b )
		{
			LLogger.Log( "b:" + b );
		}
	}
}