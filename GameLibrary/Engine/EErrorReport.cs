using System.IO;

namespace GameLibrary
{
    [System.Serializable]
    public sealed class EErrorReport
    {
        public const string ErrorReportPath = "error.log";

        public static void ExportReport(string[] message)
        {
            string _m = "";
            for(int i = 0; i < message.Length; i++)
            {
                _m += message[i] + "\n";
            }

            if (File.Exists(ErrorReportPath)) File.Delete(ErrorReportPath);
            File.WriteAllText(ErrorReportPath, _m);
        }
    }
}
