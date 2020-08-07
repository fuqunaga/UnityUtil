Shader "Unlit/Additive"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Alpha("Alpha", float) = 1
	}

	SubShader
	{
		Tags {"Queue" = "Background"}
		// No culling or depth
		Cull Off ZWrite Off ZTest Always
		Blend One One

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#pragma multi_compile __ IS_NORMAL_MAP

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float _Alpha;

			fixed4 frag(v2f_img i) : SV_Target
			{
				fixed4 bg = tex2D(_MainTex, i.uv);
#ifdef IS_NORMAL_MAP
			bg.rgb = UnpackNormal(bg);
#endif
				bg.a = _Alpha;

				return bg;
			}
			ENDCG
		}
	}
}
