namespace View.Input
{
	public static class UIEventProxy
	{
		public delegate bool UIRecognizer();

		public static UIRecognizer IsOverUIObject; 
	}
}