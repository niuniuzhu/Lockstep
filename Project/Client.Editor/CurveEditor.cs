using UnityEditor;
using UnityEngine;
using View.Builtin;

namespace Client.Editor
{
	[CustomEditor( typeof( Curve ) )]
	public class CurveEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			Curve script = ( Curve ) this.target;
			if ( GUILayout.Button( "Set" ) )
			{
				script.UpdateVisual();
			}
			if ( GUILayout.Button( "Info" ) )
			{
				string str = script.LogInfo();
				Debug.Log( str );
				clipboard = str;
			}
		}

		private static string clipboard
		{
			get { return GUIUtility.systemCopyBuffer; }
			set { GUIUtility.systemCopyBuffer = value; }
		}
	}
}