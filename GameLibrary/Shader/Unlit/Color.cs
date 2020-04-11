namespace GameLibrary.Shader
{
    public partial class BuildInShader
    {
        public const string ColorVertex =
            "\n" +
            "out vec4 vs_color; \n" +
            "void main(void) \n" +
            "{ \n" +
            "   gl_Position = mvp * aPosition; \n" +
            "   vs_color = aColor; \n"+
            "} \n"
            ;

        public const string ColorFragment =
            "in vec4 vs_color; \n"+
            "\n" +
            "void main(void)\n" +
            "{\n" +
            "FragColor = vs_color; \n" +
            "}\n"
            ;
    }
}
