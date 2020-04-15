using OpenTK;

namespace GameLibrary
{
    public sealed class EPrimitiveMesh
    {
        public enum PrimitiveType
        {
            Cube, Plane, Triangle, Sphere, Cone, Capsule, Cylinder, torus, ScreenPlane
        }

        public static EMesh GetPrimitiveMesh(PrimitiveType type)
        {
            switch (type)
            {
                case PrimitiveType.Cube:
                    {
                        EMesh cube = new EMesh("Cube");
                        cube.Load(EEngine.BuildIn + "cube.obj");
                        cube.Compile();
                        return cube;
                    }
                case PrimitiveType.Plane:
                    {
                        EMesh plane = new EMesh("Plane");
                        plane.Load(EEngine.BuildIn + "plane.obj");
                        plane.Compile();
                        return plane;
                    }
                case PrimitiveType.Triangle:
                    {
                        break;
                    }
                case PrimitiveType.Sphere:
                    {
                        EMesh sphere = new EMesh("Sphere");
                        sphere.Load(EEngine.BuildIn + "sphere.obj");
                        sphere.Compile();
                        return sphere;
                    }
                case PrimitiveType.Cone:
                    {
                        break;
                    }
                case PrimitiveType.Capsule:
                    {
                        break;
                    }
                case PrimitiveType.Cylinder:
                    {
                        break;
                    }
                case PrimitiveType.torus:
                    {
                        break;
                    }
                case PrimitiveType.ScreenPlane:
                    {
                        EMesh screenPlane = new EMesh("ScreenPlane");
                        screenPlane.vertice = new Vector3[]
                        {
                            new Vector3(-1f, 1f, 0),
                            new Vector3(1f, 1f, 0),
                            new Vector3(1f, -1f, 0),
                            new Vector3(-1f, -1f, 0),
                        };
                        screenPlane.textureCoordinate = new Vector2[]
                        {
                            new Vector2(0, 1),
                            new Vector2(1, 1),
                            new Vector2(1, 0),
                            new Vector2(0, 0),
                        };
                        screenPlane.normal = new Vector3[]
                        {
                            new Vector3(0, 0, 1)
                        };
                        screenPlane.faces = new EFace[][]
                        {
                            new EFace[]
                            {
                                new EFace{ normal = 0, uv = 0, vertex = 0 },
                                new EFace{ normal = 0, uv = 1, vertex = 1 },
                                new EFace{ normal = 0, uv = 2, vertex = 2 },
                                new EFace{ normal = 0, uv = 3, vertex = 3 },
                            }
                        };
                        screenPlane.Compile();
                        return screenPlane;
                    }
            }
            return null;
        }
    }
}
