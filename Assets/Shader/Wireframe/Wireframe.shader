Shader "Custom/Geometry/Wireframe"//検索の名前
{
    //インスペクターで見える項目
    Properties
    {
        [PowerSlider(3.0)]
        _WireframeVal ("Wireframe width", Range(0., 0.5)) = 0.05
        _FrontColor ("Front color", color) = (1., 1., 1., 1.)
        _BackColor ("Back color", color) = (1., 1., 1., 1.)
        [Toggle] _RemoveDiag("Remove diagonals?", Float) = 0.
    }
    //ココからシェーダー
    SubShader
    {
        Tags { "Queue"="Geometry" "RenderType"="Opaque" }
//フロント
        Pass
        {
            Cull Front
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geom

            // Change "shader_feature" with "pragma_compile" if you want set this keyword from c# code
            #pragma shader_feature __ _REMOVEDIAG_ON

            #include "UnityCG.cginc"//決まり文句

            struct v2g //構造体
            {
                float4 worldPos : SV_POSITION;
            };

            struct g2f //構造体
            {
                float4 pos : SV_POSITION;
                float3 bary : TEXCOORD0;
            };

            v2g vert(appdata_base v) 
            {
                v2g o;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            [maxvertexcount(3)]
            void geom(triangle v2g IN[3], inout TriangleStream<g2f> triStream) //関数
            {
                float3 param = float3(0., 0., 0.);

                #if _REMOVEDIAG_ON
                float EdgeA = length(IN[0].worldPos - IN[1].worldPos);
                float EdgeB = length(IN[1].worldPos - IN[2].worldPos);
                float EdgeC = length(IN[2].worldPos - IN[0].worldPos);

                if(EdgeA > EdgeB && EdgeA > EdgeC)
                {
                    param.y = 1.;
                }
                else if (EdgeB > EdgeC && EdgeB > EdgeA)
                {    
                    param.x = 1.;
                }
                else
                {
                    param.z = 1.;
                }
                #endif

                g2f o;
                o.pos = mul(UNITY_MATRIX_VP, IN[0].worldPos);
                o.bary = float3(1., 0., 0.) + param;
                triStream.Append(o);
                o.pos = mul(UNITY_MATRIX_VP, IN[1].worldPos);
                o.bary = float3(0., 0., 1.) + param;
                triStream.Append(o);
                o.pos = mul(UNITY_MATRIX_VP, IN[2].worldPos);
                o.bary = float3(0., 1., 0.) + param;
                triStream.Append(o);
            }

            float _WireframeVal;
            fixed4 _BackColor;

            //フラグメントシェーダーが三角形メッシュのピクセル色を決めています。
            //フラグメントシェーダーの役割はディスプレイに表示するピクセルの色を決めるこです。
            //またフラグメントシェーダーはメッシュ内のピクセルすべてに実行する関数です。
            //「frag関数」がフラグメントシェーダー。
            fixed4 frag(g2f i) : SV_Target //決まり文句
            {
                if(!any(bool3(i.bary.x < _WireframeVal, i.bary.y < _WireframeVal, i.bary.z < _WireframeVal)))
                {
                    discard;
                }
                return _BackColor;//ここで返す色を変更するとメッシュの色が変わる  
            }

            ENDCG
        }
//フロント
//バック
        Pass
        {
            Cull Back
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geom

            // Change "shader_feature" with "pragma_compile" if you want set this keyword from c# code
            #pragma shader_feature __ _REMOVEDIAG_ON

            #include "UnityCG.cginc"

            struct v2g 
            {
                float4 worldPos : SV_POSITION;
            };

            struct g2f 
            {
                float4 pos : SV_POSITION;
                float3 bary : TEXCOORD0;
            };

            v2g vert(appdata_base v) 
            {
                v2g o;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            [maxvertexcount(3)]
            void geom(triangle v2g IN[3], inout TriangleStream<g2f> triStream) 
            {
                float3 param = float3(0., 0., 0.);

                #if _REMOVEDIAG_ON
                float EdgeA = length(IN[0].worldPos - IN[1].worldPos);
                float EdgeB = length(IN[1].worldPos - IN[2].worldPos);
                float EdgeC = length(IN[2].worldPos - IN[0].worldPos);

                if(EdgeA > EdgeB && EdgeA > EdgeC)
                    param.y = 1.;
                else if (EdgeB > EdgeC && EdgeB > EdgeA)
                    param.x = 1.;
                else
                    param.z = 1.;
                #endif

                g2f o;
                o.pos = mul(UNITY_MATRIX_VP, IN[0].worldPos);
                o.bary = float3(1., 0., 0.) + param;
                triStream.Append(o);
                o.pos = mul(UNITY_MATRIX_VP, IN[1].worldPos);
                o.bary = float3(0., 0., 1.) + param;
                triStream.Append(o);
                o.pos = mul(UNITY_MATRIX_VP, IN[2].worldPos);
                o.bary = float3(0., 1., 0.) + param;
                triStream.Append(o);
            }

            float _WireframeVal;
            fixed4 _FrontColor;

            fixed4 frag(g2f i) : SV_Target 
            {
            if(!any(bool3(i.bary.x <= _WireframeVal, i.bary.y <= _WireframeVal, i.bary.z <= _WireframeVal)))
                 discard;

                return _FrontColor;
            }

            ENDCG
        }
        //バック
    }//SubShader
}