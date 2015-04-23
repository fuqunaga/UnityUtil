Shader "Custom/TexBlend" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_BlendTex ("BlendTex", 2D) = "white" {}
	_Mul ("Mul", Color) = (1,1,1,1)
}


CGINCLUDE
#include "UnityCG.cginc" 

sampler2D _MainTex;
sampler2D _BlendTex;
float4 _Mul;

float3 frag(v2f_img IN) : COLOR {
	float3 c = tex2D(_MainTex, IN.uv).rgb;

	#if UNITY_UV_STARTS_AT_TOP
	IN.uv.y = 1.0 - IN.uv.y;
	#endif

	float4 blend = tex2D(_BlendTex, IN.uv) * _Mul;
	//return c * (1-blend.a) + blend.rgb * blend.a;
	return c + blend;
	//return blend;
}

ENDCG

SubShader {

	Tags { "Queue"="Overlay" }
	Pass {
		ZTest Off Cull Off ZWrite Off
		Fog { Mode off }
		Blend off

		CGPROGRAM
		#pragma vertex vert_img
		#pragma fragment frag
		#pragma target 3.0

		ENDCG
		
	    //SetTexture [_BlendTex] {
	    //	constantColor[_Mul]
	    //    combine texture * constant
	    //}

	    //// Blend in the alpha texture using the lerp operator
	    //SetTexture [_MainTex] {
	    //    //combine previous lerp(previous) texture
		//	combine previous * previous + texture
	    //}
	    
	}
}

}
