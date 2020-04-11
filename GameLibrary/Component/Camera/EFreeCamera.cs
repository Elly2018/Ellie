using OpenTK;
using System;

namespace GameLibrary.Component
{
    public class EFreeCamera : ECamera
    {
        public float MovementSpeed;
        public float RotateSpeed;
        public float Run;

        public EFreeCamera()
        {
            MovementSpeed = 3.0f;
            RotateSpeed = 2.0f;
            Run = 2.5f;
        }

        public override void OnUpdate()
        {
            if (EInput.IsKey(OpenTK.Input.Key.W))
            {
                actor.position += (actor.forward - actor.position) * (float)ETime.DeltaTime * MovementSpeed * (EInput.IsKey(OpenTK.Input.Key.ShiftLeft) ? Run : 1);
            }
            if (EInput.IsKey(OpenTK.Input.Key.S))
            {
                actor.position += (actor.backward - actor.position) * (float)ETime.DeltaTime * MovementSpeed * (EInput.IsKey(OpenTK.Input.Key.ShiftLeft) ? Run : 1);
            }
            if (EInput.IsKey(OpenTK.Input.Key.A))
            {
                actor.position += (actor.leftward - actor.position) * (float)ETime.DeltaTime * MovementSpeed * (EInput.IsKey(OpenTK.Input.Key.ShiftLeft) ? Run : 1);
            }
            if (EInput.IsKey(OpenTK.Input.Key.D))
            {
                actor.position += (actor.rightward - actor.position) * (float)ETime.DeltaTime * MovementSpeed * (EInput.IsKey(OpenTK.Input.Key.ShiftLeft) ? Run : 1);
            }
            if (EInput.IsKey(OpenTK.Input.Key.Q))
            {
                actor.position += (actor.upward - actor.position) * (float)ETime.DeltaTime * MovementSpeed * (EInput.IsKey(OpenTK.Input.Key.ShiftLeft) ? Run : 1);
            }
            if (EInput.IsKey(OpenTK.Input.Key.E))
            {
                actor.position += (actor.downward - actor.position) * (float)ETime.DeltaTime * MovementSpeed * (EInput.IsKey(OpenTK.Input.Key.ShiftLeft) ? Run : 1);
            }
            if (EInput.IsKey(OpenTK.Input.Key.Up))
            {
                actor.rotate = Quaternion.Multiply(actor.rotate, Quaternion.FromEulerAngles(RotateSpeed * (float)ETime.DeltaTime, 0, 0));
            }
            if (EInput.IsKey(OpenTK.Input.Key.Down))
            {
                actor.rotate = Quaternion.Multiply(actor.rotate, Quaternion.FromEulerAngles(-RotateSpeed * (float)ETime.DeltaTime, 0, 0));
            }
            if (EInput.IsKey(OpenTK.Input.Key.Left))
            {
                actor.rotate = Quaternion.Multiply(actor.rotate, Quaternion.FromEulerAngles(0, RotateSpeed * (float)ETime.DeltaTime, 0));
            }
            if (EInput.IsKey(OpenTK.Input.Key.Right))
            {
                actor.rotate = Quaternion.Multiply(actor.rotate, Quaternion.FromEulerAngles(0, -RotateSpeed * (float)ETime.DeltaTime, 0));
            }

            base.OnUpdate();
        }
    }
}
