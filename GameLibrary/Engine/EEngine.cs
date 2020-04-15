using System.Collections.Generic;

namespace GameLibrary
{
    public sealed partial class EEngine
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
            if (_instance == null)
            {
                _instance = new EEngine();
                _instance.loadScene = new List<EScene>();
                EInput.Initialize();
                _instance.Init();
            }
        }

        public EAssetDatabase assetDatabase;
        public EGameSetting setting;
        public List<EScene> loadScene;
        public static EScene CurrentScene
        {
            get
            {
                return _instance.loadScene[0];
            }
        }

        private double passtime_fix = 0.0;

        public void OnUpdate()
        {
            passtime_fix += ETime.DeltaTime;

            // Call update
            for (int i = 0; i < loadScene.Count; i++)
            {
                if (loadScene[i] != null)
                {
                    for (int j = 0; j < loadScene[i].childLength; j++)
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
            //RenderShadow();

            RenderScene();

            PostProcessing();
        }
    }
}
