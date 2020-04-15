using OpenTK;
using System;

namespace GameLibrary.Shader
{
    public partial class BuildInShader
    {
        public sealed class Lambert : ShaderBase
        {
            public override string Fragment()
            {
                return
                    VersionInFunc() +
                    VertexInStruct() +
                    LightInFunc_Fragment() +
                    @"
out vec4 FragColor;

void main(void){

    vec3 norm = normalize(vin.vNormal * 2.0 - 1.0);
    vec3 viewDir = normalize(vin.vViewPos - vin.vFragPos);
    
    vec3 result = material.ambient;

    for(int i = 0; i < NR_DIR_LIGHTS; i++){
        result += CalcDirLightLambert(dirLight[i], norm);
    }
    
    for(int i = 0; i < NR_POINT_LIGHTS; i++){
        result += CalcPointLightLambert(pointLight[i], norm, vin.vFragPos);
    }

    FragColor = vec4(result, 1.0);
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
    vout.vViewPos = cameraPos;
    vout.vFragPos = vec3(model * aPosition);
    vout.vTexcoord = aTexcoord;
    vout.vNormal = normalize(aNormal); 
    vout.TBN[0] = (model * vec4(aTangent, 0.0)).xyz; 
    vout.TBN[1] = (model * vec4(aBitTangent, 0.0)).xyz; 
    vout.TBN[2] = (model * vec4(aNormal, 0.0)).xyz; 
    vout.TBN = transpose(vout.TBN);
}
";
            }
        }
    }

    public class ELambertMat : EMaterial
    {
        public ELambertMat() : base(ShaderFactory.Build(BuildinShader.Lambert))
        {
            vec3Input.Add(new Tuple<Vector3, string>(new Vector3(0.05f, 0.05f, 0.05f), "material.ambient"));
            textureInput.Add(new Tuple<ETexture, int, string>(null, 0, "DiffuseTextureVaild"));
            floatInput.Add(new Tuple<float, string>(5.0f, "material.shininess"));
        }
    }
}
