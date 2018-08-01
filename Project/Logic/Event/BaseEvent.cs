namespace Logic.Event
{
	public abstract class BaseEvent
	{
		public int type;

		public void BeginInvoke()
		{
			EventCenter.BeginInvoke( this );
		}

		public void Invoke()
		{
			EventCenter.Invoke( this );
		}

		public abstract void Release();
	}
}