using GameLibrary;
using GameLibrary.Component;
using GameLibrary.Shader;
using OpenTK;
using OpenTK.Graphics;
using System;
using System.ComponentModel;

namespace GameExample
{
    public class MyWindow : EWindow
    {
        public MyWindow(int width, int height) : base(width, height)
        {
        }

        static void Main(string[] args)
        {
            MyWindow app = new MyWindow(1280, 1080);
        }

        public override void ApplicationStart()
        {
            // Add texture
            ETexture t = new ETexture("test");
            t.Load(EEngine.BuildIn + "Test.png");

            EMaterial eMaterial = new ELambertMat();
            eMaterial.ModifyTex("DiffuseTextureVaild", t);

            // Add camera
            EActor obj = new EActor("cam");
            obj.position = new Vector3(0, 0, 0);
            obj.AddComponent<EFreeCamera>();
            EEngine.CurrentScene.AddActorToScene(obj);

            // Add render stuff
            obj = new EActor("Obj 01");
            obj.position = new Vector3(0, 0, -10);
            obj.scale = new Vector3(1f, 1f, 1f);

            EMeshFilter meshf = obj.AddComponent<EMeshFilter>();
            meshf.SetMesh(EPrimitiveMesh.GetPrimitiveMesh(EPrimitiveMesh.PrimitiveType.Cube));

            EMeshRenderer meshr = obj.AddComponent<EMeshRenderer>();
            meshr.SetMaterial(eMaterial);
            EEngine.CurrentScene.AddActorToScene(obj);

            // Add render stuff 02
            obj = new EActor("Obj 02");
            obj.position = new Vector3(5, 0, -10);
            obj.scale = new Vector3(1f, 1f, 1f);

            meshf = obj.AddComponent<EMeshFilter>();
            meshf.SetMesh(EPrimitiveMesh.GetPrimitiveMesh(EPrimitiveMesh.PrimitiveType.Cube));

            meshr = obj.AddComponent<EMeshRenderer>();
            meshr.SetMaterial(eMaterial);
            EEngine.CurrentScene.AddActorToScene(obj);

            // Add render stuff 04
            obj = new EActor("Obj 04");
            obj.position = new Vector3(1, 0, -2);
            obj.scale = new Vector3(1f, 1f, 1f);

            meshf = obj.AddComponent<EMeshFilter>();
            meshf.SetMesh(EPrimitiveMesh.GetPrimitiveMesh(EPrimitiveMesh.PrimitiveType.Sphere));

            meshr = obj.AddComponent<EMeshRenderer>();
            meshr.SetMaterial(eMaterial);
            EEngine.CurrentScene.AddActorToScene(obj);

            // Add direction light

            obj = new EActor("Dir light");
            obj.rotate = Quaternion.FromEulerAngles(new Vector3(-40, 125, 0));
            EDirectionLight dlight = obj.AddComponent<EDirectionLight>();
            dlight.color = new Color4(1.0f, 1.0f, 0.8f, 1.0f);
            dlight.intensity = 0.9f;
            EEngine.CurrentScene.AddActorToScene(obj);

            // Add point light
            obj = new EActor("Point light");
            obj.position = new Vector3(2, 3, -5);
            EPointLight plight = obj.AddComponent<EPointLight>();
            plight.color = Color4.Red;
            EEngine.CurrentScene.AddActorToScene(obj);

            // Add point light
            obj = new EActor("Point light");
            obj.position = new Vector3(-2, 3, -5);
            plight = obj.AddComponent<EPointLight>();
            plight.color = Color4.White;
            EEngine.CurrentScene.AddActorToScene(obj);
        }

        public override void ApplicationUpdate(FrameEventArgs e)
        {
            
        }
        
        public override void ApplicationRender(FrameEventArgs e)
        {
            
        }

        public override void ApplicationQuit(CancelEventArgs e)
        {
           
        }

    }
}
