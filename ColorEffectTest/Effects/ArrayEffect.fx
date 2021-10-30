sampler2D input : register(S0);
float array[255] : register(c0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 color = tex2D(input, uv);
	float tmp = array[32];
	float4 result = color;
	result.r = tmp;
	return result;
}
