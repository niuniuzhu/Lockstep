namespace WorldEditor
{
	public interface IDataLoader
	{
		Core.Misc.Map Load( string file, out string error );

		bool Save( Core.Misc.Map map, string file, out string error );
	}
}