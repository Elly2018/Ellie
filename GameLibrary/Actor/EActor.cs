using GameLibrary.Component;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GameLibrary
{
    /// <summary>
    /// Actor is the basic game object <br />
    /// It contain the basic needs for a object exist in scene
    /// </summary>
    public class EActor : EActorBase
    {
        public EActor parent
        {
            get
            {
                return FindActorParentInChildren(this);
            }
        }

        public Vector3 forward
        {
            get
            {
                return (rotate * -Vector3.UnitZ) + position;
            }
        }

        public Vector3 backward
        {
            get
            {
                return (rotate * Vector3.UnitZ) + position;
            }
        }

        public Vector3 leftward
        {
            get
            {
                return (rotate * -Vector3.UnitX) + position;
            }
        }

        public Vector3 rightward
        {
            get
            {
                return (rotate * Vector3.UnitX) + position;
            }
        }

        public Vector3 upward
        {
            get
            {
                return (rotate * Vector3.UnitY) + position;
            }
        }

        public Vector3 downward
        {
            get
            {
                return (rotate * -Vector3.UnitY) + position;
            }
        }

        protected List<EActor> children = new List<EActor>();
        public int childLength
        {
            get
            {
                return children.Count;
            }
        }

        protected List<EActorComponent> components;

        public EActor(string name) : base(name)
        {
            ELogger.Log(ELogger.LogType.Log, ELoggerTag.Scene, "Adding actor to scene: " + name);
            components = new List<EActorComponent>();
            OnLoad();
        }

        public override void OnUpdate()
        {
            for(int i = 0; i < components.Count; i++)
            {
                components[i].OnUpdate();
            }
            for (int i = 0; i < children.Count; i++)
            {
                children[i].OnUpdate();
            }
            base.OnUpdate();
        }

        public override void OnRender()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].OnRender();
            }
            for (int i = 0; i < children.Count; i++)
            {
                children[i].OnRender();
            }
            base.OnRender();
        }

        public void SetFirstChild(EActor target)
        {
            children.Insert(0, target);
        }

        public void SetLastChild(EActor target)
        {
            children.Add(target);
        }

        public T AddComponent<T>() where T : EActorComponent, new()
        {
            T buffer = new T();
            components.Add(buffer);
            MethodInfo MI = buffer.GetType().GetMethod("SetActor");
            MI.Invoke(buffer, new object[] { this });
            return buffer;
        }

        public T GetComponent<T>() where T : EActorComponent
        {
            foreach (var i in components)
            {
                if (i.GetType() == typeof(T)) return i as T;
            }
            return null;
        }

        public EActor GetChild(int index)
        {
            return children[index];
        }

        private EActor FindActorParentInChildren(EActor target)
        {
            // DFS getting the parent
            // Search all scene
            for (int i = 0; i < EEngine.instance.loadScene.Count; i++)
            {
                EScene sceneBuffer = EEngine.instance.loadScene[i];
                if (sceneBuffer == null) continue;

                // If scene have one more root objects
                if (sceneBuffer.childLength > 0)
                {
                    // Loop each object in scene root
                    for (int j = 0; j < sceneBuffer.GetChild(j).childLength; j++)
                    {
                        EActor current = sceneBuffer.GetChild(j);

                        // If the target is exist in the looping child
                        if (current.ExistInChild(target))
                        {
                            // Get the path to the target
                            EActor[] path = current.GetPathToChild(target);

                            for (int k = 0; k < path.Length; k++)
                            {
                                // Previous one is the parent
                                if (path[k] == target) return path[k - 1];
                            }
                        }
                    }
                }
            }

            return null;
        }

        public bool ExistInChild(EActor target)
        {
            bool Last = false;
            if (childLength == 0) return false;

            // The target eactor and the amount of path has attempt
            List<SearchTreeMark> memory = new List<SearchTreeMark>();
            memory.Add(new SearchTreeMark(this, 0));
            bool traceBack = false;

            while (!Last)
            {
                EActor current = memory[memory.Count - 1].actor;

                if (!traceBack)
                {
                    if (current.childLength == 0)
                    {
                        // Leaf
                        // Start traceback 
                        traceBack = true;
                    }
                    else if (current.childLength == 1)
                    {
                        // single path
                        memory[memory.Count - 1].attempt++;
                        memory.Add(new SearchTreeMark(current.GetChild(0), 0));
                        if (current.GetChild(0) == target) return true;
                    }
                    else if (current.childLength >= 2)
                    {
                        // mark point
                        memory[memory.Count - 1].attempt++;
                        int att = memory[memory.Count - 1].attempt;
                        memory.Add(new SearchTreeMark(current.GetChild(att - 1), 0));
                        if (current.GetChild(0) == target) return true;
                    }
                }
                else
                {
                    memory.RemoveAt(memory.Count - 1);

                    for (int i = memory.Count - 1; i >= 0; i--)
                    {
                        if (memory[i].actor.childLength > memory[i].attempt)
                        {
                            // need more attempt
                            traceBack = false;
                        }
                        else
                        {
                            memory.RemoveAt(i);
                        }
                    }

                    if (memory.Count == 0 && memory[memory.Count - 1].attempt == memory[memory.Count - 1].actor.childLength)
                        Last = true;
                }
            }
            return false;
        }

        public EActor[] ChildTreeToList()
        {
            bool Last = false;
            if (childLength == 0) return null;

            // The target eactor and the amount of path has attempt
            List<SearchTreeMark> memory = new List<SearchTreeMark>();
            List<EActor> result = new List<EActor>();

            memory.Add(new SearchTreeMark(this, 0));
            bool traceBack = false;

            while (!Last)
            {
                EActor current = memory[memory.Count - 1].actor;

                if (!traceBack)
                {
                    if (current.childLength == 0)
                    {
                        // Leaf
                        // Start traceback 
                        traceBack = true;
                    }
                    else if (current.childLength == 1)
                    {
                        // single path
                        memory[memory.Count - 1].attempt++;
                        memory.Add(new SearchTreeMark(current.GetChild(0), 0));

                        if (!result.Contains(current.GetChild(0)))
                        {
                            result.Add(current.GetChild(0));
                        }
                    }
                    else if (current.childLength >= 2)
                    {
                        // mark point
                        memory[memory.Count - 1].attempt++;
                        int att = memory[memory.Count - 1].attempt;
                        memory.Add(new SearchTreeMark(current.GetChild(att - 1), 0));

                        if (!result.Contains(current.GetChild(att - 1)))
                        {
                            result.Add(current.GetChild(att - 1));
                        }
                    }
                }
                else
                {
                    memory.RemoveAt(memory.Count - 1);

                    for (int i = memory.Count - 1; i >= 0; i--)
                    {
                        if (memory[i].actor.childLength > memory[i].attempt)
                        {
                            // need more attempt
                            traceBack = false;
                        }
                        else
                        {
                            memory.RemoveAt(i);
                        }
                    }

                    if (memory.Count == 0 && memory[memory.Count - 1].attempt == memory[memory.Count - 1].actor.childLength)
                        Last = true;
                }
            }
            return result.ToArray();
        }

        public EActor[] GetPathToChild(EActor target)
        {
            if (!ExistInChild(target)) return null;

            bool Last = false;
            // The target eactor and the amount of path has attempt
            List<SearchTreeMark> memory = new List<SearchTreeMark>();
            memory.Add(new SearchTreeMark(this, 0));
            bool traceBack = false;
            while (!Last)
            {
                EActor current = memory[memory.Count - 1].actor;

                if (!traceBack)
                {
                    if (current.childLength == 0)
                    {
                        // Leaf
                        // Start traceback 
                        traceBack = true;
                    }
                    else if (current.childLength == 1)
                    {
                        // single path
                        memory[memory.Count - 1].attempt++;
                        memory.Add(new SearchTreeMark(current.GetChild(0), 0));
                        if (current.GetChild(0) == target) return SearchTreeMark.ToArray(memory.ToArray());
                    }
                    else if (current.childLength >= 2)
                    {
                        // mark point
                        memory[memory.Count - 1].attempt++;
                        int att = memory[memory.Count - 1].attempt;
                        memory.Add(new SearchTreeMark(current.GetChild(att - 1), 0));
                        if (current.GetChild(0) == target) return SearchTreeMark.ToArray(memory.ToArray());
                    }
                }
                else
                {
                    memory.RemoveAt(memory.Count - 1);

                    for (int i = memory.Count - 1; i >= 0; i--)
                    {
                        if (memory[i].actor.childLength > memory[i].attempt)
                        {
                            // need more attempt
                            traceBack = false;
                        }
                        else
                        {
                            memory.RemoveAt(i);
                        }
                    }

                    if (memory.Count == 0 && memory[memory.Count - 1].attempt == memory[memory.Count - 1].actor.childLength)
                        Last = true;
                }
            }
            return null;
        }

        public class SearchTreeMark
        {
            public EActor actor = null;
            public int attempt = 0;

            public SearchTreeMark(EActor actor, int attempt)
            {
                this.actor = actor;
                this.attempt = attempt;
            }

            public static EActor[] ToArray(SearchTreeMark[] stm)
            {
                EActor[] result = new EActor[stm.Length];
                for (int i = 0; i < stm.Length; i++)
                {
                    result[i] = stm[i].actor;
                }
                return result;
            }
        }
    }
}
