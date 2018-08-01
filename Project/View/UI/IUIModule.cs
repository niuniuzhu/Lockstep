namespace View.UI
{
	public interface IUIModule
	{
		void Dispose();

		void Enter( object param );

		void Leave();

		void Update();
	}
}