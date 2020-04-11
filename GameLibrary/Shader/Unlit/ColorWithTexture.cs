namespace GameLibrary.Shader
{
    public partial class BuildInShader
    {
        public const string ColorWithTextureVertex =
            "\n" +
            "out vec4 vs_color; \n" +
            "void main(void) \n" +
            "{ \n" +
            "   gl_Position = mvp * aPosition; \n" +
            "   vTexcoord = aTexcoord; \n" +
            "   vs_color = aColor; \n" +
            "} \n"
            ;

        public const string ColorWithTextureFragment =
            "in vec4 vs_color; \n" +
            "\n" +
            "void main(void)\n" +
            "{\n" +
            "FragColor = texture (Tex00, vTexcoord) * vs_color; \n" +
            "}\n"
            ;
    }
}
