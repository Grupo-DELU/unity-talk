Shader "Highlight/Rim Highlight" {
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_RimColor("Rim Color", Color) = (1,1,1,1)
		_RimPower("Rim Power", Range(0.1, 8.0)) = 0.8
	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert noforwardadd

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input 
		{
			float2 uv_MainTex;
			float3 viewDir;
		};

		fixed4 _Color;
		fixed4 _RimColor;
		float _RimPower;

		void surf (Input IN, inout SurfaceOutput o) 
		{
			o.Albedo = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			// manual fresnel approximation
			half fresnel = dot(normalize(IN.viewDir), o.Normal);
			fresnel = 1.0 - saturate(fresnel);
			// modulates the fresnel rim intensity with a sin function
			fresnel = pow(fresnel, _RimPower * sin(_Time.w) + 2.0 * _RimPower);
			o.Emission = _RimColor.rgb * fresnel;
		}
		ENDCG
	}
	FallBack "Mobile/Diffuse"
}
