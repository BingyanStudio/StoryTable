namespace StoryTable
{
    public static class Provider
    {
        public static IVisualProvider Visual { get; set; }
        public static IDataProvider Data { get; set; }
        public static IFileProvider File { get; set; }
        public static ILogProvider Log { get; set; }
    }
    /// <summary>
    /// 提供视觉表现相关的方法
    /// <br/>通过<see cref="Menu(string, Locator)"/>处理选项
    /// <br/>通过<see cref="Say(string, string, string)"/>处理对话
    /// </summary>
    public interface IVisualProvider
    {
        void Menu(string content, Locator target, ExecutorBase executor);
        void Say(string character, string sprite, string dialogue, ExecutorBase executor);
    }
    /// <summary>
    /// 提供数据存取相关的方法
    /// <br/>通过<see cref="GetValue{T}(string)"/>读取数据
    /// <br/>通过<see cref="SetInt(string, int)"/>存储数据
    /// </summary>
    public interface IDataProvider
    {
        int GetInt(string key);
        void SetInt(string key, int value);
        string GetString(string key);
        void SetString(string key, string value);
    }
    /// <summary>
    /// 提供寻找文件相关的方法
    /// <br/>通过<see cref="FindFile(string, string)"/>寻找文件
    /// </summary>
    public interface IFileProvider
    {
        bool Find(string name);
    }
    /// <summary>
    /// 提供打印日志相关的方法
    /// </summary>
    public interface ILogProvider
    {
        void Message(string message);
        void Warning(string warning);
        void Error(string error);
    }
}
