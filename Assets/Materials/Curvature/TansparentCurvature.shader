Shader "Unlit/TansparentCurvature"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Curvature("Curvature", float) = 0.0001
		_Horizontal_Curvature ("Horizontal Curvature", float) = 0.0001
		_Color("Color", Color) = (0,0,0,0)
		_Transparency("Transparency", Range(0.0, 1.0)) = 0.25
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
		LOD 100
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		Pass
		{
			CGPROGRAM
			#pragma vertex vert 
			#pragma fragment frag

			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			uniform float _Curvature;
			uniform float _Horizontal_Curvature;
			uniform float4 _Color;
			float _Transparency;
			
			v2f vert (appdata v)
			{
				v2f o;
				float4 worldSpace = mul(unity_ObjectToWorld, v.vertex);
				worldSpace.xyz -= _WorldSpaceCameraPos.xyz;
				worldSpace = float4((worldSpace.z * worldSpace.z) * _Horizontal_Curvature, (worldSpace.z * worldSpace.z) * -_Curvature, 0.0f, 0.0f);

				o.vertex = UnityObjectToClipPos(v.vertex + mul(unity_WorldToObject, worldSpace));

				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv) + _Color;
				col.a = _Transparency;
				return col;
			}
			ENDCG
		}
	}
}
