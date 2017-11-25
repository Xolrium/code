Shader "Custom/LowPoly" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 100	
		
		Pass {

		CGPROGRAM
		#include "UnityCG.cginc"
		#pragma vertex vert
		#pragma fragment frag
		#pragma geometry geom
		#pragma target 4.0

		sampler2D _MainTex;
		
		struct v2g
		{
			float4 pos : SV_POSITION;
			float3 norm : NORMAL;
			float2 uv : TEXCOORD0;
		};

		struct g2f
		{
			float4 pos : SV_POSITION;
			float3 norm : NORMAL;
			float2 uv :TEXCOORD0;
		};

		v2g vert(appdata_full v)
		{
			float3 v0 = v.vertex.xyz;
			v2g OUT;
			OUT.pos = v.vertex;
			OUT.norm = v.normal;
			OUT.uv = v.texcoord;
			return OUT;
		}

		[maxvertexcount(3)]
		void geom(triangle v2g IN[3], inout TriangleStream<g2f> triStream)
		{
			float3 v0 = IN[0].pos.xyz;
			float3 v1 = IN[1].pos.xyz;
			float3 v2 = IN[2].pos.xyz;

			float3 normal = normalize(cross(v0 - v1, v1 - v0));
			v0 += (normal * 0.1) * (_SinTime.x);
			v1 += (normal * 0.1) * (_SinTime.x);
			v2 += (normal * 0.1) * (_SinTime.x);

			g2f OUT;
			OUT.pos = UnityObjectToClipPos(v0);
			OUT.norm = normal;
			OUT.uv = IN[0].uv;
			triStream.Append(OUT);
 
			OUT.pos = UnityObjectToClipPos(v1);
			OUT.norm = normal;
			OUT.uv = IN[1].uv;
			triStream.Append(OUT);
 
			OUT.pos = UnityObjectToClipPos(v2);
			OUT.norm = normal;
			OUT.uv = IN[2].uv;
			triStream.Append(OUT);
		}

		half4 frag(g2f IN) : COLOR
		{
			fixed4 col = tex2D(_MainTex, IN.uv);	
			return col;
		}
		ENDCG
		}
	}
	FallBack "Diffuse"
}
