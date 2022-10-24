Shader "Shader"
{
    Properties
    {
        _Color("Main Color", Color) = (0,0,0,0)

        [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Float) = 0
    }
    SubShader
    {
        Tags{ "RenderType" = "Transparent" "Queue" = "Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off ZWrite Off 
        ZTest[_ZTest]

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

            float4 _Color;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = _Color;
                col.a = 0;
                col.a += (1-smoothstep(-0.1, 0.1, i.uv.x))*2;
                col.a += (1-smoothstep(-0.1, 0.1, i.uv.y))*2;
                col.a += smoothstep(0.9, 1.1, i.uv.x)*2;
                col.a += smoothstep(0.9, 1.1, i.uv.y)*2;
                col.a *= _Color.a;
                return col;
            }
            ENDCG
        }
    }
}
