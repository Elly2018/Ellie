namespace GameLibrary
{
    /// <summary>
    /// Define loading state
    /// </summary>
    public enum LoadingResultFlag
    {
        Successfully, Failed, FolderNotExist, FileNotExist
    }

    /// <summary>
    /// Define loading result struct
    /// </summary>
    public struct LoadingResult
    {
        public LoadingResultFlag flag;
        public string message;
    }
}
