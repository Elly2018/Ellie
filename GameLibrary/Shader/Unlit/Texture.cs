﻿using OpenTK;
using System;

namespace GameLibrary.Shader
{
    public partial class BuildInShader
    {
        public sealed class Texture : ShaderBase
        {
            public override string Fragment()
            {
                return
                    VersionInFunc() +
                    VertexInStruct() +
                    @"
out vec4 FragColor;
uniform vec3 Color;
uniform int TextureVaild = 0;
layout (binding=0) uniform sampler2D Surface;

void main(void)
{
    if(TextureVaild == 1)
    {
        FragColor = texture(Surface, vin.vTexcoord);
    }
    else
    {
        FragColor = vec4(Color, 1.0);
    }
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

void main(void) 
{ 
    gl_Position = mvp * aPosition; 
    vout.vTexcoord = aTexcoord; 
}
";
            }
        }
    }

    public class ETextureMat : EMaterial
    {
        public ETextureMat() : base(ShaderFactory.Build(BuildinShader.Texture))
        {
            vec3Input.Add(new Tuple<Vector3, string>(new Vector3(0.8f), "Color"));
            textureInput.Add(new Tuple<ETexture, int, string>(null, 0, "TextureVaild"));
        }
    }
}
