namespace GameLibrary.Shader
{
    public partial class BuildInShader
    {
        public class General
        {
            public const string Prefix =
            "#version 450 core \n"
            ;

            public const string Tex00 =
                "uniform sampler2D Tex00; \n"
                ;
            public const string Tex01 =
                "uniform sampler2D Tex01; \n"
                ;
            public const string Tex02 =
                "uniform sampler2D Tex02; \n"
                ;
            public const string Tex03 =
                "uniform sampler2D Tex03; \n"
                ;
            public const string Tex04 =
                "uniform sampler2D Tex04; \n"
                ;
            public const string Tex05 =
                "uniform sampler2D Tex05; \n"
                ;
            public const string Tex06 =
                "uniform sampler2D Tex06; \n"
                ;
            public const string Tex07 =
                "uniform sampler2D Tex07; \n"
                ;
            public const string Tex08 =
                "uniform sampler2D Tex08; \n"
                ;
            public const string Tex09 =
                "uniform sampler2D Tex09; \n"
                ;

            public const string Time =
                "layout (location = 50) uniform vec4 p_time; \n"
                ;

            public const string CameraPos =
                "uniform vec3 cameraPos; \n"
                ;
        }

        public class ForVertex
        {
            public const string PrefixInput =
                "layout (location = 0) in vec4 aPosition; \n" +
                "layout (location = 1) in vec4 aColor; \n" +
                "layout (location = 2) in vec2 aTexcoord; \n" +
                "layout (location = 3) in vec3 aNormal; \n" +
                "layout (location = 4) in vec3 aTangent; \n" +
                "layout (location = 5) in vec3 aBitTangent; \n";

            public const string MVP =
                "layout (location = 20) uniform mat4 mvp; \n" +
                "layout (location = 21) uniform mat4 model; \n" +
                "layout (location = 22) uniform mat4 view; \n" +
                "layout (location = 23) uniform mat4 projection; \n"
                ;

            public const string outTextureCoord =
                "out vec2 vTexcoord; \n"
                ;

            public const string outNormal =
                "out vec3 vNormal; \n"
                ;

            public const string outFragPosition =
                "out vec3 vFragPos; \n"
                ;

            public const string outTBN =
                "out mat3 TBN; \n"
                ;
        }
        
        public class ForFragment
        {
            public const string FragColor =
                "out vec4 FragColor; \n"
                ;

            public const string VFragPosition =
                "in vec3 vFragPos; \n"
                ;

            public const string VViewPos =
                "in vec3 ViewPos;"
                ;

            public const string VTexturecoord =
                "in vec2 vTexcoord; \n"
                ;

            public const string VNormal =
                "in vec3 vNormal; \n"
                ;

            public const string VTBN =
                "in mat3 TBN; \n"
                ;
        }   

        public class ForLight
        {
            public const string l_ambient =
                "uniform vec3 l_ambient; \n" +
                "uniform float l_ambient_intensity; \n"
                ;

            public const string l_direction =
                "struct DirLight{ \n" +
                "   vec3 l_direction_dir; \n" +
                "   vec3 l_direction_color; \n" +
                "   float l_direction_intensity; \n" +
                "}; \n" +

                "uniform DirLight dirLight[3]; \n" +
                "\n" +

                "vec3 CalcDirLight(DirLight target) \n" +
                "{\n" +
                //"   vec3 viewDir = cameraPos - vFragPos; \n" +
                "   float p = max(dot(normalize(target.l_direction_dir), normalize(vNormal* TBN)), 0.0); \n" +
                "   return target.l_direction_color * p * target.l_direction_intensity; \n" +
                "}\n"
                ;

            public const string l_point =
                "struct PointLight{ \n" +
                "   vec3 l_point_pos; \n" +
                "   vec3 l_point_color;\n" +
                "   float l_point_intensity;\n" +
                "   float l_point_linear;\n" +
                "   float l_point_quadratic;\n" +
                "}; \n" +

                "uniform PointLight pointLight[4]; \n" +
                "\n" +

                "vec3 CalcPointLight(PointLight target) \n" +
                "{\n" +
                //"   vec3 viewDir = normalize(cameraPos - vFragPos); \n" +
                "   float distance = length(target.l_point_pos - vFragPos);\n" +
                "   vec3 lightDir = normalize(target.l_point_pos - vFragPos);\n" +
                "   float diff = max(dot(normalize(vNormal * TBN), lightDir), 0.0);\n" +
                "   float diffuse = diff = diff * (1.0 / (1.0 + target.l_point_linear * distance + (target.l_point_quadratic * distance * distance)));\n" +
                "   return target.l_point_color * diffuse * target.l_point_intensity; \n" +
                "}\n"
                ;
        }
    }
}
