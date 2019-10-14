Shader "Vfx/ChangeTextureBillboard"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_SecondTex ("SecondTexture", 2D) = "white" {}
		_Transition ("Transition", Range(0,1)) = 0
		_Scale ("Scale", Float) = 1
	}
	SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		LOD 100

		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _SecondTex;
			float _Transition;
			float _Scale;

			v2f vert(appdata input) 
			{
				v2f output;

				float4x4 mv = UNITY_MATRIX_MV;

				// First colunm.
				mv._m00 = _Scale; 
				mv._m10 = 0.0f; 
				mv._m20 = 0.0f ; 
			
				// Second colunm.
				mv._m01 = 0.0f; 
				mv._m11 = _Scale; 
				mv._m21 = 0.0f; 

				// Thrid colunm.
				mv._m02 = 0.0f; 
				mv._m12 = 0.0f; 
				mv._m22 = _Scale; 					

				output.vertex = mul(UNITY_MATRIX_P, mul(mv, input.vertex));
				output.uv = TRANSFORM_TEX(input.uv, _MainTex);
				UNITY_TRANSFER_FOG(output,output.vertex);		
				return output;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv)*(1-_Transition) +  tex2D(_SecondTex, i.uv)*_Transition;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
