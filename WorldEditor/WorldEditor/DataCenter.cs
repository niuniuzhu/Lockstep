using WorldEditor.DataImpl;

namespace WorldEditor
{
	public static class DataCenter
	{
		public static RootData root { get; private set; }

		public static void CreateDataRoot( Core.Misc.Map data )
		{
			root = new RootData();
			root.FromJson( data );
		}

		public static Core.Misc.Map SaveFromRoot()
		{
			return root.ToJson();
		}
	}
}