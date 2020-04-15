using OpenTK;
using System;

namespace GameLibrary.Shader
{
    public partial class BuildInShader
    {
        public sealed class ErrorColor : ShaderBase
        {
            public override string Fragment()
            {
                return 
                    VersionInFunc() +
                    @"
in vec4 vs_color;
out vec4 FragColor;

void main(void)
{
    FragColor = vs_color;
}
";
            }

            public override string Vertex()
            {
                return
                    VersionInFunc() +
                    MeshInFunc() +
                    @"
out vec4 vs_color;
void main(void)
{
    gl_Position = mvp * aPosition;
    vs_color = aColor;
}
";
            }
        }
    }

    public class EErrorColorMat : EMaterial
    {
        public EErrorColorMat() : base(ShaderFactory.Build(BuildinShader.ErrorColor))
        {
        }
    }
}
