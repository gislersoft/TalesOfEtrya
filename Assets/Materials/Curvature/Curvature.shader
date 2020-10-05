﻿Shader "Unlit/Curvature"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Curvature ("Curvature", float) = 0.0001 
		_Horizontal_Curvature ("Horizontal Curvature", float) = 0.0001
		_Color ("Color", Color) = (0,0,0,0)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		CGPROGRAM
#pragma surface surf Lambert vertex:vert addshadow
			uniform sampler2D _MainTex;
			uniform float _Curvature;
			uniform float _Horizontal_Curvature;
			uniform float4 _Color;
			
			struct Input 
			{
				float2 uv_MainTex;
			};

			void vert(inout appdata_full v) 
			{
				float4 worldSpace = mul(unity_ObjectToWorld, v.vertex);
				worldSpace.xyz -= _WorldSpaceCameraPos.xyz;
				worldSpace = float4((worldSpace.z * worldSpace.z) * _Horizontal_Curvature, (worldSpace.z * worldSpace.z) * -_Curvature, 0.0f, 0.0f);

				v.vertex += mul(unity_WorldToObject, worldSpace);
			}

			void surf(Input IN, inout SurfaceOutput o) 
			{
				half4 c = tex2D(_MainTex, IN.uv_MainTex);
				o.Albedo = c.rgb * _Color;
				o.Alpha = c.a; 
			}
		ENDCG
	}
	FallBack "Mobile/Diffuse"
}
