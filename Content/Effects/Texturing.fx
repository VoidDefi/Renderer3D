#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

matrix WorldViewProjection;
float3 LightDirection;

struct VertexInput
{
    float4 Position : POSITION0;
    float4 Color : COLOR0;
    float2 TexCoord : TEXCOORD0;
    float3 Normal : NORMAL0;
};

struct VertexOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TexCoord : TEXCOORD0;
    float3 Normal : NORMAL0;
};

VertexOutput VertexMain(in VertexInput input)
{
    VertexOutput output = (VertexOutput) 0;

    output.Position = mul(input.Position, WorldViewProjection);
    output.Color = input.Color;
    output.TexCoord = input.TexCoord;
    output.Normal = input.Normal;
	
    return output;
}

float4 PixelMain(VertexOutput input) : COLOR
{
    float angle = dot(normalize(input.Normal), normalize(LightDirection));
    
    return input.Color * angle;
}

technique Technique1
{
    pass ShaderPass
    {
        VertexShader = compile VS_SHADERMODEL VertexMain();
        PixelShader = compile PS_SHADERMODEL PixelMain();
    }
}