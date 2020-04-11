namespace GameLibrary.Shader
{
    public partial class BuildInShader
    {
        public const string AmbientVertex =
            "\n" +
            "void main(void) \n" +
            "{ \n" +
            "   gl_Position = mvp * aPosition; \n" +
            "} \n"
            ;

        public const string AmbientFragment =
            "\n" +
            "void main(void)\n" +
            "{\n" +
            "FragColor = vec4(l_ambient, 1.0) * l_ambient_intensity; \n" +
            "}\n"
            ;
    }
}
