namespace GameLibrary.Shader
{
    public enum BuildinShader
    {
        Color,
        ErrorColor,
        TintTexture,
        Texture,
        Lambert,
        Specular,
        Phong,
        Screen,
        GammaCorrection
    }

    public class ShaderFactory
    {
        public static EShader Build(BuildinShader type)
        {
            EShader result = null;

            switch (type)
            {
                case BuildinShader.Color:
                    {
                        result = new EShader();
                        result.SetShader(new BuildInShader.Color());
                        break;
                    }

                case BuildinShader.ErrorColor:
                    {
                        result = new EShader();
                        result.SetShader(new BuildInShader.ErrorColor());
                        break;
                    }

                case BuildinShader.TintTexture:
                    {
                        result = new EShader();
                        result.SetShader(new BuildInShader.TintTexture());
                        break;
                    }

                case BuildinShader.Texture:
                    {
                        result = new EShader();
                        result.SetShader(new BuildInShader.Texture());
                        break;
                    }

                case BuildinShader.Lambert:
                    {
                        result = new EShader();
                        result.SetShader(new BuildInShader.Lambert());
                        break;
                    }

                case BuildinShader.Specular:
                    {
                        break;
                    }
                case BuildinShader.Phong:
                    {
                        break;
                    }
                case BuildinShader.Screen:
                    {
                        result = new EShader();
                        result.SetShader(new BuildInShader.Texture());
                        break;
                    }

                case BuildinShader.GammaCorrection:
                    {
                        result = new EShader();
                        result.SetShader(new BuildInShader.GammaCorrection());
                        break;
                    }
            }

            return result;
        }
    }
}
