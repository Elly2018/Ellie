namespace GameLibrary.Shader
{
    public enum BuildinShader
    {
        Color,
        ColorWithTexture,
        Texture,
        Ambinet,
        AmbinetWithTexture,
        Diffuse,
        Specular,
        Phong
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
                        result.SetShader(
                            BuildInShader.General.Prefix +
                            BuildInShader.ForVertex.PrefixInput +
                            BuildInShader.ForVertex.MVP +
                            BuildInShader.ColorVertex,

                            BuildInShader.General.Prefix +
                            BuildInShader.General.Time +
                            BuildInShader.ForFragment.FragColor +
                            BuildInShader.ColorFragment);
                        break;
                    }

                case BuildinShader.ColorWithTexture:
                    {
                        result = new EShader();
                        result.SetShader(
                            BuildInShader.General.Prefix +
                            BuildInShader.ForVertex.PrefixInput +
                            BuildInShader.ForVertex.outTextureCoord +
                            BuildInShader.ForVertex.MVP +
                            BuildInShader.ColorWithTextureVertex,

                            BuildInShader.General.Prefix +
                            BuildInShader.General.Time +
                            BuildInShader.General.Tex00 +
                            BuildInShader.ForFragment.VTexturecoord +
                            BuildInShader.ForFragment.FragColor +
                            BuildInShader.ColorWithTextureFragment);
                        break;
                    }
                    
                case BuildinShader.Texture:
                    {
                        result = new EShader();
                        result.SetShader(
                            BuildInShader.General.Prefix +
                            BuildInShader.ForVertex.PrefixInput +
                            BuildInShader.ForVertex.outTextureCoord +
                            BuildInShader.ForVertex.MVP +
                            BuildInShader.TextureVertex,

                            BuildInShader.General.Prefix +
                            BuildInShader.General.Time +
                            BuildInShader.General.Tex00 +
                            BuildInShader.ForFragment.VTexturecoord +
                            BuildInShader.ForFragment.FragColor +
                            BuildInShader.TextureFragment);
                        break;
                    }
                    
                case BuildinShader.Ambinet:
                    {
                        result = new EShader();
                        result.SetShader(
                            BuildInShader.General.Prefix +
                            BuildInShader.ForVertex.PrefixInput +
                            BuildInShader.ForVertex.MVP +
                            BuildInShader.AmbientVertex,

                            BuildInShader.General.Prefix +
                            BuildInShader.ForLight.l_ambient +
                            BuildInShader.General.Time +
                            BuildInShader.ForFragment.FragColor +
                            BuildInShader.AmbientFragment);
                        break;
                    }

                case BuildinShader.AmbinetWithTexture:
                    {
                        result = new EShader();
                        result.SetShader(
                            BuildInShader.General.Prefix +
                            BuildInShader.ForVertex.PrefixInput +
                            BuildInShader.ForVertex.outTextureCoord +
                            BuildInShader.ForVertex.MVP +
                            BuildInShader.AmbientWithTextureVertex,

                            BuildInShader.General.Prefix +
                            BuildInShader.General.Tex00 +
                            BuildInShader.ForFragment.VTexturecoord +
                            BuildInShader.ForLight.l_ambient +
                            BuildInShader.General.Time +
                            BuildInShader.ForFragment.FragColor +
                            BuildInShader.AmbientWithTextureFragment);
                        break;
                    }

                case BuildinShader.Diffuse:
                    {
                        result = new EShader();
                        result.SetShader(
                            BuildInShader.General.Prefix +
                            BuildInShader.ForVertex.PrefixInput +
                            BuildInShader.ForVertex.outTextureCoord +
                            BuildInShader.ForVertex.outFragPosition +
                            BuildInShader.ForVertex.outNormal +
                            BuildInShader.ForVertex.outTBN +
                            BuildInShader.ForVertex.MVP +
                            BuildInShader.DiffuseVertex,

                            BuildInShader.General.Prefix +
                            BuildInShader.General.Time +
                            BuildInShader.General.Tex00 +
                            BuildInShader.General.CameraPos +
                            BuildInShader.ForFragment.VTexturecoord +
                            BuildInShader.ForFragment.VFragPosition +
                            BuildInShader.ForFragment.VNormal +
                            BuildInShader.ForFragment.VTBN +
                            BuildInShader.ForFragment.FragColor +
                            BuildInShader.ForLight.l_ambient +
                            BuildInShader.ForLight.l_direction +
                            BuildInShader.ForLight.l_point +
                            BuildInShader.DiffuseFragment);
                        break;
                    }

                case BuildinShader.Specular:
                    break;
                case BuildinShader.Phong:
                    break;
            }

            return result;
        }
    }
}
