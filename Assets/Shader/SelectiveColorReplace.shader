// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "SelectiveColorReplace"
{
    Properties
    {
    	_ReplaceColor ("Frog Skin Color", Color) = (0.6666667, 0.6901961, 0.345098, 1)
    	_SecondReplaceColor ("Frog Shadow Color", Color) = (0.4901961, 0.3882353, 0.2941177, 1)
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "Queue" = "Transparent+1"
        }

        Pass
        {
	        ZWrite Off
	        Blend SrcAlpha OneMinusSrcAlpha
	        Cull Off

	        CGPROGRAM
	        #pragma vertex vert
	        #pragma fragment frag
	        #pragma multi_compile DUMMY PIXELSNAP_ON

	        sampler2D _MainTex;
	        float4 _ReplaceColor;
	        float4 _SecondReplaceColor;
	        fixed4 _Color;

	        struct Vertex
	        {
	            float4 vertex : POSITION;
	            float4 color : COLOR;
	            float2 uv_MainTex : TEXCOORD0;
	            float2 uv2 : TEXCOORD1;
	        };

	        struct Fragment
	        {
	            float4 vertex : POSITION;
	            float4 color : COLOR;
	            float2 uv_MainTex : TEXCOORD0;
	            float2 uv2 : TEXCOORD1;
	        };

	        Fragment vert(Vertex v)
	        {
	            Fragment o;

	            o.vertex = UnityObjectToClipPos(v.vertex);
	            o.color = v.color;
	            o.uv_MainTex = v.uv_MainTex;
	            o.uv2 = v.uv2;

	            return o;
	        }

	        float4 frag(Fragment IN) : COLOR
	        {
	            half4 c = tex2D(_MainTex, IN.uv_MainTex);

	            if (c.r >= _ReplaceColor.r - 0.005 && c.r <= _ReplaceColor.r + 0.005
	                && c.g >= _ReplaceColor.g - 0.005 && c.g <= _ReplaceColor.g + 0.005
	                && c.b >= _ReplaceColor.b - 0.005 && c.b <= _ReplaceColor.b + 0.005)
	            {
	                return IN.color;
	            }
	            if (c.r >= _SecondReplaceColor.r - 0.005 && c.r <= _SecondReplaceColor.r + 0.005
	                && c.g >= _SecondReplaceColor.g - 0.005 && c.g <= _SecondReplaceColor.g + 0.005
	                && c.b >= _SecondReplaceColor.b - 0.005 && c.b <= _SecondReplaceColor.b + 0.005)
	            {
	                return (IN.color * _SecondReplaceColor);
	            }

	            return c;
	        }
            ENDCG
        }
    }
}