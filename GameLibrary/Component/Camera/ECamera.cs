using OpenTK;
using System;
using System.Collections.Generic;

namespace GameLibrary.Component
{
    [EComponentName("Camera")]
    public class ECamera : EActorComponent, IDisposable
    {
        public static List<ECamera> cameras = new List<ECamera>();
        public static ECamera mainCamera
        {
            get
            {
                return cameras[0];
            }
        }

        public float FOV;
        public int width;
        public int height;

        public float Near;
        public float Far;

        public Matrix4 projection
        {
            get
            {
                return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(FOV), (float)width / (float)height, Near, Far);
            }
        }

        public Matrix4 lookMatrix
        {
            get
            {
                
                return Matrix4.LookAt(actor.position, actor.forward, Vector3.UnitY);
            }
        }

        public ECamera()
        {
            FOV = 60f;
            width = 1280;
            height = 1080;
            Near = 0.5f;
            Far = 500.0f;

            ECamera.cameras.Add(this);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public void Dispose()
        {
            ECamera.cameras.Remove(this);
        }
    }
}
