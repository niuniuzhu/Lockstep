Shader "Game/BSC Image Effect"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}  
		_BrightnessAmount ("Brightness Amount", Range(0.0, 2.0)) = 1.0  
		_SaturationAmount ("Saturation Amount", Range(0.0, 1.0)) = 1.0  
		_ContrastAmount ("Contrast Amount", Range(0.0, 3.0)) = 1.0  
	}
	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			fixed _BrightnessAmount;  
			fixed _SaturationAmount;  
            fixed _ContrastAmount;

			float3 ContrastSaturationBrightness (float3 color, float brt, float sat, float con) {
				float avgLumR = 0.5;
				float avgLumG = 0.5;
				float avgLumB = 0.5;
				
				float3 LuminanceCoeff = float3 (0.2125, 0.7154, 0.0721);
				
				float3 avgLumin = float3 (avgLumR, avgLumG, avgLumB);
				float3 brtColor = color * brt;
				float intensityf = dot (brtColor, LuminanceCoeff);
				float3 intensity = float3 (intensityf, intensityf, intensityf);
				
				float3 satColor = lerp (intensity, brtColor, sat);
				float3 conColor = lerp (avgLumin, satColor, con);
				
				return conColor;
			}

			fixed4 frag(v2f_img i) : COLOR {
				fixed4 renderTex = tex2D(_MainTex, i.uv);
				renderTex.rgb = ContrastSaturationBrightness (renderTex.rgb, _BrightnessAmount, _SaturationAmount, _ContrastAmount);
				return renderTex;
			}
			ENDCG
		}
	}
}
