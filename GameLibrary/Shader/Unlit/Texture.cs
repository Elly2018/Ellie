namespace GameLibrary.Shader
{
    public partial class BuildInShader
    {
        public const string TextureVertex =
            "\n" +
            "void main(void) \n" +
            "{ \n" +
            "   gl_Position = mvp * aPosition; \n" +
            "   vTexcoord = aTexcoord; \n" +
            "} \n"
            ;

        public const string TextureFragment =
            "\n" +
            "void main(void)\n" +
            "{\n" +
            "FragColor = texture (Tex00, vTexcoord); \n" +
            "}\n"
            ;
    }
}
