using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Client.Editor
{
	public class Tools
	{
		[MenuItem( "Tools/Test" )]
		public static void CleanCache()
		{
		}

		[MenuItem( "Tools/Get forward" )]
		public static void GetForward()
		{
			if ( Selection.activeGameObject != null )
				Debug.Log( Selection.activeGameObject.transform.rotation * Vector3.right );
		}

		[MenuItem( "Tools/Shadow Off" )]
		public static void ShutdownShadows()
		{
			GameObject[] gos = Selection.gameObjects;
			foreach ( GameObject go in gos )
			{
				Renderer[] renderers = go.GetComponentsInChildren<Renderer>();
				foreach ( Renderer renderer in renderers )
				{
					renderer.shadowCastingMode = ShadowCastingMode.Off;
					renderer.receiveShadows = false;
					renderer.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
				}
				TrailRenderer[] trs = go.GetComponentsInChildren<TrailRenderer>();
				foreach ( TrailRenderer tr in trs )
				{
					tr.shadowCastingMode = ShadowCastingMode.Off;
					tr.receiveShadows = false;
					tr.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
				}
			}
		}

		[MenuItem( "Tools/Add spare" )]
		public static void AddSpare()
		{
			GameObject[] gos = Selection.gameObjects;
			foreach ( GameObject go in gos )
			{
				AddSpare( go.transform );
			}
		}

		private static void AddSpare( Transform model )
		{
			Transform foothold = new GameObject( "__Foothold" ).transform;
			foothold.position = new Vector3( 0f, 0.12f, 0f );
			foothold.SetParent( model, false );
			new GameObject( "__Overhead" ).transform.SetParent( model, false );
			new GameObject( "__HitPoint" ).transform.SetParent( model, false );
			Debug.Log( "__Foothold added" );
			Debug.Log( "__Overhead added" );
			Debug.Log( "__HitPoint added" );
			Transform[] children = model.GetComponentsInChildren<Transform>();
			int count = children.Length;
			for ( int i = 0; i < count; i++ )
			{
				Transform child = children[i];
				switch ( child.name )
				{
					case "Bip01 L Hand":
					case "Bip001 L Hand":
						new GameObject( "__LHand" ).transform.SetParent( child, false );
						Debug.Log( "__LHand added" );
						break;
					case "Bip01 R Hand":
					case "Bip001 R Hand":
						new GameObject( "__RHand" ).transform.SetParent( child, false );
						Debug.Log( "__RHand added" );
						break;
					case "Bip01 L Toe0":
					case "Bip001 L Toe0":
						new GameObject( "__LFoot" ).transform.SetParent( child, false );
						Debug.Log( "__LFoot added" );
						break;
					case "Bip01 R Toe0":
					case "Bip001 R Toe0":
						new GameObject( "__RFoot" ).transform.SetParent( child, false );
						Debug.Log( "__RFoot added" );
						break;
					case "Bip01 HeadNub":
					case "Bip001 HeadNub":
						new GameObject( "__HeadNub" ).transform.SetParent( child, false );
						Debug.Log( "__HeadNub added" );
						break;
					case "Bip01 Prop1":
					case "Bip001 Prop1":
						new GameObject( "__Weapon0" ).transform.SetParent( child, false );
						new GameObject( "__Weapon1" ).transform.SetParent( child, false );
						Debug.Log( "__Weapon0 added" );
						Debug.Log( "__Weapon1 added" );
						break;
				}
			}
		}
	}
}
