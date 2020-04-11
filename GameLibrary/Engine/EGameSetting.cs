using System.IO;
using Newtonsoft.Json;

namespace GameLibrary
{
    /// <summary>
    /// Setting will define how the game act in the begining stage <br />
    /// Usually engine will load all the resources first <br />
    /// Then follow the setting file
    /// </summary>
    [System.Serializable]
    public sealed class EGameSetting
    {
        private static LoadingResult _lastLoadResult;

        public static string SettingFileName
        {
            get
            {
                return Path.Combine(FolderStructure.Setting, "setting.ini");
            }
        }

        public string FirstLoadingScene;
        public int FramePreSecond;
        public float FixedUpdateTime;
        public bool DebugMode;

        public EGameSetting()
        {
            FirstLoadingScene = "";
            FramePreSecond = 60;
            FixedUpdateTime = 0.004f;
            DebugMode = false;
        }

        public static EGameSetting Loading()
        {
            if (!File.Exists(SettingFileName))
            {
                _lastLoadResult = new LoadingResult() { flag = LoadingResultFlag.FileNotExist, message = "file path: " + SettingFileName + " does not exist" };
                ELogger.Log(ELogger.LogType.Warning, ELoggerTag.Initialize, _lastLoadResult.message);
                return null;
            }

            string jsonText = File.ReadAllText(SettingFileName);

            try
            {
                EGameSetting buffer = 
                JsonConvert.DeserializeObject<EGameSetting>(jsonText, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                });

                _lastLoadResult = new LoadingResult() { flag = LoadingResultFlag.Successfully, message = "Setting file loading successfully" };
                ELogger.Log(ELogger.LogType.Log, ELoggerTag.Initialize, _lastLoadResult.message);
                return buffer;
            }
            catch (JsonSerializationException ex)
            {
                _lastLoadResult = new LoadingResult() { flag = LoadingResultFlag.Failed, message = ex.Message };
                ELogger.Log(ELogger.LogType.Log, ELoggerTag.Initialize, _lastLoadResult.message);
                return null;
                // Could not find member 'DeletedDate' on object of type 'Account'. Path 'DeletedDate', line 4, position 23.
            }

        }

        /// <summary>
        /// Getting 
        /// </summary>
        /// <returns></returns>
        public static LoadingResult GetResult()
        {
            return _lastLoadResult;
        }

        public static void CreateDefaultSetting()
        {
            ELogger.Log(ELogger.LogType.Log, ELoggerTag.Initialize, "Craete default setting file");
            string jsonText = JsonConvert.SerializeObject(new EGameSetting(), Formatting.Indented);
            File.WriteAllText(SettingFileName, jsonText);
        }
    }
}
