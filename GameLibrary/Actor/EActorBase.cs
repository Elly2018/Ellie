using GameLibrary.Component;
using OpenTK;
using System;
using System.Collections.Generic;

namespace GameLibrary
{
    public abstract class EActorBase
    {
        private string _name;
        public string name
        {
            get
            {
                return _name;
            }
        }

        public Quaternion rotate;
        public Vector3 scale;
        public Vector3 position;
        public Vector3 direction
        {
            get
            {
                Vector3 result = new Vector3();
                float an = 0.0f;
                return result;
            }
        }

        protected string tag;
        protected int layer;

        public EActorBase(string name)
        {
            scale = new Vector3(1, 1, 1);
            position = new Vector3(0, 0, 0);
            rotate = Quaternion.Identity;
            _name = name;
        }

        public virtual void OnLoad() { }
        public virtual void OnEnable() { }
        public virtual void OnUpdate() { }
        public virtual void OnFixedUpdate() { }
        public virtual void OnLateUpdate() { }
        public virtual void OnRender() { }
        public virtual void OnDisable() { }
        public virtual void OnDestroy() { }
    }
}
