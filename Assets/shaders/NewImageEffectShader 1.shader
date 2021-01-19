Shader "coustom/NewImageEffectShader 1"
{
    Properties
    {
		_R ("radius", Range (0.1, 15)) = 0.03

    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
			float _R;

            fixed4 frag (v2f inp) : SV_Target
            {
				float4 col = 0;
				float r = _R/100;
				float step = min(r/5+0.001,0.005);	
				float  weight_sum = 0;
				float w;
				float factor = 1;

				if(inp.uv.y<0.25){
					factor=1000;
				}
				for(float offset1=-r ; offset1<=r ; offset1+=step){
					float2 uv = inp.uv + float2( offset1 , 0);
					w = exp(-offset1*offset1/r*20*factor);
					col += tex2D(_MainTex, uv)*w;
					weight_sum += w;
				}
				
				col =  col / weight_sum;

				return col;
            }
            ENDCG
        }
    }
}
