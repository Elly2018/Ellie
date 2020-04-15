using OpenTK;
using System;

namespace GameLibrary.Shader
{
    public partial class BuildInShader
    {
        public sealed class GammaCorrection : ShaderBase
        {
            public override string Fragment()
            {
                return
                    VersionInFunc() +
                    VertexInStruct() +
                    FXAA() +
                    @"
out vec4 FragColor;
layout(binding=0) uniform sampler2D Screen;
uniform vec2 ScreenSize;
uniform int ScreenVaild;

void main(void)
{
    vec4 color = vec4(1.0);
    color.rgb = FXAA(Screen, vin.vTexcoord, vec2(1.0 / ScreenSize.x, 1.0 / ScreenSize.y));
    //color = texture(Screen, vin.vTexcoord);
    color.rgb = pow(color.rgb, vec3(1.0/2.2));
    FragColor = color;
}
";
            }

            public override string Vertex()
            {
                return
                    VersionInFunc() +
                    MeshInFunc() +
                    VertexOutStruct() +
                    @"
out vec4 vs_color;
void main(void)
{
    gl_Position = mvp * aPosition;
    vout.vTexcoord = aTexcoord; 
}
";
            }
        }
    }

    public class EGammaCorrectionMat : EMaterial
    {
        public EGammaCorrectionMat() : base(ShaderFactory.Build(BuildinShader.GammaCorrection))
        {

        }
    }
}
