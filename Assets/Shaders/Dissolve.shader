
Shader "Bas/Dissolve" {
	Properties {
		_MainTex ("Albedo", 2D) = "white" {}
		_Noise ("Noise", 2D) = "white" {}
		_Normal ("Normal", 2D) = "bump" {}
		_MetallicSmooth ("Metallic (RGB) Smooth (A)", 2D) = "white" {}
		_AO ("AO", 2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,1)
		[HDR]_EdgeColor1 ("Edge Color", Color) = (1,1,1,1)
		[HDR]_Emission ("Emission", Color) = (0,0,0,0)
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_EdgeSize ("EdgeSize", Range(0,1)) = 0.2
		_Progress ("Progress", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "Queue"="AlphaTest" "RenderType"="TransparentCutout" "IgnoreProjector"="True" }
		Cull Off

		LOD 200
		
		CGPROGRAM

		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows addshadow 

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Noise;
		sampler2D _Normal;
		sampler2D _MetallicSmooth;
		sampler2D _AO;

		struct Input {
			float2 uv_Noise;
			float2 uv_MainTex;
			fixed4 color : COLOR0;
			float3 worldPos;
		};

		half _Glossiness, _Metallic, _EdgeSize;
		half4 _Color, _EdgeColor1, _Emission;
		half _Progress;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			half3 Noise = tex2D (_Noise, IN.uv_Noise); // Get the color from the noise texture.
			half cutoff  = lerp(0, _Progress + _EdgeSize, _Progress); // Based on how far _Progress is between 0 and 1, cutoff will range from 0 to 1 + EdgeSize

			// The noise color will be compared to the cutoff value. 
			// Darker colors will get cut off earlier in the animation, lighter colors will get cut off later in the animation.
			
			half clampedNoiseValue = clamp(Noise.r, _EdgeSize, 1); // will always be at least EdgeSize and at most 1.
			half Edge = smoothstep(cutoff + _EdgeSize, cutoff, clampedNoiseValue);
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			fixed3 EmissiveCol = c.a * _Emission;

			half3 emissionColor = lerp(0, EmissiveCol + _EdgeColor1 * Edge, step(0, Noise.r - cutoff));

			// clip ensures the current pixel isn't drawn if the cutoff value is reached. Comment this out if you just want the cracking effect but want the object to remain visible.
			clip(Noise - cutoff); //Accessing a vector as a scalar will access the first component of the vector (So this is equivalent to Noise.r - _cutoff)

			half4 MetallicSmooth = tex2D (_MetallicSmooth, IN.uv_MainTex);
			
			o.Albedo = _Color;
			o.Occlusion = tex2D (_AO, IN.uv_MainTex);
			o.Emission = emissionColor; // Either 0 or EmissiveCol + _EdgeColor1 * Edge, based on the cutoff value and the noise color.
			o.Normal = UnpackNormal (tex2D (_Normal, IN.uv_MainTex));
			o.Metallic = MetallicSmooth.r * _Metallic;
			o.Smoothness = MetallicSmooth.a * _Glossiness;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
