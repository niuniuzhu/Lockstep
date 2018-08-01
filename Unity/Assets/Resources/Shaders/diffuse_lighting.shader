Shader "Game/DiffuseLight" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}

		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (0, 0.1)) = .015
		_Damp ("Damp", Range (0, 1)) = 0.1
	}

    SubShader
    {
		Pass
		{
			Tags {"RenderType"="Opaque" "IgnoreProjector"="True" "LightMode" = "ForwardBase"}

			Cull Front
            ColorMask RGB
			Lighting Off
			Fog {Mode Off}
			
			CGPROGRAM
			#include "UnityCG.cginc"
            #pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag
			
			fixed4 _OutlineColor;
			float _Outline;
			float _Damp;
			
			struct v2f
			{
				float4 pos : SV_POSITION;
				float4 color : COLOR; 
			};
			
			v2f vert(appdata_base v)
			{
                v2f o;
                float3 camPos = mul(unity_WorldToObject,_WorldSpaceCameraPos);
                float3 norm = normalize(v.normal);
                v.vertex.xyz += v.normal * _Outline * pow(abs(camPos.z),_Damp);
                o.pos = UnityObjectToClipPos(v.vertex);
				o.color = _OutlineColor;
				return o;
			}
			
			fixed4 frag(v2f i) :COLOR
			{
				return i.color;
			}
			ENDCG
		}

        Pass
        {
            Tags{ "LightMode" = "ForwardBase" "IgnoreProjector"="True" }
 
            CGPROGRAM
            #include "UnityCG.cginc"
 
            #pragma target 3.0
            #pragma vertex vertexShader
            #pragma fragment fragmentShader
 
            float4 _LightColor0;
			float4 _MainTex_ST;
			sampler2D _MainTex;

            struct a2v  
            {  
                float4 vertex : POSITION;  
                float3 normal : NORMAL;  
                float4 tangent : TANGENT;  
                float4 texcoord : TEXCOORD0;  
            };  
 
            struct v2f {
                float4 pos : SV_POSITION;
                float3 normal : NORMAL;
				half2 uv : TEXCOORD0;
                float3 lightDir : TEXCOORD1;
            };
 
            v2f vertexShader(a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                TANGENT_SPACE_ROTATION;
                o.normal = normalize(mul(rotation, v.normal));
                o.lightDir = normalize(mul(rotation, ObjSpaceLightDir(v.vertex)));
				o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
 
                return o;
            }
 
            fixed4 fragmentShader(v2f i) : SV_Target
            {
                fixed4 diff = saturate(dot(i.lightDir, i.normal));
 
				fixed4 tex = tex2D(_MainTex, i.uv);

                return (UNITY_LIGHTMODEL_AMBIENT + diff) * _LightColor0 * tex;
            }
 
            ENDCG
        }
    }
    Fallback off
}