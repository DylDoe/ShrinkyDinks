// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-2530-OUT,clip-7590-OUT;n:type:ShaderForge.SFN_TexCoord,id:559,x:31413,y:32758,varname:node_559,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Floor,id:7590,x:32127,y:33060,varname:node_7590,prsc:2|IN-3828-OUT;n:type:ShaderForge.SFN_RemapRange,id:5393,x:31581,y:32802,varname:node_5393,prsc:2,frmn:0,frmx:1,tomn:0,tomx:1|IN-559-V;n:type:ShaderForge.SFN_Length,id:2165,x:31745,y:32839,varname:node_2165,prsc:2|IN-5393-OUT;n:type:ShaderForge.SFN_Add,id:3828,x:31950,y:32975,varname:node_3828,prsc:2|A-2165-OUT,B-6380-OUT;n:type:ShaderForge.SFN_Slider,id:7150,x:31462,y:33117,ptovrint:False,ptlb:Health,ptin:_Health,varname:node_7150,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.005,cur:1.005,max:1.005;n:type:ShaderForge.SFN_Color,id:9536,x:32209,y:33324,ptovrint:False,ptlb:node_9536,ptin:_node_9536,varname:node_9536,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.7254902,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_OneMinus,id:9543,x:32263,y:32966,varname:node_9543,prsc:2|IN-7590-OUT;n:type:ShaderForge.SFN_Add,id:2530,x:32407,y:33010,varname:node_2530,prsc:2|A-9543-OUT,B-9536-RGB;n:type:ShaderForge.SFN_RemapRange,id:6380,x:31793,y:33158,varname:node_6380,prsc:2,frmn:0,frmx:1,tomn:1,tomx:0|IN-7150-OUT;proporder:7150-9536;pass:END;sub:END;*/

Shader "Shader Forge/HealthOnPlayerModel" {
    Properties {
        _Health ("Health", Range(0.005, 1.005)) = 1.005
        _node_9536 ("node_9536", Color) = (0.7254902,0,0,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
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
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _Health;
            uniform float4 _node_9536;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float node_7590 = floor((length((i.uv0.g*1.0+0.0))+(_Health*-1.0+1.0)));
                clip(node_7590 - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = ((1.0 - node_7590)+_node_9536.rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _Health;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float node_7590 = floor((length((i.uv0.g*1.0+0.0))+(_Health*-1.0+1.0)));
                clip(node_7590 - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
