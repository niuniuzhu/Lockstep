Shader "UI/Default UI"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		
		[Toggle(ALPHA_TEX)]_UseAlphaTexture ("Use Alpha Texture?",Float) = 0
		[PerRendererData] _AlphaTex("Sprite Alpha Texture", 2D) = "white" {}
		
		[Toggle(MASK_TEX)]_UseMaskTexture ("Use Mask Texture?",Float) = 0
		_MaskTex("Mask Sprite Texture", 2D) = "white" {}

		_Color ("Tint", Color) = (1,1,1,1)
		_ColorMask ("Color Mask", Float) = 15

		[Enum(UnityEngine.Rendering.BlendMode)]_BlendSrcFactor ("Blend SrcFactor", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)]_BlendDstFactor ("Blend DstFactor", Float) = 10
		
		[Toggle(COLOR_FILTER)]_UseColorFilter ("Use Color Filter?",Float) = 0
		_ColorOffset ("Color Offset", Vector) = (0,0,0,0)
		
		[Toggle(BLUR_FILTER)]_UseBlurFilter ("Use Blur Filter?",Float) = 0
        _BlurSize ("Blur Size", Range(0, 10)) = 0

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip?", Float) = 0
		[Toggle(GRAYED)] _UseGrayed ("Use Grayed?", Float) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite On
		//ZTest [unity_GUIZTestMode]
		ZTest Always
		Fog { Mode Off }
		ColorMask [_ColorMask]
		Blend [_BlendSrcFactor] [_BlendDstFactor]

		Pass
		{
			Name "Default"

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.5

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"

			#pragma multi_compile __ UNITY_UI_ALPHACLIP
			#pragma multi_compile __ GRAYED
			#pragma multi_compile __ COLOR_FILTER
			#pragma multi_compile __ BLUR_FILTER
			#pragma multi_compile __ ALPHA_TEX
			#pragma multi_compile __ MASK_TEX

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				#ifdef MASK_TEX
				float2 uvmask  : TEXCOORD1;
				#endif
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
				#ifdef MASK_TEX
				float2 uvmask  : TEXCOORD2;
				#endif
			};
			
			fixed4 _Color;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.worldPosition = IN.vertex;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);

				OUT.texcoord = IN.texcoord;

				#ifdef MASK_TEX
                OUT.uvmask = IN.uvmask;
				#endif
				
				#ifdef UNITY_HALF_TEXEL_OFFSET
				OUT.vertex.xy += (_ScreenParams.zw-1.0) * float2(-1,1) * OUT.vertex.w;
				#endif
				
				OUT.color = IN.color * _Color;
				return OUT;
			}
			
			float4 _ClipRect;
			fixed4 _TextureSampleAdd;
			sampler2D _MainTex;
			
			#ifdef ALPHA_TEX
			sampler2D _AlphaTex;
			#endif
			
			#ifdef MASK_TEX
			sampler2D _MaskTex;
			#endif

			#ifdef COLOR_FILTER
			float4x4 _ColorMatrix;
			float4 _ColorOffset;
			#endif

			#ifdef BLUR_FILTER
            float4 _MainTex_TexelSize;
            float _BlurSize;
			#endif

			fixed4 frag(v2f IN) : SV_Target
			{
				#ifdef BLUR_FILTER
                fixed4 color = fixed4(0,0,0,0);
                #define GRABPIXEL(weight,kernelx) tex2D( _MainTex, float2(IN.texcoord.x + _MainTex_TexelSize.x * kernelx * _BlurSize, IN.texcoord.y)) * weight
				color += GRABPIXEL(0.0221841669359, -4.0);
                color += GRABPIXEL(0.03883721099665, -3.0);
                color += GRABPIXEL(0.0679383105525, -2.0);
                color += GRABPIXEL(0.0836540280605, -1.0);
                color += GRABPIXEL(0.0897884560805,  0.0);
                color += GRABPIXEL(0.0836540280605, +1.0);
                color += GRABPIXEL(0.0679383105525, +2.0);
                color += GRABPIXEL(0.03883721099665, +3.0);
                color += GRABPIXEL(0.0221841669359, +4.0);
				
                #define GRABPIXEL_Y(weight,kernely) tex2D( _MainTex, float2(IN.texcoord.x, IN.texcoord.y + _MainTex_TexelSize.y * kernely * _BlurSize)) * weight
				color += GRABPIXEL_Y(0.0221841669359, -4.0);
                color += GRABPIXEL_Y(0.03883721099665, -3.0);
                color += GRABPIXEL_Y(0.0679383105525, -2.0);
                color += GRABPIXEL_Y(0.0836540280605, -1.0);
                color += GRABPIXEL_Y(0.0897884560805,  0.0);
                color += GRABPIXEL_Y(0.0836540280605, +1.0);
                color += GRABPIXEL_Y(0.0679383105525, +2.0);
                color += GRABPIXEL_Y(0.03883721099665, +3.0);
                color += GRABPIXEL_Y(0.0221841669359, +4.0);
				#else
				fixed4 color = tex2D(_MainTex, IN.texcoord);
				#endif

				#ifdef ALPHA_TEX
				color = fixed4(color.rgb + _TextureSampleAdd.rgb, tex2D(_AlphaTex, IN.texcoord).r + _TextureSampleAdd.a) * IN.color;
				#else
				color = (color + _TextureSampleAdd) * IN.color;
				#endif

				#ifdef MASK_TEX
				color.a *= tex2D(_MaskTex, IN.uvmask).r;
				#endif

				#ifdef GRAYED
				fixed grey = dot(color.rgb, fixed3(0.299, 0.587, 0.114));  
				color.rgb = fixed3(grey, grey, grey);
				#endif

				#ifdef COLOR_FILTER
				fixed4 col2 = color;
				col2.r = dot(color, _ColorMatrix[0])+_ColorOffset.x;
				col2.g = dot(color, _ColorMatrix[1])+_ColorOffset.y;
				col2.b = dot(color, _ColorMatrix[2])+_ColorOffset.z;
				col2.a = dot(color, _ColorMatrix[3])+_ColorOffset.w;
				color = col2;
				#endif
				
				color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
				
				#ifdef UNITY_UI_ALPHACLIP
				clip (color.a - 0.001);
				#endif

				return color;
			}
			ENDCG
		}
	}
}
