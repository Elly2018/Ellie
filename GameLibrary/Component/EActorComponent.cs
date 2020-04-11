using System;

namespace GameLibrary.Component
{
    public class EActorComponent
    {
        private EActor _actor;
        public EActor actor
        {
            get
            {
                return _actor;
            }
        }

        public void SetActor(EActor target)
        {
            _actor = target;
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

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class EComponentName : Attribute
    {
        public string name;
        public EComponentName(string name)
        {
            this.name = name;
        }
    }
}
