using System;
using System.IO;
using System.Collections.Generic;

namespace GameLibrary
{
    [System.Serializable]
    public sealed class ELogger
    {
        public enum LogType
        {
            Log, Warning, Error
        }

        public struct LoggerMessage
        {
            public LogType type;
            public string tag;
            public string message;

            public LoggerMessage(LogType type, string tag, string message)
            {
                this.type = type;
                this.tag = tag;
                this.message = message;
                Console.WriteLine("[" + type.ToString() + "]" + " " + "[" + tag + "]" + " " + message);
            }
        }

        public static string LogFileName
        {
            get
            {
                string time = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-log";
                int index = 0;
                bool alreadyExist = true;
                while (!alreadyExist)
                {
                    alreadyExist = File.Exists(Path.Combine(FolderStructure.Log, time + index.ToString("0000") + ".txt"));
                    index++;
                }
                return Path.Combine(FolderStructure.Log, time + (index - 1).ToString("0000") + ".txt");
            }
        }
        private const string _unknownTag = "unknown";
        private static List<LoggerMessage> _message = new List<LoggerMessage>();

        #region Create | Clean Message
        public static void Log(LogType type, string tag, string message)
        {
            _message.Add(new LoggerMessage(type, tag, message));
        }

        public static void Log(string tag, string message)
        {
            _message.Add(new LoggerMessage(LogType.Log, tag, message));
        }

        public static void Log(string message)
        {
            _message.Add(new LoggerMessage(LogType.Log, _unknownTag, message));
        }

        public static void LogWarning(string tag, string message)
        {
            _message.Add(new LoggerMessage(LogType.Warning, tag, message));
        }

        public static void LogWarning(string message)
        {
            _message.Add(new LoggerMessage(LogType.Warning, _unknownTag, message));
        }

        public static void LogError(string tag, string message)
        {
            _message.Add(new LoggerMessage(LogType.Error, tag, message));
        }

        public static void LogError(string message)
        {
            _message.Add(new LoggerMessage(LogType.Error, _unknownTag, message));
        }

        public static void CleanMessage()
        {
            _message.Clear();
        }
        #endregion

        #region Get Logger Message Array
        public static string[] GetLoggerMessage()
        {
            List<string> result = new List<string>();

            for(int i = 0; i < _message.Count; i++)
            {
                result.Add("[" + _message[i].type.ToString() + "]" + " " + "[" + _message[i].tag + "]" + " " + _message[i].message);
            }

            return result.ToArray();
        }

        public static string[] GetLoggerMessageByTag(string tag)
        {
            List<string> result = new List<string>();

            for (int i = 0; i < _message.Count; i++)
            {
                if(_message[i].tag == tag)
                    result.Add("[" + _message[i].type.ToString() + "]" + " " + "[" + _message[i].tag + "]" + " " + _message[i].message);
            }

            return result.ToArray();
        }

        public static string[] GetLoggerMessageByType(LogType type)
        {
            List<string> result = new List<string>();

            for (int i = 0; i < _message.Count; i++)
            {
                if (_message[i].type == type)
                    result.Add("[" + _message[i].type.ToString() + "]" + " " + "[" + _message[i].tag + "]" + " " + _message[i].message);
            }

            return result.ToArray();
        }

        public static string[] GetLoggerMessageByTypeAndTag(LogType type, string tag)
        {
            List<string> result = new List<string>();

            for (int i = 0; i < _message.Count; i++)
            {
                if (_message[i].type == type && _message[i].tag == tag)
                    result.Add("[" + _message[i].type.ToString() + "]" + " " + "[" + _message[i].tag + "]" + " " + _message[i].message);
            }

            return result.ToArray();
        }

        public static string[] GetLoggerMessageBySearch(string keyword)
        {
            List<string> result = new List<string>();

            for (int i = 0; i < _message.Count; i++)
            {
                if (_message[i].message.Contains(keyword))
                    result.Add("[" + _message[i].type.ToString() + "]" + " " + "[" + _message[i].tag + "]" + " " + _message[i].message);
            }

            return result.ToArray();
        }
        #endregion

        public static void LogOutput()
        {
            string[] message = ELogger.GetLoggerMessage();
            string _m = "";
            for (int i = 0; i < message.Length; i++)
            {
                _m += message[i] + "\n";
            }

            File.WriteAllText(LogFileName, _m);
        }
    }

    /// <summary>
    /// Engine use log tag <br />
    /// We separate it to a class
    /// </summary>
    public sealed class ELoggerTag
    {
        public const string Initialize = "Initialize";
        public const string Shader = "Shader";
        public const string Texture = "Texture";
        public const string Material = "Material";
        public const string Scene = "Scene";
    }
}
