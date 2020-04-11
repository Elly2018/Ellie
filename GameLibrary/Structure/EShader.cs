using System;
using OpenTK.Graphics.ES30;

namespace GameLibrary
{
    public sealed class EShader : IDisposable
    {
        private int _program;
        public int program
        {
            get
            {
                return _program;
            }
        }

        private int vertexShader;
        private int fragmentShader;

        public EShader()
        {
            _program = GL.CreateProgram();
            ELogger.Log(ELogger.LogType.Log, ELoggerTag.Shader, "Create new shader: " + _program.ToString());
        }

        public void SetVertexShader(string shader)
        {
            ELogger.Log(ELogger.LogType.Log, ELoggerTag.Shader, "Shader vertex code enter: \n\n" + shader + "\n");
            vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, shader);
            GL.CompileShader(vertexShader);

            string shaderLog = GL.GetShaderInfoLog(vertexShader);
            if (shaderLog != System.String.Empty)
                ELogger.Log(ELogger.LogType.Warning, ELoggerTag.Shader, shaderLog);

            GL.AttachShader(_program, vertexShader);

            GL.LinkProgram(_program);
        }

        public void SetVertexFragment(string shader)
        {
            ELogger.Log(ELogger.LogType.Log, ELoggerTag.Shader, "Shader fragment code enter: \n\n" + shader + "\n");
            fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, shader);
            GL.CompileShader(fragmentShader);

            string shaderLog = GL.GetShaderInfoLog(fragmentShader);
            if (shaderLog != System.String.Empty)
                ELogger.Log(ELogger.LogType.Warning, ELoggerTag.Shader, shaderLog);

            GL.AttachShader(_program, fragmentShader);

            GL.LinkProgram(_program);
        }

        public void SetShader(string vertex, string fragment)
        {
            ELogger.Log(ELogger.LogType.Log, ELoggerTag.Shader, "Shader vertex code enter: \n" + vertex);
            ELogger.Log(ELogger.LogType.Log, ELoggerTag.Shader, "Shader fragment code enter: \n" + fragment);

            vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertex);
            GL.CompileShader(vertexShader);
            string shaderLog = GL.GetShaderInfoLog(vertexShader);
            if (shaderLog != System.String.Empty)
                ELogger.Log(ELogger.LogType.Warning, ELoggerTag.Shader, shaderLog);

            GL.AttachShader(_program, vertexShader);

            fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragment);
            GL.CompileShader(vertexShader);
            shaderLog = GL.GetShaderInfoLog(fragmentShader);
            if (shaderLog != System.String.Empty)
                ELogger.Log(ELogger.LogType.Warning, ELoggerTag.Shader, shaderLog);

            GL.AttachShader(_program, fragmentShader);

            GL.LinkProgram(_program);
        }

        public void Use()
        {
            GL.UseProgram(_program);
        }

        public void Dispose()
        {
            GL.DetachShader(_program, vertexShader);
            GL.DetachShader(_program, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteProgram(_program);
        }
    }
}
