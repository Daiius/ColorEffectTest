sampler2D input : register(S0);
float threshold : register(c0);

static const float PI = 3.14159265;

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 color = tex2D(input, uv) / threshold;
	float c = clamp(color.r, 0, 1);
	float r = clamp(c * 3 - 1, 0, 1);
	float g = sin(c * PI);
	float b = clamp(-(2*c - 0.84), 0, 1);
	return float4(r,g,b,1.0);
}
