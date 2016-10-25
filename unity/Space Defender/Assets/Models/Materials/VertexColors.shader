Shader "Custom/VertexColors" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
	}
	SubShader {
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert alpha

		struct Input {
			float2 uv_MainTex;
			float4 color : COLOR;
		};

		fixed4 _Color;

		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = IN.color.rgb;
			o.Emission = IN.color.rgb; // * _Color.a;
			o.Alpha = IN.color.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}