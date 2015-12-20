// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:Mobile/Diffuse,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:False,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33627,y:32978,varname:node_4013,prsc:2|diff-8845-OUT,emission-8759-OUT;n:type:ShaderForge.SFN_Color,id:1304,x:32350,y:32660,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:8845,x:32838,y:32808,varname:node_8845,prsc:2|A-1304-RGB,B-2670-RGB,C-151-OUT;n:type:ShaderForge.SFN_VertexColor,id:2670,x:32350,y:32830,varname:node_2670,prsc:2;n:type:ShaderForge.SFN_Cubemap,id:4883,x:32380,y:33076,ptovrint:False,ptlb:CubeMap,ptin:_CubeMap,varname:_CubeMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,pvfc:4|DIR-5080-OUT;n:type:ShaderForge.SFN_ViewVector,id:6966,x:31890,y:32954,varname:node_6966,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5080,x:32104,y:33075,varname:node_5080,prsc:2|A-6966-OUT,B-6011-OUT;n:type:ShaderForge.SFN_Vector1,id:6011,x:31890,y:33109,varname:node_6011,prsc:2,v1:-1;n:type:ShaderForge.SFN_Fresnel,id:6748,x:31701,y:33297,varname:node_6748,prsc:2|EXP-7052-OUT;n:type:ShaderForge.SFN_Multiply,id:2854,x:32669,y:33079,varname:node_2854,prsc:2|A-4883-RGB,B-9142-OUT;n:type:ShaderForge.SFN_Slider,id:7052,x:31341,y:33317,ptovrint:False,ptlb:node_7052,ptin:_node_7052,varname:_node_7052,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.01,cur:0.01,max:10;n:type:ShaderForge.SFN_OneMinus,id:151,x:32655,y:32939,varname:node_151,prsc:2|IN-9142-OUT;n:type:ShaderForge.SFN_Relay,id:9142,x:32556,y:33235,varname:node_9142,prsc:2|IN-6748-OUT;n:type:ShaderForge.SFN_Cubemap,id:9534,x:32381,y:33367,ptovrint:False,ptlb:CubeMapAmb,ptin:_CubeMapAmb,varname:node_9534,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,pvfc:0|DIR-3132-OUT;n:type:ShaderForge.SFN_Add,id:8759,x:33337,y:33138,varname:node_8759,prsc:2|A-2854-OUT,B-2363-OUT;n:type:ShaderForge.SFN_NormalVector,id:3132,x:31908,y:33457,prsc:2,pt:True;n:type:ShaderForge.SFN_Multiply,id:2363,x:33101,y:33313,varname:node_2363,prsc:2|A-9534-RGB,B-6915-OUT,C-8845-OUT;n:type:ShaderForge.SFN_Slider,id:6915,x:32224,y:33599,ptovrint:False,ptlb:LightController,ptin:_LightController,varname:node_6915,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;proporder:1304-4883-7052-9534-6915;pass:END;sub:END;*/

Shader "vr/Environment_lit" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _CubeMap ("CubeMap", Cube) = "_Skybox" {}
        _node_7052 ("node_7052", Range(0.01, 10)) = 0.01
        _CubeMapAmb ("CubeMapAmb", Cube) = "_Skybox" {}
        _LightController ("LightController", Range(0, 2)) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers metal xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _Color;
            uniform samplerCUBE _CubeMap;
            uniform float _node_7052;
            uniform samplerCUBE _CubeMapAmb;
            uniform float _LightController;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float node_9142 = pow(1.0-max(0,dot(normalDirection, viewDirection)),_node_7052);
                float3 node_8845 = (_Color.rgb*i.vertexColor.rgb*(1.0 - node_9142));
                float3 diffuseColor = node_8845;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = ((texCUBE(_CubeMap,(viewDirection*(-1.0))).rgb*node_9142)+(texCUBE(_CubeMapAmb,normalDirection).rgb*_LightController*node_8845));
/// Final Color:
                float3 finalColor = diffuse + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers metal xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _Color;
            uniform samplerCUBE _CubeMap;
            uniform float _node_7052;
            uniform samplerCUBE _CubeMapAmb;
            uniform float _LightController;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float node_9142 = pow(1.0-max(0,dot(normalDirection, viewDirection)),_node_7052);
                float3 node_8845 = (_Color.rgb*i.vertexColor.rgb*(1.0 - node_9142));
                float3 diffuseColor = node_8845;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Mobile/Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
