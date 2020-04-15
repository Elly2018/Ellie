using GameLibrary.Component;
using GameLibrary.Shader;
using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;

namespace GameLibrary
{
    /// <summary>
    /// Scene is represent a current game space <br />
    /// Engine will depend on the scenes that is loaded to process and render
    /// </summary>
    [System.Serializable]
    public sealed class EScene
    {
        public static EScene Current;

        /// <summary>
        /// There are multiple game objects in the root <br />
        /// Which you can trace down the child one by one and get a full tree
        /// </summary>
        private List<EActor> gameObjects = new List<EActor>();
        public ELightSetting lightSetting = new ELightSetting();

        public int childLength
        {
            get
            {
                return gameObjects.Count;
            }
        }

        public void AddActorToScene(EActor actor)
        {
            gameObjects.Add(actor);
        }

        public EActor GetChild(int index)
        {
            return gameObjects[index];
        }

        public EActor[] ListAllActorInScene
        {
            get
            {
                List<EActor> result = new List<EActor>();

                // DFS getting the actor
                // Search all scene
                for(int i = 0; i < gameObjects.Count; i++)
                {
                    EActor sceneBuffer = gameObjects[i];

                    if (sceneBuffer == null) continue;

                    result.Add(sceneBuffer);
                    EActor[] allchild = sceneBuffer.ChildTreeToList();

                    if(allchild != null)
                    {
                        result.AddRange(allchild);
                    }
                }

                return result.ToArray();
            }
        }

        public static EScene GetDefaultScene()
        {
            EMaterial e = new EColorMat();
            // Empty scene
            EScene buffer = new EScene();

            // Add render stuff 03
            EActor obj = new EActor("floor");
            obj.position = new Vector3(0, -0.1f, 0);
            obj.scale = new Vector3(20f, 20f, 20f);

            EMeshFilter meshf = obj.AddComponent<EMeshFilter>();
            meshf.SetMesh(EPrimitiveMesh.GetPrimitiveMesh(EPrimitiveMesh.PrimitiveType.Plane));

            EMeshRenderer meshr = obj.AddComponent<EMeshRenderer>();
            meshr.SetMaterial(e);
            buffer.AddActorToScene(obj);

            return buffer;
        }
    }
}
