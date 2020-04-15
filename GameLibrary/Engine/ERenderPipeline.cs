using GameLibrary.Component;
using GameLibrary.Shader;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;

namespace GameLibrary
{
    public sealed partial class EEngine
    {
        private TextureFrameBuffer sceneBuffer;

        private EMesh a;
        private EMaterial b;

        public void Init()
        {
            a = EPrimitiveMesh.GetPrimitiveMesh(EPrimitiveMesh.PrimitiveType.ScreenPlane);
            b = new EGammaCorrectionMat();

            sceneBuffer = new TextureFrameBuffer();
            
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, sceneBuffer.frameBuffer);

            GL.BindTexture(TextureTarget.Texture2D, sceneBuffer.texture); // color
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                                EWindow.GLWidth, EWindow.GLHeight,
                                0, (PixelFormat)PixelInternalFormat.Rgba, PixelType.UnsignedByte,
                                IntPtr.Zero);
            
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToBorder);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToBorder);
            GL.BindTexture(TextureTarget.Texture2D, 0);

            GL.BindTexture(TextureTarget.Texture2D, sceneBuffer.texture2); // depth
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent32,
                                EWindow.GLWidth, EWindow.GLHeight,
                                0, (PixelFormat)PixelInternalFormat.DepthComponent, PixelType.Int,
                                IntPtr.Zero);
            GL.BindTexture(TextureTarget.Texture2D, 0);

            ////Create color attachment texture
            GL.FramebufferTexture2D(FramebufferTarget.FramebufferExt, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, sceneBuffer.texture, 0);
            GL.FramebufferTexture2D(FramebufferTarget.FramebufferExt, FramebufferAttachment.DepthAttachment, TextureTarget.Texture2D, sceneBuffer.texture2, 0);

            //No need for renderbuffer object since we don't need stencil buffer yet
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        public void RenderShadow()
        {

        }

        public void RenderScene()
        {
            sceneBuffer.BindFrameBuffer();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(new Color4(0.05f, 0.05f, 0.05f, 1));

            GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);
            GL.PatchParameter(PatchParameterInt.PatchVertices, 3);
            GL.Enable(EnableCap.Blend);
            //GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture1D);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.TextureCubeMap);
            GL.Enable(EnableCap.LineSmooth);
            GL.Enable(EnableCap.PolygonSmooth);
            GL.Enable(EnableCap.Multisample);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);

            // Call drawing
            for (int i = 0; i < loadScene.Count; i++)
            {
                ELightSetting.Current = loadScene[i].lightSetting;
                EScene.Current = loadScene[i];

                if (loadScene[i] != null)
                {
                    for (int j = 0; j < loadScene[i].childLength; j++)
                    {
                        loadScene[i].GetChild(j).OnRender();
                    }
                }
            }

            sceneBuffer.UnBindFrameBuffer();
        }

        public void PostProcessing()
        {
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
            GL.ClearColor(new Color4(0.05f, 0.05f, 0.05f, 1));
            GL.Disable(EnableCap.CullFace);

            b.Use();
            a.UseMesh();

            Matrix4 model = Matrix4.Identity;
            GL.UniformMatrix4(20, false, ref model);
            GL.Uniform2(GL.GetUniformLocation(b.shader.program, "ScreenSize"), new Vector2(EWindow.GLWidth, EWindow.GLHeight));
            GL.ActiveTexture(TextureUnit.Texture0);
            sceneBuffer.BindTexture();

            GL.DrawArrays(PrimitiveType.Triangles, 0, a.vertexStruct.Length);

            sceneBuffer.UnBindTexture();
        }
    }

    public sealed class TextureFrameBuffer
    {
        private int _texture;
        public int texture
        {
            get
            {
                return _texture;
            }
        }

        private int _texture2;
        public int texture2
        {
            get
            {
                return _texture2;
            }
        }

        private int _frameBuffer;
        public int frameBuffer
        {
            get
            {
                return _frameBuffer;
            }
        }

        public TextureFrameBuffer()
        {
            _frameBuffer = GL.GenFramebuffer();
            GL.CreateTextures(TextureTarget.Texture2D, 1, out _texture);
            GL.CreateTextures(TextureTarget.Texture2D, 1, out _texture2);
        }

        public void CleanFrameBuffer()
        {

        }

        public void BindTexture()
        {
            GL.BindTexture(TextureTarget.Texture2D, texture);
        }

        public void BindTexture2()
        {
            GL.BindTexture(TextureTarget.Texture2D, texture2);
        }

        public void BindFrameBuffer()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, frameBuffer);
            GL.BlitFramebuffer(0, 0, EWindow.GLWidth, EWindow.GLHeight, 0, 0, EWindow.GLWidth, EWindow.GLHeight, ClearBufferMask.ColorBufferBit, BlitFramebufferFilter.Nearest);
        }

        public void UnBindTexture()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void UnBindFrameBuffer()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }
    }
}
