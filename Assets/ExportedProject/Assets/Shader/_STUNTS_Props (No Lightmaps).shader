Shader "_STUNTS/Props (No Lightmaps)" {
	Properties {
		[Header(Ambient and Shadows)] _ShadowAmbientInt ("Shadow Ambient int", Range(0, 1)) = 0.5
		[Header(Textures)] _MainTex ("Texture Map (RGB Albedo, A Specular)", 2D) = "white" {}
		[Header(Specular)] _Shinnes ("Shinnes", Range(0.1, 700)) = 20
		_SpecInt ("Intensity Mat", Range(0, 10)) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}