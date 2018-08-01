using UnityEngine;

namespace View.Builtin
{
	[ExecuteInEditMode]
	[RequireComponent( typeof ( Camera ) )]
	[AddComponentMenu( "KriptoFX/RFX4_BloomAndDistortion" )]
#if UNITY_5_4_OR_NEWER
    [ImageEffectAllowedInSceneView]
#endif
	public class DistortionAndBloom : MonoBehaviour
	{
		[Range( 0.05f, 1 )] [Tooltip( "Camera render texture resolution" )] public float RenderTextureResolutoinFactor = 0.25f;

		public bool UseBloom = true;

		[Range( 0.1f, 3 )] [Tooltip( "Filters out pixels under this level of brightness." )] public float Threshold = 2f;

		[SerializeField, Range( 0, 1 )] [Tooltip( "Makes transition between under/over-threshold gradual." )] public float
			SoftKnee = 0f;

		[Range( 1, 7 )] [Tooltip( "Changes extent of veiling effects in A screen resolution-independent fashion." )] public
			float Radius = 7;

		[Tooltip( "Blend factor of the result image." )] public float Intensity = 1;

		[Tooltip( "Controls filter quality and buffer resolution." )] public bool HighQuality;


		[Tooltip( "Reduces flashing noise with an additional filter." )] public bool AntiFlicker;

		private const string shaderName = "Hidden/KriptoFX/PostEffects/RFX4_Bloom";
		private const string shaderAdditiveName = "Hidden/KriptoFX/PostEffects/RFX4_BloomAdditive";

		private RenderTexture source;
		private RenderTexture destination;
		private int previuosFrameWidth, previuosFrameHeight;
		private float previousScale;
		private Camera _cameraInstance;

		private Material m_Material;

		public Material mat
		{
			get
			{
				if ( this.m_Material == null )
					this.m_Material = CheckShaderAndCreateMaterial( Shader.Find( shaderName ) );

				return this.m_Material;
			}
		}

		private Material m_MaterialAdditive;

		public Material matAdditive
		{
			get
			{
				if ( this.m_MaterialAdditive == null )
				{
					this.m_MaterialAdditive = CheckShaderAndCreateMaterial( Shader.Find( shaderAdditiveName ) );
					this.m_MaterialAdditive.renderQueue = 3900;
				}

				return this.m_MaterialAdditive;
			}
		}

		public static Material CheckShaderAndCreateMaterial( Shader s )
		{
			if ( s == null ||
			     !s.isSupported )
				return null;

			Material material = new Material( s );
			material.hideFlags = HideFlags.DontSave;
			return material;
		}

		#region Private Members

		private const int kMaxIterations = 16;
		private readonly RenderTexture[] m_blurBuffer1 = new RenderTexture[kMaxIterations];
		private readonly RenderTexture[] m_blurBuffer2 = new RenderTexture[kMaxIterations];

		private void OnDisable()
		{
			if ( this.m_Material != null )
				DestroyImmediate( this.m_Material );
			this.m_Material = null;

			if ( this.m_MaterialAdditive != null )
				DestroyImmediate( this.m_MaterialAdditive );
			this.m_MaterialAdditive = null;

			if ( this._cameraInstance != null ) this._cameraInstance.gameObject.SetActive( false );
		}

		private void OnDestroy()
		{
			if ( this._cameraInstance != null ) DestroyImmediate( this._cameraInstance.gameObject );
		}

		private void OnGUI()
		{
			if ( UnityEngine.Event.current.type.Equals( EventType.Repaint ) )
			{
				if ( this.UseBloom )
					UnityEngine.Graphics.DrawTexture( new Rect( 0, 0, Screen.width, Screen.height ), this.destination, this.matAdditive );
			}
		}

		private void Start()
		{
			this.InitializeRenderTarget();
			//InitializeCameraCopy(); 
		}

		private void LateUpdate()
		{
			if ( this.previuosFrameWidth != Screen.width ||
			     this.previuosFrameHeight != Screen.height ||
			     Mathf.Abs( this.previousScale - this.RenderTextureResolutoinFactor ) > 0.01f )
			{
				this.InitializeRenderTarget();
				this.previuosFrameWidth = Screen.width;
				this.previuosFrameHeight = Screen.height;
				this.previousScale = this.RenderTextureResolutoinFactor;
			}
			//InitializeCameraCopy();
			Shader.EnableKeyword( "DISTORT_OFF" );
			this.UpdateCameraCopy();
			if ( this.UseBloom ) this.UpdateBloom();
			Shader.SetGlobalTexture( "_GrabTextureMobile", this.source );
			Shader.SetGlobalFloat( "_GrabTextureMobileScale", this.RenderTextureResolutoinFactor );
			Shader.DisableKeyword( "DISTORT_OFF" );
		}

		private void InitializeRenderTarget()
		{
			int width = ( int ) ( Screen.width * this.RenderTextureResolutoinFactor );
			int height = ( int ) ( Screen.height * this.RenderTextureResolutoinFactor );
			this.source = new RenderTexture( width, height, 16, RenderTextureFormat.DefaultHDR );
			if ( this.UseBloom )
			{
				this.destination = new RenderTexture( this.RenderTextureResolutoinFactor > 0.99 ? width : width / 2,
				                                      this.RenderTextureResolutoinFactor > 0.99 ? height : height / 2, 0,
				                                      RenderTextureFormat.ARGB32 );
			}
		}

