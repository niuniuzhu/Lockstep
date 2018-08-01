using UnityEngine;
using UnityEngine.Rendering;

namespace View.Misc
{
	public class Utils
	{
		public static void AddChild( Transform parent, Transform child, bool autoRenameLayer, bool deep )
		{
			child.SetParent( parent, false );
			if ( autoRenameLayer )
			{
				int layer = parent.gameObject.layer;
				child.gameObject.layer = layer;
				if ( deep )
				{
					Transform[] trs = child.GetComponentsInChildren<Transform>( true );
					foreach ( Transform tr in trs )
						tr.gameObject.layer = layer;
				}
			}
		}

		public static void SetLayer( GameObject go, int layer )
		{
			Transform[] transforms = go.GetComponentsInChildren<Transform>( true );
			foreach ( Transform t in transforms )
				t.gameObject.layer = layer;
		}

		public static void SetLayer( GameObject go, string name )
		{
			SetLayer( go, LayerMask.NameToLayer( name ) );
		}

		public static void SetShadowMode( GameObject go, ShadowCastingMode shadowCastingMode )
		{
			Renderer[] renderers = go.GetComponentsInChildren<Renderer>( true );
			foreach ( Renderer renderer in renderers )
				renderer.shadowCastingMode = shadowCastingMode;
		}

		public static void SetReceivedShadow( GameObject go, bool value )
		{
			Renderer[] renderers = go.GetComponentsInChildren<Renderer>( true );
			foreach ( Renderer renderer in renderers )
				renderer.receiveShadows = value;
		}

		public static float RadianToAngle( float radian )
		{
			return radian * 180f / Mathf.PI;
		}

		public static float AngleToRadian( float angle )
		{
			return angle * Mathf.PI / 180f;
		} 
	}
}