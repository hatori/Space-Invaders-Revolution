Texture screenBuffer; 

sampler TextureSampler = 
sampler_state 
{ 
	texture = <screenBuffer>; 
	magfilter = LINEAR; 
	minfilter = LINEAR; 
	mipfilter = LINEAR; 
	AddressU = mirror; 
	AddressV = mirror;
}; 
	
struct VertexToPixel
{
	float4 Position : POSITION;
	float2 TexCoords : TEXCOORD0; 
}; 

struct PixelToFrame 
{
	float4 Color : COLOR0; 
};

VertexToPixel VShader(float4 position : POSITION, float2 texCoords : TEXCOORD0) 
{ 
	VertexToPixel Output = (VertexToPixel)0; 
	Output.Position = position; 
	Output.TexCoords = texCoords; return Output; 
}

PixelToFrame PShader(VertexToPixel PSIn) 
{ 
	PixelToFrame Output = (PixelToFrame)0;
	Output.Color = tex2D(TextureSampler, PSIn.TexCoords);
	return Output; 
}

technique First_shader
{
	pass Pass0
	{
		VertexShader = compile vs_2_0 VShader();
		PixelShader = compile ps_2_0 PShader();
	}
}