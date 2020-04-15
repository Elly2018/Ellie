using GameLibrary.Shader;
using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK;
using OpenTK.Graphics;
using System.Collections.Generic;

namespace GameLibrary
{
    public class EMaterial
    {
        private static EMaterial _DefaultMaterial;
        public static EMaterial DefaultMaterial
        {
            get
            {
                if(_DefaultMaterial == null)
                {
                    _DefaultMaterial = new EErrorColorMat();
                }
                return _DefaultMaterial;
            }
        }

        private EShader _shader = null;
        public EShader shader
        {
            get
            {
                if (_shader == null)
                {
                    ELogger.Log(ELogger.LogType.Error, ELoggerTag.Material, "Cannot find shader ! : " + this.GetType().Name);
                    return DefaultMaterial.shader;
                }
                else
                {
                    return _shader;
                }
            }
            set
            {
                _shader = value;
            }
        }

        public List<Tuple<float, string>> floatInput = new List<Tuple<float, string>>();
        public List<Tuple<int, string>> intInput = new List<Tuple<int, string>>();
        public List<Tuple<Vector2, string>> vec2Input = new List<Tuple<Vector2, string>>();
        public List<Tuple<Vector3, string>> vec3Input = new List<Tuple<Vector3, string>>();
        public List<Tuple<Vector4, string>> vec4Input = new List<Tuple<Vector4, string>>();
        public List<Tuple<Matrix3, string>> mat3Input = new List<Tuple<Matrix3, string>>();
        public List<Tuple<Matrix4, string>> mat4Input = new List<Tuple<Matrix4, string>>();
        public List<Tuple<ETexture, int, string>> textureInput = new List<Tuple<ETexture, int, string>>();

        public EMaterial(EShader useShader)
        {
            shader = useShader;
            ELogger.Log(ELogger.LogType.Log, ELoggerTag.Material, "Adding material: " + this.GetType().Name);
        }

        public void Use()
        {
            shader.Use();

            foreach (var i in floatInput)
            {
                GL.Uniform1(GL.GetUniformLocation(shader.program, i.Item2), i.Item1);
            }
            foreach (var i in intInput)
            {
                GL.Uniform1(GL.GetUniformLocation(shader.program, i.Item2), i.Item1);
            }
            foreach (var i in vec2Input)
            {
                GL.Uniform2(GL.GetUniformLocation(shader.program, i.Item2), i.Item1);
            }
            foreach (var i in vec3Input)
            {
                GL.Uniform3(GL.GetUniformLocation(shader.program, i.Item2), i.Item1);
            }
            foreach (var i in vec4Input)
            {
                GL.Uniform4(GL.GetUniformLocation(shader.program, i.Item2), i.Item1);
            }
            foreach (var i in mat3Input)
            {
                Matrix3 buffer = i.Item1;
                GL.UniformMatrix3(GL.GetUniformLocation(shader.program, i.Item2), false, ref buffer);
            }
            foreach (var i in mat4Input)
            {
                Matrix4 buffer = i.Item1;
                GL.UniformMatrix4(GL.GetUniformLocation(shader.program, i.Item2), false, ref buffer);
            }
            foreach (var i in textureInput)
            {
                if (i.Item1 == null)
                {
                    GL.Uniform1(GL.GetUniformLocation(shader.program, i.Item3), 0);
                }
                else
                {
                    TextureUnit tu = (TextureUnit)33984 + i.Item2;
                    GL.ActiveTexture(tu);
                    i.Item1.Bind();
                    GL.Uniform1(GL.GetUniformLocation(shader.program, i.Item3), 1);
                }

            }
        }

        public void ModifyFloat(string name, float value)
        {
            for(int i = 0; i < floatInput.Count; i++)
            {
                if (floatInput[i].Item2 == name)
                {
                    floatInput[i] = new Tuple<float, string>(value, floatInput[i].Item2);
                    return;
                }
            }
        }

        public float? GetFloat(string name)
        {
            for (int i = 0; i < floatInput.Count; i++)
            {
                if (floatInput[i].Item2 == name)
                {
                    return floatInput[i].Item1;
                }
            }
            return null;
        }

        public void ModifyInt(string name, int value)
        {
            for (int i = 0; i < intInput.Count; i++)
            {
                if (intInput[i].Item2 == name)
                {
                    intInput[i] = new Tuple<int, string>(value, intInput[i].Item2);
                    return;
                }
            }
        }

        public int? GetInt(string name)
        {
            for (int i = 0; i < intInput.Count; i++)
            {
                if (intInput[i].Item2 == name)
                {
                    return intInput[i].Item1;
                }
            }
            return null;
        }

        public void ModifyVec2(string name, Vector2 value)
        {
            for (int i = 0; i < vec2Input.Count; i++)
            {
                if (vec2Input[i].Item2 == name)
                {
                    vec2Input[i] = new Tuple<Vector2, string>(value, vec2Input[i].Item2);
                    return;
                }
            }
        }

        public Vector2? GetVec2(string name)
        {
            for (int i = 0; i < vec2Input.Count; i++)
            {
                if (vec2Input[i].Item2 == name)
                {
                    return vec2Input[i].Item1;
                }
            }
            return null;
        }

        public void ModifyVec3(string name, Vector3 value)
        {
            for (int i = 0; i < vec3Input.Count; i++)
            {
                if (vec3Input[i].Item2 == name)
                {
                    vec3Input[i] = new Tuple<Vector3, string>(value, vec3Input[i].Item2);
                    return;
                }
            }
        }

        public Vector3? GetVec3(string name)
        {
            for (int i = 0; i < vec3Input.Count; i++)
            {
                if (vec3Input[i].Item2 == name)
                {
                    return vec3Input[i].Item1;
                }
            }
            return null;
        }

        public void ModifyVec4(string name, Vector4 value)
        {
            for (int i = 0; i < vec4Input.Count; i++)
            {
                if (vec4Input[i].Item2 == name)
                {
                    vec4Input[i] = new Tuple<Vector4, string>(value, vec4Input[i].Item2);
                    return;
                }
            }
        }

        public Vector4? GetVec4(string name)
        {
            for (int i = 0; i < vec4Input.Count; i++)
            {
                if (vec4Input[i].Item2 == name)
                {
                    return vec4Input[i].Item1;
                }
            }
            return null;
        }

        public void ModifyTex(string name, ETexture value)
        {
            for (int i = 0; i < textureInput.Count; i++)
            {
                if (textureInput[i].Item3 == name)
                {
                    textureInput[i] = new Tuple<ETexture, int, string>(value, textureInput[i].Item2, textureInput[i].Item3);
                    return;
                }
            }
        }

        public ETexture GetTex(string name)
        {
            for (int i = 0; i < textureInput.Count; i++)
            {
                if (textureInput[i].Item3 == name)
                {
                    return textureInput[i].Item1;
                }
            }
            return null;
        }
    }
}
