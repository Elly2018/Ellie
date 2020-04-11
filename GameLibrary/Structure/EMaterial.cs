using System;

namespace GameLibrary
{
    public sealed class EMaterial : IDisposable
    {
        public EShader shader;
        public ETexture mainTexture;

        public EMaterial(EShader useShader)
        {
            shader = useShader;
        }

        public void Use()
        {
            shader.Use();
            mainTexture.Bind();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
