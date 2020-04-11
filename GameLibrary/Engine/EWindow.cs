using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace GameLibrary
{
    public abstract class EWindow : GameWindow
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        public EWindow(int width, int height) : base(width, height, new GraphicsMode(new ColorFormat(8, 8, 8, 8), 24, 8, 4), "Ellyality Demo")
        {   
            // Initialize the engine
            EEngine.Initialize();
            VSync = VSyncMode.Off;

            // Loading the asset into database
            EEngine.instance.assetDatabase = EAssetDatabase.Load();

            // Check if the loading successfully
            if(EAssetDatabase.GetResult().flag == LoadingResultFlag.FolderNotExist)
            {
                ELogger.Log(ELogger.LogType.Warning, ELoggerTag.Initialize, "Missing project folder");
                EAssetDatabase.CreateFolderStructure();
                EErrorReport.ExportReport(ELogger.GetLoggerMessage());
                Exit();
            }

            if(EAssetDatabase.GetResult().flag == LoadingResultFlag.Failed)
            {
                ELogger.Log(ELogger.LogType.Error, ELoggerTag.Initialize, "Loading asset failed");
                EErrorReport.ExportReport(ELogger.GetLoggerMessage());
                Exit();
            }

            // Loading the setting file into engine
            EEngine.instance.setting = EGameSetting.Loading();

            // Check if the loading successfully
            if (EGameSetting.GetResult().flag == LoadingResultFlag.FileNotExist)
            {
                ELogger.Log(ELogger.LogType.Warning, ELoggerTag.Initialize, "Missing project files");
                EGameSetting.CreateDefaultSetting();
                EErrorReport.ExportReport(ELogger.GetLoggerMessage());
                Exit();
            }

            if (EGameSetting.GetResult().flag == LoadingResultFlag.Failed)
            {
                ELogger.Log(ELogger.LogType.Error, ELoggerTag.Initialize, "Loading setting failed");
                EGameSetting.CreateDefaultSetting();
                EErrorReport.ExportReport(ELogger.GetLoggerMessage());
                Exit();
            }

            // If there is no scene has select
            // Create an empty scene for engine drawing
            if(EEngine.instance.setting.FirstLoadingScene == null || EEngine.instance.setting.FirstLoadingScene == "")
            {
                EEngine.instance.loadScene.Add(EScene.GetDefaultScene());
                Console.WriteLine("Default Scene");
            }

            // Hide console window
            var handle = GetConsoleWindow();
            ShowWindow(handle, EEngine.instance.setting.DebugMode ? SW_SHOW : SW_HIDE);

            Run(EEngine.instance.setting.FixedUpdateTime, (double)1 / (double)EEngine.instance.setting.FramePreSecond);
        }

        public abstract void ApplicationStart(EventArgs e);
        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            ApplicationStart(e);
            base.OnLoad(e);
        }

        public abstract void ApplicationUpdate(FrameEventArgs e);
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            ETime.PassTime += e.Time;
            ETime.DeltaTime = e.Time;

            EEngine.instance.OnUpdate();
            EEngine.instance.OnFixedUpdate();

            ApplicationUpdate(e);
            base.OnUpdateFrame(e);
        }

        public abstract void ApplicationRender(FrameEventArgs e);
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            this.Title = $"{"Ellyality Demo"}: (Vsync: {VSync}) FPS: {1f / e.Time:0}";
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(new Color4(0.05f, 0.05f, 0.05f, 1));

            GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);
            GL.PatchParameter(PatchParameterInt.PatchVertices, 3);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            //GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.Texture1D);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.TextureCubeMap);
            GL.Enable(EnableCap.LineSmooth);
            GL.Enable(EnableCap.PolygonSmooth);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.Enable(EnableCap.Multisample);

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);

            EEngine.instance.OnRender();

            // Driven class render function
            ApplicationRender(e);
            GL.PointSize(10);
            SwapBuffers();
            base.OnRenderFrame(e);
        }

        #region Input Related
        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            EInput.Key[(int)e.Key] = true;
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            EInput.Key[(int)e.Key] = false;
            base.OnKeyUp(e);
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            EInput.MousePositionDelta = new Vector2(e.XDelta, e.YDelta);
            EInput.MousePosition = new Vector2(e.X, e.Y);
            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
        }
        #endregion

        public abstract void ApplicationQuit(CancelEventArgs e);
        protected override void OnClosing(CancelEventArgs e)
        {
            ApplicationQuit(e);
            ELogger.LogOutput();
            base.OnClosing(e);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            base.OnResize(e);
        }
    }

    public sealed class EEngine
    {
        public const string BuildIn = "BuildIn/";

        private static EEngine _instance;
        public static EEngine instance
        {
            get
            {
                return _instance;
            }
        }

        public static void Initialize()
        {
            if(_instance == null)
            {
                _instance = new EEngine();
                _instance.loadScene = new List<EScene>();
                EInput.Initialize();
            }
        }

        public EAssetDatabase assetDatabase;
        public EGameSetting setting;
        public List<EScene> loadScene;

        private double passtime_fix = 0.0;

        public void OnUpdate() 
        {
            passtime_fix += ETime.DeltaTime;

            // Call update
            for(int i = 0; i < loadScene.Count; i++)
            {
                if(loadScene[i] != null)
                {
                    for(int j = 0; j < loadScene[i].childLength; j++)
                    {
                        loadScene[i].GetChild(j).OnUpdate();
                    }
                }
            }
        }
        public void OnFixedUpdate() 
        {
            if (passtime_fix > setting.FixedUpdateTime)
            {
                passtime_fix = 0;

                // Call fixedupdate
                for (int i = 0; i < loadScene.Count; i++)
                {
                    if (loadScene[i] != null)
                    {
                        for (int j = 0; j < loadScene[i].childLength; j++)
                        {
                            loadScene[i].GetChild(j).OnFixedUpdate();
                        }
                    }
                }
            }
        }
        public void OnRender() 
        {
            // Call drawing
            for (int i = 0; i < loadScene.Count; i++)
            {
                ELightSetting.Current = loadScene[i].lightSetting;
                EScene.Current = loadScene[i];

                if (loadScene[i] != null)
                {
                    for (int j = 0; j < loadScene[i].childLength; j++)
                    {
                        loadScene[i].GetChild(j).OnRender();
                    }
                }
            }
        }
    }
}
