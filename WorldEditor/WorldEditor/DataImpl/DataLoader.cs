using System;
using Core.Misc;

namespace WorldEditor.DataImpl
{
	public class DataLoader : IDataLoader
	{
		public Map Load( string file, out string error )
		{
			string text;
			try
			{
				text = System.IO.File.ReadAllText( file );
			}
			catch ( Exception e )
			{
				error = e.ToString();
				return null;
			}
			Map json = ( Map )MiniJSON.JsonDecode( text );

			error = string.Empty;
			return json;
		}

		public bool Save( Map map, string file, out string error )
		{
			string json = MiniJSON.JsonEncode( map, true );
			try
			{
				System.IO.File.WriteAllText( file, json );
			}
			catch ( Exception e )
			{
				error = e.ToString();
				return false;
			}
			error = string.Empty;
			return true;
		}
	}
}