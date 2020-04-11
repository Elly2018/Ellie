using OpenTK;
using FileFormatWavefront;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using System;
using OpenTK.Graphics;

namespace GameLibrary
{
    public sealed class EMesh
    {
        private string _objectName;
        public string objectName
        {
            get
            {
                return _objectName;
            }
        }

        public Vector3[] vertice;
        public Vector3[] normal;
        public Vector2[] textureCoordinate;
        public EFace[][] faces;

        private int vertexArray;
        private int vertexBuffer;

        public VertexStruct[] vertexStruct
        {
            get
            {
                List<VertexStruct> result = new List<VertexStruct>();

                for (int i = 0; i < faces.Length; i++)
                {
                    /*
                    for (int j = 0; j < faces[i].Length; j++)
                    {
                        int v1 = faces[i][j].vertex;
                        int uv1 = faces[i][j].uv.HasValue ? faces[i][j].uv.Value : -1;
                        int n1 = faces[i][j].normal.HasValue ? faces[i][j].normal.Value : -1;

                        result.Add(new VertexStruct(
                            new Vector4(vertice[v1].X, vertice[v1].Y, vertice[v1].Z, 1),
                            Color4.HotPink,
                            (uv1 == -1 ? new Vector2(0, 0) : textureCoordinate[uv1]),
                            (n1 == -1 ? new Vector3(0, 0, 0) : normal[n1])
                            ));
                    }
                    */

                    int v0 = faces[i][0].vertex;
                    int uv0 = faces[i][0].uv.HasValue ? faces[i][0].uv.Value : -1;
                    int n0 = faces[i][0].normal.HasValue ? faces[i][0].normal.Value : -1;

                    for (int j = 1; j < faces[i].Length - 1; j++)
                    {
                        int v1 = faces[i][j].vertex;
                        int uv1 = faces[i][j].uv.HasValue ? faces[i][j].uv.Value : -1;
                        int n1 = faces[i][j].normal.HasValue ? faces[i][j].normal.Value : -1;

                        int v2 = faces[i][j + 1].vertex;
                        int uv2 = faces[i][j + 1].uv.HasValue ? faces[i][j + 1].uv.Value : -1;
                        int n2 = faces[i][j + 1].normal.HasValue ? faces[i][j + 1].normal.Value : -1;

                        Vector3 deltaPos1 = vertice[v1] - vertice[v0];
                        Vector3 deltaPos2 = vertice[v2] - vertice[v0];

                        Vector2 deltaUV1 = (uv0 == -1 ? new Vector2(0, 0) : textureCoordinate[uv1] - textureCoordinate[uv0]);
                        Vector2 deltaUV2 = (uv0 == -1 ? new Vector2(0, 0) : textureCoordinate[uv2] - textureCoordinate[uv0]);

                        float r = 1.0f / (deltaUV1.X * deltaUV2.Y - deltaUV1.Y * deltaUV2.X);

                        result.Add(new VertexStruct(
                            new Vector4(vertice[v0].X, vertice[v0].Y, vertice[v0].Z, 1),
                            Color4.HotPink,
                            (uv0 == -1 ? new Vector2(0, 0) : textureCoordinate[uv0]),
                            (n0 == -1 ? new Vector3(0, 0, 0) : normal[n0]),
                            (deltaPos1 * deltaUV2.Y - deltaPos2 * deltaUV1.Y) * r,
                            (deltaPos2 * deltaUV1.X - deltaPos1 * deltaUV2.X) * r
                            ));
                        result.Add(new VertexStruct(
                            new Vector4(vertice[v1].X, vertice[v1].Y, vertice[v1].Z, 1),
                            Color4.HotPink,
                            (uv1 == -1 ? new Vector2(0, 0) : textureCoordinate[uv1]),
                            (n1 == -1 ? new Vector3(0, 0, 0) : normal[n1]),
                            (deltaPos1 * deltaUV2.Y - deltaPos2 * deltaUV1.Y) * r,
                            (deltaPos2 * deltaUV1.X - deltaPos1 * deltaUV2.X) * r
                            ));
                        result.Add(new VertexStruct(
                            new Vector4(vertice[v2].X, vertice[v2].Y, vertice[v2].Z, 1),
                            Color4.HotPink,
                            (uv2 == -1 ? new Vector2(0, 0) : textureCoordinate[uv2]),
                            (n2 == -1 ? new Vector3(0, 0, 0) : normal[n2]),
                            (deltaPos1 * deltaUV2.Y - deltaPos2 * deltaUV1.Y) * r,
                            (deltaPos2 * deltaUV1.X - deltaPos1 * deltaUV2.X) * r
                            ));
                    }

                }

                return result.ToArray();
            }
        }

        public int[] indiceBuffer
        {
            get
            {
                List<int> result = new List<int>();

                for (int i = 0; i < faces.Length; i++)
                {
                    for (int j = 1; j < faces[i].Length - 1; j++)
                    {

                    }
                }

                return result.ToArray();
            }
        }

        public EMesh(string name)
        {
            _objectName = name;
        }

