sampler2D input : register(s0);
sampler1D array : register(s1);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 color = tex2D(input, uv);
	float4 tmp = tex1D(array, uv[0]);
	float4 result = color;
	result.r = tmp.r;
	return result;
}
