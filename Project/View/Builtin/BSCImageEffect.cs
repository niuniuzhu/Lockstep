using DG.Tweening;
using UnityEngine;

namespace View.Builtin
{
	public class BSCImageEffect : MonoBehaviour
	{
		public Material material;
		public float brightnessAmount = 1.0f;
		public float saturationAmount = 1.0f;
		public float contrastAmount = 1.0f;
		public float duration = 1.2f;

		public delegate void CompleteHandler();

		public void FadeIn( CompleteHandler callback )
		{
			DOTween.To( ( t ) => this.saturationAmount = t, this.saturationAmount, 0f, this.duration ).OnComplete( () => { if ( callback != null )callback.Invoke(); } );
		}

		public void FadeOut( CompleteHandler callback )
		{
			DOTween.To( ( t ) => this.saturationAmount = t, this.saturationAmount, 1f, this.duration ).OnComplete( () => { if ( callback != null )callback.Invoke(); } );
		}

		void OnRenderImage( RenderTexture sourceTexture, RenderTexture destTexture )
		{
			if ( this.material != null )
			{
				this.material.SetFloat( "_BrightnessAmount", this.brightnessAmount );
				this.material.SetFloat( "_SaturationAmount", this.saturationAmount );
				this.material.SetFloat( "_ContrastAmount", this.contrastAmount );

				UnityEngine.Graphics.Blit( sourceTexture, destTexture, this.material );
			}
			else
				UnityEngine.Graphics.Blit( sourceTexture, destTexture );
		}
	}
}