Shader "Custom/CurveWorld"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Curvature("Curvature", Float) = 0.001
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200

		UsePass "TSF/Base1/BASE"
		UsePass "TSF/BaseOutline1/BASEOUTLINE"

		CGPROGRAM
		#pragma surface surf Lambert vertex:vert addshadow

		uniform sampler2D _MainTex;
		uniform float _Curvature;

		struct Input
		{
			float2 uv_MainTex;
		};

		void vert(inout appdata_full v)
		{
			float4 worldSpace = mul(unity_ObjectToWorld, v.vertex);
			worldSpace.xyz -= _WorldSpaceCameraPos.xyz;
			worldSpace = float4(0.0f, (worldSpace.z * worldSpace.z) * -_Curvature, 0.0f, 0.0f);

			v.vertex += mul(unity_WorldToObject, worldSpace);
		}

		void surf(Input i, inout SurfaceOutput o)
		{
			half4 c = tex2D(_MainTex, i.uv_MainTex);
			o.Albedo = c.rbg;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Mobile/Diffuse"
}
