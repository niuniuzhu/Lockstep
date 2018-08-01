// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/UIModel"
{
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Scale ("Scale", Float) = 1
	}
 
	SubShader {
		Pass
		{
			Tags {
				"Queue"="Transparent" 
				"IgnoreProjector"="True" 
				"RenderType"="Transparent" 
				}
			
			Cull Off
			Lighting Off
			
			CGPROGRAM
			#include "UnityCG.cginc"
			#pragma vertex vert
			#pragma fragment frag
			
			float _Scale;
			float4 _MainTex_ST;
			sampler2D _MainTex;
			
			struct v2f {
				float4 pos : POSITION;
				half2 uv : TEXCOORD0;
			};
			
			v2f vert(appdata_base v) {
				v2f o;
				v.vertex.xyz *= _Scale;
				o.pos = UnityObjectToClipPos (v.vertex);
				o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
				return o;
			}
			
			fixed4 frag(v2f i) :COLOR
			{
				fixed4 tex = tex2D(_MainTex, i.uv);
				fixed4 c = tex ;
				return c;
			}
			ENDCG
		}
	}
	Fallback Off
}
