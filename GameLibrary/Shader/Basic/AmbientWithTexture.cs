namespace GameLibrary.Shader
{
    public partial class BuildInShader
    {
        public const string AmbientWithTextureVertex =
            "\n" +
            "void main(void) \n" +
            "{ \n" +
            "   gl_Position = mvp * aPosition; \n" +
            "   vTexcoord = aTexcoord; \n" +
            "} \n"
            ;

        public const string AmbientWithTextureFragment =
            "\n" +
            "void main(void)\n" +
            "{\n" +
            "FragColor = texture (Tex00, vTexcoord) * vec4(l_ambient, 1.0) * l_ambient_intensity; \n" +
            "}\n"
            ;
    }
}
