using OpenTK;

namespace GameLibrary
{
    public sealed class EPrimitiveMesh
    {
        public enum PrimitiveType
        {
            Cube, Plane, Triangle, Sphere, Cone, Capsule, Cylinder, torus
        }

        public static EMesh GetPrimitiveMesh(PrimitiveType type)
        {
            switch (type)
            {
                case PrimitiveType.Cube:
                    EMesh cube = new EMesh("Cube");
                    cube.Load(EEngine.BuildIn + "cube.obj");
                    cube.Compile();
                    return cube;
                case PrimitiveType.Plane:
                    EMesh plane = new EMesh("Plane");
                    plane.Load(EEngine.BuildIn + "plane.obj");
                    plane.Compile();
                    return plane;
                case PrimitiveType.Triangle:
                    EMesh triangle = new EMesh("Triangle");
                    triangle.vertice = Triangle_Vertics;
                    triangle.Compile();
                    return triangle;
                case PrimitiveType.Sphere:
                    EMesh sphere = new EMesh("Sphere");
                    sphere.Load(EEngine.BuildIn + "sphere.obj");
                    sphere.Compile();
                    return sphere;
                case PrimitiveType.Cone:
                    break;
                case PrimitiveType.Capsule:
                    break;
                case PrimitiveType.Cylinder:
                    break;
                case PrimitiveType.torus:
                    break;
                default:
                    break;
            }
            return null;
        }

        #region Triangle
        public static Vector3[] Triangle_Vertics = new Vector3[]
        {
            new Vector3(-0.25f, 0.25f, 0.5f),
            new Vector3(0.0f, -0.25f, 0.5f),
            new Vector3(0.25f, 0.25f, 0.5f)
        };
        #endregion
    }
}
