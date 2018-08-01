using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Core.Misc;

namespace Logic.Model
{
	public static class Defs
	{
		private static Hashtable _map;
		private static readonly Dictionary<string, string> L_SCRIPTS = new Dictionary<string, string>();
		private static readonly Regex REGEX = new Regex( @"\[(\D\d+)\]" );

		public static void Init( string json, string lscript )
		{
			_map = ( Hashtable ) MiniJSON.JsonDecode( json );
			ParseScripts( lscript );
		}

		public static Hashtable Get( string key )
		{
			return ( Hashtable ) _map[key];
		}

		public static Hashtable GetMap( string id )
		{
			return _map.GetMap( "maps" ).GetMap( id );
		}

		public static Hashtable GetEntity( string id )
		{
			return _map.GetMap( "entities" ).GetMap( id );
		}

		public static Hashtable GetSkill( string id )
		{
			return _map.GetMap( "skills" ).GetMap( id );
		}

		public static Hashtable GetBuff( string id )
		{
			return _map.GetMap( "buffs" ).GetMap( id );
		}

		public static Hashtable GetBuffState( string id )
		{
			return _map.GetMap( "states" ).GetMap( id );
		}

		public static string GetScript( string id )
		{
			string s;
			return L_SCRIPTS.TryGetValue( id, out s ) ? s : null;
		}

		private static void ParseScripts( string lscript )
		{
			string line;
			string id = string.Empty;
			StringReader reader = new StringReader( lscript );
			while ( ( line = reader.ReadLine() ) != null )
			{
				Match match = REGEX.Match( line );
				if ( match.Success )
				{
					id = match.Groups[1].Value;
					L_SCRIPTS[id] = string.Empty;
				}
				else
				{
					if ( !string.IsNullOrEmpty( line ) )
						L_SCRIPTS[id] += line + System.Environment.NewLine;
				}
			}
		}
	}
}