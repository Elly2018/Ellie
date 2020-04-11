using OpenTK.Graphics;

namespace GameLibrary
{
    public sealed class ELightSetting
    {
        public static ELightSetting Current;

        private ESkybox skybox;
        public Color4 ambientColor = new Color4(1.0f, 1.0f, 0.95f, 1.0f);
        public float ambientIntensity = 0.3f;

        public ELightSetting()
        {

        }
    }
}
