using System.Collections.Generic;
using System.IO;

namespace GameLibrary
{
    /// <summary>
    /// Asset database will loop the project file and return the full asset tree
    /// </summary>
    [System.Serializable]
    public sealed class EAssetDatabase
    {
        private static LoadingResult _lastLoadResult;

        /// <summary>
        /// Begin loading the asset folder
        /// </summary>
        /// <returns></returns>
        public static EAssetDatabase Load()
        {
            ELogger.Log(ELogger.LogType.Log, ELoggerTag.Initialize, "Asset start loading...");

            // Check project folder structure exist
            foreach(var i in FolderStructure.All)
            {
                if (!Directory.Exists(i))
                {
                    _lastLoadResult = new LoadingResult() { flag = LoadingResultFlag.FolderNotExist, message = "Missing " + i + " folder in the project root" };
                    ELogger.Log(ELogger.LogType.Warning, ELoggerTag.Initialize, _lastLoadResult.message);
                    return null;
                }
            }
            EAssetDatabase buffer = new EAssetDatabase();
            _lastLoadResult = new LoadingResult() { flag = LoadingResultFlag.Successfully, message = "Asset database loading successfully" };
            ELogger.Log(ELogger.LogType.Warning, ELoggerTag.Initialize, _lastLoadResult.message);
            return buffer;
        }

        public static void CreateFolderStructure()
        {
            ELogger.Log(ELogger.LogType.Log, ELoggerTag.Initialize, "Craete default project folder structure");

            foreach (var i in FolderStructure.All)
            {
                Directory.CreateDirectory(i);
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
    }

    public sealed class FolderStructure
    {
        public static string[] All = new string[]
        {
            Asset,
            Setting,
            Log
        };

        public const string Asset = "Asset";
        public const string Setting = "Setting";
        public const string Log = "Log";
    }
}
