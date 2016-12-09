// File: VertexColor.shader 
// Author: Ross Brown - r.brown@qut.edu.au
// Updated: 11/03/2015
// Description: Colour rendering custom shader

Shader "VertexColor" 
{
      SubShader // Unity chooses the subshader that fits the GPU best
      { 
      Pass // some shaders require multiple passes
      { 
         CGPROGRAM // here begins the part in Unity's Cg
         
         #pragma vertex VS_Gouraud 
            // this specifies the vert function as the vertex shader 
         #pragma fragment PS_Gouraud
            // this specifies the frag function as the fragment shader
         #pragma target 3.0
            
		 
		struct VSInput 
		{
			float4 pos: POSITION;
			float3 col: COLOR;
		};

		struct VSGouraudOutput 
		{
			float4 pos: SV_POSITION;
			float4 col: COLOR;
		};
 
         // Gouraud Vertex Shader
		VSGouraudOutput VS_Gouraud(VSInput a_Input) 
		{
			VSGouraudOutput Output;
		
			// calculate vertex position homogenous
			Output.pos = mul(UNITY_MATRIX_MVP, a_Input.pos);
		
			// combine for colour
			Output.col.rgb = float4(a_Input.col, 1.0f);
				
			return Output;
		}
         
		// Gouraud Pixel Shader
		float4 PS_Gouraud(VSGouraudOutput a_Input) : COLOR 
		{
			return a_Input.col;
		}

         ENDCG // here ends the part in Cg 
      }
   }
}