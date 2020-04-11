namespace GameLibrary.Shader
{
    public partial class BuildInShader
    {
        public const string DiffuseVertex =
            "\n" +
            "void main(void) \n" +
            "{ \n" +
            "   gl_Position = mvp * aPosition; \n" +
            "   vFragPos = vec3(model * aPosition); \n" +
            "   vTexcoord = aTexcoord; \n" +
            "   vNormal = normalize(aNormal); \n" +
            "   TBN[0] = (model * vec4(aTangent, 0.0)).xyz; \n" +
            "   TBN[1] = (model * vec4(aBitTangent, 0.0)).xyz; \n" +
            "   TBN[2] = (model * vec4(aNormal, 0.0)).xyz; \n" +
            "} \n"
            ;

        public const string DiffuseFragment =
            "\n" +
            "void main(void){ \n" +
            "   vec3 result = l_ambient * l_ambient_intensity;\n"+
            "   for(int i = 0; i < 3; i++) {\n" +
            "       result += CalcDirLight(dirLight[i]); \n" +
            "   }\n" +
            "   for(int i = 0; i < 4; i++) {\n" +
            "       result += CalcPointLight(pointLight[i]); \n" +
            "   }\n" +
            "   FragColor = texture (Tex00, vTexcoord) * vec4(result, 1.0);\n" +
            "}"
            ;
    }
}
