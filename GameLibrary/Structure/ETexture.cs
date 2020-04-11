using OpenTK.Graphics.OpenGL4;
using System.Drawing.Imaging;
using System.Drawing;
using System;
using System.IO;

namespace GameLibrary
{
    public sealed class ETexture : IDisposable
    {
        private string _textureName;
        public string textureName
        {
            get
            {
                return _textureName;
            }
        }

        private Bitmap bitmap;
        private int textureID;

        private int _minMipmapLevel = 0;
        private int _maxMipmapLevel = 8;

        public ETexture(string name)
        {
            _textureName = name;
            ELogger.Log(ELogger.LogType.Log, ELoggerTag.Texture, "Create new texture: " + textureID.ToString());
        }

        public void Load(string path)
        {
            if (File.Exists(path))
            {
                bitmap = (Bitmap)Image.FromFile(path);
                ELogger.Log(ELogger.LogType.Log, ELoggerTag.Texture, "Loading texture from path: " + path);
                Compile();
            }
            else
            {
                ELogger.Log(ELogger.LogType.Warning, ELoggerTag.Texture, "Cannot find texture from path: " + path);
            }
        }

        public void Compile()
        {
            float[] data = LoadTexture(bitmap);
            GL.CreateTextures(TextureTarget.Texture2D, 1, out textureID);
            GL.BindTexture(TextureTarget.Texture2D, textureID);

            GL.TextureStorage2D(textureID, _maxMipmapLevel, SizedInternalFormat.Rgba32f, bitmap.Width, bitmap.Height);
            GL.TextureSubImage2D(textureID, 0, 0, 0, bitmap.Width, bitmap.Height, OpenTK.Graphics.OpenGL4.PixelFormat.Rgba, PixelType.Float, data);
            
            GL.GenerateTextureMipmap(textureID);
            GL.TextureParameterI(textureID, TextureParameterName.TextureBaseLevel, ref _minMipmapLevel);
            GL.TextureParameterI(textureID, TextureParameterName.TextureMaxLevel, ref _maxMipmapLevel);
            var textureMinFilter = (int)TextureMinFilter.LinearMipmapLinear;
            GL.TextureParameterI(textureID, TextureParameterName.TextureMinFilter, ref textureMinFilter);
            var textureMagFilter = (int)TextureMinFilter.Linear;
            GL.TextureParameterI(textureID, TextureParameterName.TextureMagFilter, ref textureMagFilter);
            // data not needed from here on, OpenGL has the data

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            ELogger.Log(ELogger.LogType.Log, ELoggerTag.Texture, "Texture compile complete");
            ELogger.Log(ELogger.LogType.Log, ELoggerTag.Texture, "\tTexture width: " + bitmap.Width.ToString());
            ELogger.Log(ELogger.LogType.Log, ELoggerTag.Texture, "\tTexture height: " + bitmap.Height.ToString());
        }

        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, textureID);
        }

        public void Dispose()
        {
            GL.DeleteTexture(textureID);
        }

        private float[] LoadTexture(Bitmap map)
        {
            float[] r;
            int width = map.Width;
            int height = map.Height;
            r = new float[width * height * 4];
            int index = 0;

            BitmapData data = null;
            try
            {
                data = map.LockBits(
                    new Rectangle(0, 0, width, height),
                    ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format24bppRgb);


                unsafe
                {
                    var ptr = (byte*)data.Scan0;
                    int remain = data.Stride - data.Width * 3;
                    for (int i = 0; i < data.Height; i++)
                    {
                        for (int j = 0; j < data.Width; j++)
                        {
                            r[index++] = ptr[2] / 255f;
                            r[index++] = ptr[1] / 255f;
                            r[index++] = ptr[0] / 255f;
                            r[index++] = 1f;
                            ptr += 3;
                        }
                        ptr += remain;
                    }
                }
            }
            finally
            {
                map.UnlockBits(data);
            }

            return r;
        }
    }
}