        public void Load(string path)
        {
            FileLoadResult<FileFormatWavefront.Model.Scene> fileLoadResult = FileFormatObj.Load(path, false);

            if(fileLoadResult.Model != null)
            {
                _objectName = fileLoadResult.Model.ObjectName;

                List<Vector3> buffer00 = new List<Vector3>();
                for (int i = 0; i < fileLoadResult.Model.Vertices.Count; i++)
                {
                    buffer00.Add(new Vector3(
                        fileLoadResult.Model.Vertices[i].x,
                        fileLoadResult.Model.Vertices[i].y,
                        fileLoadResult.Model.Vertices[i].z));
                }
                vertice = buffer00.ToArray();

                buffer00 = new List<Vector3>();
                for (int i = 0; i < fileLoadResult.Model.Normals.Count; i++)
                {
                    buffer00.Add(new Vector3(
                        fileLoadResult.Model.Normals[i].x,
                        fileLoadResult.Model.Normals[i].y,
                        fileLoadResult.Model.Normals[i].z));
                }
                normal = buffer00.ToArray();

                List<Vector2> buffer01 = new List<Vector2>();
                for (int i = 0; i < fileLoadResult.Model.Uvs.Count; i++)
                {
                    buffer01.Add(new Vector2(
                        fileLoadResult.Model.Uvs[i].u,
                        fileLoadResult.Model.Uvs[i].v));
                }
                textureCoordinate = buffer01.ToArray();

                List<List<EFace>> buffer02 = new List<List<EFace>>();
                for (int i = 0; i < fileLoadResult.Model.UngroupedFaces.Count; i++)
                {
                    buffer02.Add(new List<EFace>());
                    for (int j = 0; j < fileLoadResult.Model.UngroupedFaces[i].Indices.Count; j++)
                    {
                        buffer02[i].Add(new EFace()
                        {
                            vertex = fileLoadResult.Model.UngroupedFaces[i].Indices[j].vertex,
                            normal = fileLoadResult.Model.UngroupedFaces[i].Indices[j].normal,
                            uv = fileLoadResult.Model.UngroupedFaces[i].Indices[j].uv,
                        });
                    }
                }

                faces = new EFace[buffer02.Count][];
                for(int i = 0; i < buffer02.Count; i++)
                {
                    faces[i] = buffer02[i].ToArray();
                }
            }

            Compile();
        }

        public void Compile()
        {
            vertexArray = GL.GenVertexArray();
            GL.BindVertexArray(vertexArray);

            vertexBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
            GL.NamedBufferStorage(
                vertexBuffer,
                VertexStruct.Size * vertexStruct.Length,        // the size needed by this buffer
                vertexStruct,                           // data to initialize with
                BufferStorageFlags.MapWriteBit);    // at this point we will only write to the buffer

            // position
            GL.VertexArrayAttribBinding(vertexArray, 0, 0);
            GL.EnableVertexArrayAttrib(vertexArray, 0);
            GL.VertexArrayAttribFormat(
                vertexArray,
                0,                      // attribute index, from the shader location = 0
                4,                      // size of attribute, vec4
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                0);                     // relative offset, first item

            // color
            GL.VertexArrayAttribBinding(vertexArray, 1, 0);
            GL.EnableVertexArrayAttrib(vertexArray, 1);
            GL.VertexArrayAttribFormat(
                vertexArray,
                1,                      // attribute index, from the shader location = 1
                4,                      // size of attribute, vec4
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                16);                    // relative offset after a vec4

            // texture
            GL.VertexArrayAttribBinding(vertexArray, 2, 0);
            GL.EnableVertexArrayAttrib(vertexArray, 2);
            GL.VertexArrayAttribFormat(
                vertexArray,
                2,                      // attribute index, from the shader location = 1
                2,                      // size of attribute, vec4
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                32);                    // relative offset after a vec4

            // normal
            GL.VertexArrayAttribBinding(vertexArray, 3, 0);
            GL.EnableVertexArrayAttrib(vertexArray, 3);
            GL.VertexArrayAttribFormat(
                vertexArray,
                3,                      // attribute index, from the shader location = 1
                3,                      // size of attribute, vec4
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                40);                    // relative offset after a vec4

            // tangent
            GL.VertexArrayAttribBinding(vertexArray, 3, 0);
            GL.EnableVertexArrayAttrib(vertexArray, 3);
            GL.VertexArrayAttribFormat(
                vertexArray,
                3,                      // attribute index, from the shader location = 1
                3,                      // size of attribute, vec4
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                52);

            // bit tangent
            GL.VertexArrayAttribBinding(vertexArray, 3, 0);
            GL.EnableVertexArrayAttrib(vertexArray, 3);
            GL.VertexArrayAttribFormat(
                vertexArray,
                3,                      // attribute index, from the shader location = 1
                3,                      // size of attribute, vec4
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                64);

            GL.VertexArrayVertexBuffer(vertexArray, 0, vertexBuffer, IntPtr.Zero, VertexStruct.Size);
        }

        public void UseMesh()
        {
            GL.BindVertexArray(vertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
        }

    }

    public struct IndiceStruct
    {
        public const int Size = 3 * 4;
    }

    public struct VertexStruct
    {
        public const int Size = (4 + 4 + 3 + 3 + 3 + 2) * 4; // size of struct in bytes

        private readonly Vector4 _position;
        private readonly Color4 _color;
        private readonly Vector2 _textureCoordinate;
        private readonly Vector3 _normal;
        private readonly Vector3 _tangent;
        private readonly Vector3 _bittangent;

        public VertexStruct(Vector4 position, Color4 color,  Vector2 textureCoordinate, Vector3 normal, Vector3 tangent, Vector3 bittangent)
        {
            _position = position;
            _normal = normal;
            _color = color;
            _textureCoordinate = textureCoordinate;
            _tangent = tangent;
            _bittangent = bittangent;
        }
    }

    public struct EFace
    {
        public int vertex;
        public int? normal;
        public int? uv;
    }
}
