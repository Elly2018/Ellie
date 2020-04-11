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
            // Empty scene
            EScene buffer = new EScene();

            // Add shader
            EShader c = ShaderFactory.Build(BuildinShader.Diffuse);

            // Add texture
            ETexture t = new ETexture("test");
            t.Load(EEngine.BuildIn + "Test.png");
            t.Bind();

            EMaterial eMaterial = new EMaterial(c);
            eMaterial.mainTexture = t;

            // Add camera
            EActor obj = new EActor("cam");
            obj.position = new Vector3(0, 0, 0);
            obj.AddComponent<EFreeCamera>();
            buffer.AddActorToScene(obj);

            // Add render stuff
            obj = new EActor("Obj 01");
            obj.position = new Vector3(0, 0, -10);
            obj.scale = new Vector3(1f, 1f, 1f);

            EMeshFilter meshf = obj.AddComponent<EMeshFilter>();
            meshf.SetMesh(EPrimitiveMesh.GetPrimitiveMesh(EPrimitiveMesh.PrimitiveType.Cube));

            EMeshRenderer meshr = obj.AddComponent<EMeshRenderer>();
            meshr.SetMaterial(eMaterial);
            buffer.AddActorToScene(obj);

            // Add render stuff 02
            obj = new EActor("Obj 02");
            obj.position = new Vector3(5, 0, -10);
            obj.scale = new Vector3(1f, 1f, 1f);

            meshf = obj.AddComponent<EMeshFilter>();
            meshf.SetMesh(EPrimitiveMesh.GetPrimitiveMesh(EPrimitiveMesh.PrimitiveType.Cube));

            meshr = obj.AddComponent<EMeshRenderer>();
            meshr.SetMaterial(eMaterial);
            buffer.AddActorToScene(obj);

            // Add render stuff 03
            obj = new EActor("Obj 03");
            obj.position = new Vector3(0, -0.1f, 0);
            obj.scale = new Vector3(20f, 20f, 20f);

            meshf = obj.AddComponent<EMeshFilter>();
            meshf.SetMesh(EPrimitiveMesh.GetPrimitiveMesh(EPrimitiveMesh.PrimitiveType.Plane));

            meshr = obj.AddComponent<EMeshRenderer>();
            meshr.SetMaterial(eMaterial);
            buffer.AddActorToScene(obj);

            // Add render stuff 04
            obj = new EActor("Obj 04");
            obj.position = new Vector3(1, 0, -2);
            obj.scale = new Vector3(1f, 1f, 1f);

            meshf = obj.AddComponent<EMeshFilter>();
            meshf.SetMesh(EPrimitiveMesh.GetPrimitiveMesh(EPrimitiveMesh.PrimitiveType.Sphere));

            meshr = obj.AddComponent<EMeshRenderer>();
            meshr.SetMaterial(eMaterial);
            buffer.AddActorToScene(obj);

            // Add direction light

            obj = new EActor("Dir light");
            obj.rotate = Quaternion.FromEulerAngles(new Vector3(-40, 125, 0));
            EDirectionLight dlight = obj.AddComponent<EDirectionLight>();
            dlight.color = new Color4(1.0f, 1.0f, 0.8f, 1.0f);
            dlight.intensity = 0.9f;
            buffer.AddActorToScene(obj);

            // Add point light
            obj = new EActor("Point light");
            obj.position = new Vector3(0, 1, 5);
            EPointLight plight = obj.AddComponent<EPointLight>();
            plight.color = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
            plight.intensity = 3f;
            buffer.AddActorToScene(obj);

            // Add point light
            obj = new EActor("Point light");
            obj.position = new Vector3(0, 0, -12);
            plight = obj.AddComponent<EPointLight>();
            plight.color = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
            plight.intensity = 3f;
            buffer.AddActorToScene(obj);

            return buffer;
        }
    }
}