		private void UpdateBloom()
		{
			bool useRGBM = Application.isMobilePlatform;

			// source texture size
			int tw = this.source.width;
			int th = this.source.height;

			// halve the texture size for the low quality mode
			if ( !this.HighQuality )
			{
				tw /= 2;
				th /= 2;
			}

			// blur buffer format
			RenderTextureFormat rtFormat = useRGBM ? RenderTextureFormat.Default : RenderTextureFormat.DefaultHDR;

			// determine the iteration count
			float logh = Mathf.Log( th, 2 ) + this.Radius - 8;
			int logh_i = ( int ) logh;
			int iterations = Mathf.Clamp( logh_i, 1, kMaxIterations );

			// update the shader properties
			float threshold = Mathf.GammaToLinearSpace( this.Threshold );

			this.mat.SetFloat( "_Threshold", threshold );

			float knee = threshold * this.SoftKnee + 1e-5f;
			Vector3 curve = new Vector3( threshold - knee, knee * 2, 0.25f / knee );
			this.mat.SetVector( "_Curve", curve );

			bool pfo = !this.HighQuality && this.AntiFlicker;
			this.mat.SetFloat( "_PrefilterOffs", pfo ? -0.5f : 0.0f );

			this.mat.SetFloat( "_SampleScale", 0.5f + logh - logh_i );
			this.mat.SetFloat( "_Intensity", Mathf.Max( 0.0f, this.Intensity ) );

			RenderTexture prefiltered = RenderTexture.GetTemporary( tw, th, 0, rtFormat );

			UnityEngine.Graphics.Blit( this.source, prefiltered, this.mat, this.AntiFlicker ? 1 : 0 );

			// construct A mip pyramid
			RenderTexture last = prefiltered;
			for ( int level = 0; level < iterations; level++ )
			{
				this.m_blurBuffer1[level] = RenderTexture.GetTemporary( last.width / 2, last.height / 2, 0, rtFormat );
				UnityEngine.Graphics.Blit( last, this.m_blurBuffer1[level], this.mat, level == 0 ? ( this.AntiFlicker ? 3 : 2 ) : 4 );
				last = this.m_blurBuffer1[level];
			}

			// upsample and combine loop
			for ( int level = iterations - 2; level >= 0; level-- )
			{
				RenderTexture basetex = this.m_blurBuffer1[level];
				this.mat.SetTexture( "_BaseTex", basetex );
				this.m_blurBuffer2[level] = RenderTexture.GetTemporary( basetex.width, basetex.height, 0, rtFormat );
				UnityEngine.Graphics.Blit( last, this.m_blurBuffer2[level], this.mat, this.HighQuality ? 6 : 5 );
				last = this.m_blurBuffer2[level];
			}

			this.destination.DiscardContents();
			UnityEngine.Graphics.Blit( last, this.destination, this.mat, this.HighQuality ? 8 : 7 );


			for ( int i = 0; i < kMaxIterations; i++ )
			{
				if ( this.m_blurBuffer1[i] != null ) RenderTexture.ReleaseTemporary( this.m_blurBuffer1[i] );
				if ( this.m_blurBuffer2[i] != null ) RenderTexture.ReleaseTemporary( this.m_blurBuffer2[i] );
				this.m_blurBuffer1[i] = null;
				this.m_blurBuffer2[i] = null;
			}

			RenderTexture.ReleaseTemporary( prefiltered );
		}

		private void InitializeCameraCopy()
		{
			if ( this._cameraInstance != null ) this._cameraInstance.gameObject.SetActive( true );
			GameObject findedCam = GameObject.Find( "RenderTextureCamera" );
			if ( findedCam == null )
			{
				GameObject renderTextureCamera = new GameObject( "RenderTextureCamera" );

				renderTextureCamera.transform.parent = Camera.main.transform;
				this._cameraInstance = renderTextureCamera.AddComponent<Camera>();
				this._cameraInstance.CopyFrom( Camera.main );
				this._cameraInstance.clearFlags = Camera.main.clearFlags;
				this._cameraInstance.depth--;
#if !UNITY_5_6_OR_NEWER
				this._cameraInstance.allowHDR = true;
#else
            _cameraInstance.allowHDR = true;
#endif
				this._cameraInstance.targetTexture = this.source;
				Shader.SetGlobalTexture( "_GrabTextureMobile", this.source );
				Shader.SetGlobalFloat( "_GrabTextureMobileScale", this.RenderTextureResolutoinFactor );
				this._cameraInstance.Render();
				//_cameraInstance.enabled = false;
			}
			else this._cameraInstance = findedCam.GetComponent<Camera>();
		}

		private void UpdateCameraCopy()
		{
			Camera cam = Camera.current;

			if ( cam != null )
			{
				//_cameraInstance.CopyFrom(cam);
				//_cameraInstance.clearFlags = cam.clearFlags;
				//_cameraInstance.depth--;
				//_cameraInstance.hdr = true;
				//_cameraInstance.targetTexture = source;
				//Shader.SetGlobalTexture("_GrabTextureMobile", source);
				//_cameraInstance.Render();
				//source.DiscardContents();
				if ( cam.name == "SceneCamera" )
				{
					this.source.DiscardContents();
					cam.targetTexture = this.source;
					cam.Render();
					cam.targetTexture = null;
					return;
				}
			}
			cam = Camera.main;
#if !UNITY_5_6_OR_NEWER
			bool hdr = cam.allowHDR;
			this.source.DiscardContents();
			cam.allowHDR = true;
			cam.targetTexture = this.source;
			cam.Render();
			cam.allowHDR = hdr;
			cam.targetTexture = null;
#else
        var hdr = cam.allowHDR;
        source.DiscardContents();
        cam.allowHDR = true;
        cam.targetTexture = source;
        cam.Render();
        cam.allowHDR = hdr;
        cam.targetTexture = null;
#endif
		}

		#endregion
	}
}