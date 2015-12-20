// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:32996,y:32750,varname:node_4795,prsc:2|emission-5332-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32495,y:32793,varname:node_2393,prsc:2|A-9248-OUT,B-2053-RGB,C-797-RGB,D-9248-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32235,y:32772,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32235,y:32930,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:9248,x:32235,y:33081,varname:node_9248,prsc:2,v1:2;n:type:ShaderForge.SFN_Tex2d,id:4618,x:32014,y:33115,ptovrint:False,ptlb:gr,ptin:_gr,varname:node_4618,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:444,x:31600,y:33444,ptovrint:False,ptlb:lightOn,ptin:_lightOn,varname:node_444,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:5824,x:32473,y:33190,varname:node_5824,prsc:2|A-420-OUT,B-8446-OUT;n:type:ShaderForge.SFN_Vector1,id:8446,x:32014,y:33267,varname:node_8446,prsc:2,v1:10;n:type:ShaderForge.SFN_Add,id:420,x:32265,y:33324,varname:node_420,prsc:2|A-4618-R,B-6038-OUT;n:type:ShaderForge.SFN_Clamp01,id:2167,x:32611,y:33093,varname:node_2167,prsc:2|IN-5824-OUT;n:type:ShaderForge.SFN_Multiply,id:5332,x:32777,y:32879,varname:node_5332,prsc:2|A-2393-OUT,B-2167-OUT;n:type:ShaderForge.SFN_RemapRange,id:6038,x:31924,y:33484,varname:node_6038,prsc:2,frmn:0,frmx:1,tomn:-1.1,tomx:0.1|IN-444-OUT;proporder:797-4618-444;pass:END;sub:END;*/

Shader "Shader Forge/LightSaiber" {
    Properties {
        _TintColor ("Color", Color) = (0.5,0.5,0.5,1)
        _gr ("gr", 2D) = "white" {}
        _lightOn ("lightOn", Range(0, 1)) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers metal xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TintColor;
            uniform sampler2D _gr; uniform float4 _gr_ST;
            uniform float _lightOn;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float node_9248 = 2.0;
                float4 _gr_var = tex2D(_gr,TRANSFORM_TEX(i.uv0, _gr));
                float3 emissive = ((node_9248*i.vertexColor.rgb*_TintColor.rgb*node_9248)*saturate(((_gr_var.r+(_lightOn*1.2+-1.1))*10.0)));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
