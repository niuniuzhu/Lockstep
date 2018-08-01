// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Game/Billboard" {
	Properties {
			_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
			_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "DisableBatching" = "True"  }
	
		pass{
			//Cull Off
			//ZTest Always
			Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			sampler2D _MainTex;
			fixed4 _TintColor;
			
			struct v2f {
				float4 pos : POSITION;
				half2 texc:TEXCOORD0;
			};
			
			v2f vert(appdata_base v)
			{
				v2f o;
				
				float3 cameraPosition = ObjSpaceViewDir(float4(0,0,0,1));
				float3 forward = normalize(-cameraPosition);
				float3 up = normalize(cross(float3(0,1,0), forward));
				float3 right = cross(forward, up);
				
				float4x4 m = float4x4(up.x,right.x,forward.x,0,
															up.y,right.y,forward.y,0,
															up.z,right.z,forward.z,0,
															-dot(up, cameraPosition),-dot(right, cameraPosition),-dot(forward, cameraPosition),1);
				float4 pos = mul(m,v.vertex);
				o.pos = UnityObjectToClipPos(pos);
				o.texc = v.texcoord;
				return o;
			}
			
			fixed4 frag(v2f i):COLOR
			{
				fixed4 c = tex2D(_MainTex, i.texc) * _TintColor;
				c.a = c.r;
				return c;
			}
			ENDCG
		}
	}
	Fallback Off
}