Shader "Vfx/UVTranslation-TransparencyMap"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_TransparencyMap ("Transparency Map", 2D) = "white" {}
		_Transparency ("Transparency", Float) = 1 
		_SpeedX ("Speed X", Float) = 1
		_SpeedY ("Speed Y", Float) = 0
		_Color ("Color", Color) = (1,1,1,1)
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
			sampler2D _TransparencyMap;
			float _Transparency;
			float _SpeedX;
			float _SpeedY;
			float4 _MainTex_ST;
			float4 _Color;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 t =  tex2D(_TransparencyMap, i.uv);
				i.uv.y += _Time * _SpeedX;
				i.uv.x += _Time * _SpeedY;
				fixed4 col = tex2D(_MainTex, i.uv);
				col.rgb *= _Color;
				col.a *= _Transparency * t.a; 
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
