using Logic.Property;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace AttrGenerator
{
	static class Program
	{
		static void Main( string[] args )
		{
			string savePath = "../../../Client.Logic/Property/";
			string filename = "__PropertyBase.cs";

			if ( !Directory.Exists( savePath ) )
				Directory.CreateDirectory( savePath );

			string str = Gen( typeof( Attr ) );

			File.WriteAllText( Path.Combine( savePath, filename ), str, Encoding.UTF8 );

			Console.WriteLine( str );
			Console.ReadLine();
		}

		static string Gen( Type t )
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine( "namespace Client.Logic.Property\n{" );

			FieldInfo[] fieldInfos = t.GetFields();

			Dictionary<string, StringBuilder> clsNameToSb = new Dictionary<string, StringBuilder>();
			foreach ( FieldInfo fieldInfo in fieldInfos )
				GetClsNames( fieldInfo, ref clsNameToSb );

			Dictionary<string, StringBuilder> ctrToSb = new Dictionary<string, StringBuilder>();
			foreach ( KeyValuePair<string, StringBuilder> kv in clsNameToSb )
				ctrToSb[kv.Key] = new StringBuilder();

			foreach ( FieldInfo fieldInfo in fieldInfos )
				GenProps( fieldInfo, ref clsNameToSb );

			foreach ( FieldInfo fieldInfo in fieldInfos )
				GenInitializations( fieldInfo, ref ctrToSb );

			foreach ( KeyValuePair<string, StringBuilder> kv in clsNameToSb )
			{
				sb.AppendLine( $"\tpublic partial class {kv.Key}\n\t{{" );
				sb.Append( kv.Value );
				sb.AppendLine( $"\n\t\tpublic void SetDefault()\n\t\t{{" );
				sb.Append( ctrToSb[kv.Key] );
				sb.AppendLine( "\t\t}" );
				sb.AppendLine( "\t}" );
			}

			sb.Append( "\n}\n" );
			return sb.ToString();
		}

		private static void GetClsNames( FieldInfo fieldInfo, ref Dictionary<string, StringBuilder> clsNameToSb )
		{
			AttrDescAttribute attr = fieldInfo.GetCustomAttribute<AttrDescAttribute>( false );
			if ( attr == null )
				return;
			foreach ( string attrName in attr.names )
			{
				if ( !clsNameToSb.ContainsKey( attrName ) )
					clsNameToSb[attrName] = new StringBuilder();
			}
		}

		static void GenProps( FieldInfo fieldInfo, ref Dictionary<string, StringBuilder> clsNameToSb )
		{
			AttrDescAttribute attr = fieldInfo.GetCustomAttribute<AttrDescAttribute>( false );
			if ( attr == null )
				return;
			string t = !attr.type.IsNested ? attr.type.FullName : attr.type.ReflectedType + "." + attr.type.Name;
			string field;
			if ( string.IsNullOrEmpty( attr.field ) )
				field = fieldInfo.Name[0].ToString().ToLower() + fieldInfo.Name.Substring( 1 );
			else
				field = attr.field;
			foreach ( string attrName in attr.names )
			{
				StringBuilder sb = clsNameToSb[attrName];
				sb.Append( $"\t\tpublic {t} " );
				sb.AppendLine( $"{field} => ( {t} )this._attrMap[{fieldInfo.DeclaringType}.{fieldInfo.Name}];" );
			}
		}

		static void GenInitializations( FieldInfo fieldInfo, ref Dictionary<string, StringBuilder> ctrToSb )
		{
			AttrDescAttribute attr = fieldInfo.GetCustomAttribute<AttrDescAttribute>( false );
			if ( attr == null )
				return;

			string t = !attr.type.IsNested ? attr.type.FullName : attr.type.ReflectedType + "." + attr.type.Name;
			int count = attr.names.Length;
			for ( int i = 0; i < count; i++ )
			{
				string attrName = attr.names[i];
				string defaultValue = !string.IsNullOrEmpty( attr.defaultVal )
										  ? attr.defaultVal
										  : ( attr.defaultVals != null ? attr.defaultVals[i] : $"default( {t} )" );
				StringBuilder sb = ctrToSb[attrName];
				sb.AppendLine( $"\t\t\tthis._attrMap[{fieldInfo.DeclaringType}.{fieldInfo.Name}] = {defaultValue};" );
			}
		}
	}
}
