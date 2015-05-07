Shader "Custom/TexBlend" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_BlendTex ("BlendTex", 2D) = "white" {}
	_Mul ("Mul", Color) = (1,1,1,1)
}


CGINCLUDE
#pragma vertex vert_img
#pragma target 3.0
#include "UnityCG.cginc" 

sampler2D _MainTex;
half4 _MainTex_TexelSize;
sampler2D _BlendTex;
float4 _Mul;

float4 GetBaseColor(float2 uv)
{
	#if UNITY_UV_STARTS_AT_TOP
	if (_MainTex_TexelSize.y >= 0){
		uv.y = 1.0 - uv.y;
	}
	#endif

	return tex2D(_MainTex, uv);
}

float4 GetBlendColor(float2 uv)
{
	#if UNITY_UV_STARTS_AT_TOP
	uv.y = 1.0 - uv.y;
	#endif

	return tex2D(_BlendTex, uv) * _Mul;
}

float4 frag_add(v2f_img IN) : COLOR {

	return GetBaseColor(IN.uv) + GetBlendColor(IN.uv);
}

float4 frag_blend(v2f_img IN) : COLOR 
{
	float4 blend = GetBlendColor(IN.uv);

	return GetBaseColor(IN.uv) * (1 - blend.a) + blend * blend.a;
}

ENDCG

SubShader {

	Tags { "Queue"="Overlay" }

	ZTest Off Cull Off ZWrite Off
	Fog { Mode off }
	Blend off

	Pass {
		CGPROGRAM
		#pragma fragment frag_add
		ENDCG
	}

	Pass {
		CGPROGRAM
		#pragma fragment frag_blend
		ENDCG
	}
}

}
