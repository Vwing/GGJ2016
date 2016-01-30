Shader "Custom/DiffuseWorldspace" {
    Properties{
        _Color("Main Color", Color) = (1,1,1,1)
        _MainTex("Base (RGB)", 2D) = "white" {}
        _Scale("Texture Scale", Float) = 1.0
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0

    }
        SubShader{
        Tags{ "RenderType" = "Opaque" }

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;
        fixed4 _Color;
        float _Scale;

        half _Glossiness;
        half _Metallic;

        struct Input {
            float3 worldNormal;
            float3 worldPos;
        };

        void surf(Input IN, inout SurfaceOutputStandard o) {
            float2 UV;
            fixed4 c;

            // RIGHT NOW USES SAME TEXTURE FOR ALL SIDES
            // change _MainTex to other passed in textures later if want
            if (abs(IN.worldNormal.x) > 0.5) {
                UV = IN.worldPos.yz; // side
                c = tex2D(_MainTex, UV* _Scale); // use WALLSIDE texture
            } else if (abs(IN.worldNormal.z) > 0.5) {
                UV = IN.worldPos.xy; // front
                c = tex2D(_MainTex, UV* _Scale); // use WALL texture
            } else {
                UV = IN.worldPos.xz; // top
                c = tex2D(_MainTex, UV* _Scale); // use FLR texture
            }

            o.Albedo = c.rgb * _Color;

            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
        }
        Fallback "Diffuse"
}