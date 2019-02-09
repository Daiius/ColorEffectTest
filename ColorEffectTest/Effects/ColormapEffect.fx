sampler2D input : register(S0);

static const float PI = 3.14159265;

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 color = tex2D(input, uv);
	float r = clamp(color.r * 3 - 1, 0, 1);
	float g = sin(color.r * PI);
	float b = clamp(-(2*color.r - 0.84), 0, 1);
	return float4(r,g,b,1.0);
}