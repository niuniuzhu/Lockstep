using System.Collections.Generic;

namespace Server.Misc
{
	public class CommandArgs
	{
		public Dictionary<string, string> argPairs { get; } = new Dictionary<string, string>();

		public List<string> Params { get; } = new List<string>();
	}

	public class CommandLine
	{
		public static CommandArgs Parse( string[] args )
		{
			char[] kEqual = { '=' };
			char[] kArgStart = { '-', '\\' };
			CommandArgs ca = new CommandArgs();
			int ii = -1;
			string token = NextToken( args, ref ii );
			while ( token != null )
			{
				if ( IsArg( token ) )
				{
					string arg = token.TrimStart( kArgStart ).TrimEnd( kEqual );
					string value = null;
					if ( arg.Contains( "=" ) )
					{
						string[] r = arg.Split( kEqual, 2 );
						if ( r.Length == 2 && r[1] != string.Empty )
						{
							arg = r[0];
							value = r[1];
						}
					}

					while ( value == null )
					{
						string next = NextToken( args, ref ii );
						if ( next != null )
						{
							if ( IsArg( next ) )
							{
								ii--;
								value = "true";
							}
							else if ( next != "=" )
							{
								value = next.TrimStart( kEqual );
							}
						}
					}

					ca.argPairs.Add( arg, value );
				}
				else if ( token != string.Empty )
				{
					ca.Params.Add( token );
				}
				token = NextToken( args, ref ii );
			}
			return ca;
		}

		static bool IsArg( string arg )
		{
			return ( arg.StartsWith( "-" ) || arg.StartsWith( "\\" ) );
		}


		static string NextToken( string[] args, ref int ii )
		{
			ii++; // move to next token
			while ( ii < args.Length )
			{
				string cur = args[ii].Trim();
				if ( cur != string.Empty )
				{
					return cur;
				}
				ii++;
			}
			return null;
		}
	}
}