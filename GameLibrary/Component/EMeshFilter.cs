using System;

namespace GameLibrary.Component
{
    [EComponentName("Mesh Filter")]
    public class EMeshFilter : EActorComponent
    {
        private EMesh _mesh;

        public EMeshFilter()
        {
        }

        public void SetMesh(EMesh m)
        {
            _mesh = m;
        }

        public EMesh GetMesh()
        {
            return _mesh;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }
    }
}
