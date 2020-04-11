using NUnit.Framework;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;

namespace GameLibrary.Component
{
    [EComponentName("Mesh Renderer")]
    public class EMeshRenderer : EActorComponent
    {
        private EMaterial _material;

        public EMeshRenderer()
        {
        }

        public void SetMaterial(EMaterial s)
        {
            _material = s;
        }

        public override void OnRender()
        {
            if (_material == null) return;
            EMesh mesh = actor.GetComponent<EMeshFilter>().GetMesh();
            if(mesh != null)
            {
                _material.Use();
                mesh.UseMesh();

                Vector3 localp = actor.position;
                Quaternion localr = actor.rotate;
                Vector3 locals = actor.scale;

                Matrix4 model =
                    Matrix4.CreateTranslation(localp)
                    * Matrix4.CreateFromQuaternion(localr)
                    * Matrix4.CreateScale(locals);

                Matrix4 view = ECamera.mainCamera.lookMatrix;

                Matrix4 projection = ECamera.mainCamera.projection;

                Matrix4 m1 = model * view * projection;

                GL.UniformMatrix4(20, false, ref m1);
                GL.UniformMatrix4(21, false, ref model);
                GL.UniformMatrix4(22, false, ref view);
                GL.UniformMatrix4(23, false, ref projection);

                GL.Uniform4(50, 
                    new Vector4((float)ETime.PassTime / (float)10,
                    (float)ETime.PassTime / (float)5,
                    (float)ETime.PassTime / (float)3,
                    (float)ETime.PassTime / (float)1));

                // Prefix
                GL.Uniform3(GL.GetUniformLocation(_material.shader.program, "cameraPos"), ECamera.mainCamera.actor.position);

                // Ambient light
                GL.Uniform3(GL.GetUniformLocation(_material.shader.program, "l_ambient"), new Vector3(ELightSetting.Current.ambientColor.R, ELightSetting.Current.ambientColor.G, ELightSetting.Current.ambientColor.B));
                GL.Uniform1(GL.GetUniformLocation(_material.shader.program, "l_ambient_intensity"), ELightSetting.Current.ambientIntensity);

                // Direction Light
                for (int i = 0; i < EDirectionLight.GetAllDirectionLight.Length; i++)
                {
                    GL.Uniform3(GL.GetUniformLocation(_material.shader.program, $"dirLight[{i}].l_direction_dir"), EDirectionLight.GetAllDirectionLight[i].actor.forward);
                    GL.Uniform3(GL.GetUniformLocation(_material.shader.program, $"dirLight[{i}].l_direction_color"), new Vector3(EDirectionLight.GetAllDirectionLight[i].color.R, EDirectionLight.GetAllDirectionLight[i].color.G, EDirectionLight.GetAllDirectionLight[i].color.B));
                    GL.Uniform1(GL.GetUniformLocation(_material.shader.program, $"dirLight[{i}].l_direction_intensity"), EDirectionLight.GetAllDirectionLight[i].intensity);
                }

                // Point light
                for (int i = 0; i < EPointLight.GetAllPointLight.Length; i++)
                {
                    GL.Uniform3(GL.GetUniformLocation(_material.shader.program, $"pointLight[{i}].l_point_pos"), EPointLight.GetAllPointLight[i].actor.position);
                    GL.Uniform3(GL.GetUniformLocation(_material.shader.program, $"pointLight[{i}].l_point_color"), new Vector3(EPointLight.GetAllPointLight[i].color.R, EPointLight.GetAllPointLight[i].color.G, EPointLight.GetAllPointLight[i].color.B));
                    GL.Uniform1(GL.GetUniformLocation(_material.shader.program, $"pointLight[{i}].l_point_intensity"), EPointLight.GetAllPointLight[i].intensity);
                    GL.Uniform1(GL.GetUniformLocation(_material.shader.program, $"pointLight[{i}].l_point_linear"), EPointLight.GetAllPointLight[i].linear);
                    GL.Uniform1(GL.GetUniformLocation(_material.shader.program, $"pointLight[{i}].l_point_quadratic"), EPointLight.GetAllPointLight[i].quadratic);
                }

                GL.DrawArrays(PrimitiveType.Triangles, 0, mesh.vertexStruct.Length);
                //GL.DrawElements(PrimitiveType.Triangles, mesh.vertexStruct.Length, DrawElementsType.UnsignedInt, 0);
            }

            base.OnRender();
        }
    }
}
