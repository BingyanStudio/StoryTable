namespace StoryTable
{
    public static class Provider
    {
        private static IVisualProvider visualProvider;
        public static void SetVisual(IVisualProvider provider) => visualProvider = provider;
        public static void Menu(string content, int target, Executor executor) => visualProvider.Menu(content, target, executor);
        public static void Say(string character, string sprite, string dialogue, Executor executor) => visualProvider.Say(character, sprite, dialogue, executor);

        private static IDataProvider dataProvider;
        public static void SetData(IDataProvider provider) => dataProvider = provider;
        public static T GetValue<T>(string key) => dataProvider.GetValue<T>(key);
        public static void SetValue<T>(string key, T value) => dataProvider.SetValue(key, value);

        private static IFileProvider fileProvider;
        public static void SetFile(IFileProvider provider) => fileProvider = provider;

        private static ILogProvider logProvider;
        public static void SetLog(ILogProvider provider) => logProvider = provider;
        public static void Message(string message) => logProvider.Message(message);
        public static void Warning(string warning) => logProvider.Warning(warning);
        public static void Error(string error) => logProvider.Error(error);
    }
    /// <summary>
    /// 提供视觉表现相关的方法
    /// <br/>通过<see cref="Menu(string, int)"/>处理选项
    /// <br/>通过<see cref="Say(string, string, string)"/>处理对话
    /// </summary>
    public interface IVisualProvider
    {
        void Menu(string content, int target, Executor executor);
        void Say(string character, string sprite, string dialogue, Executor executor);
    }
    /// <summary>
    /// 提供数据存取相关的方法
    /// <br/>通过<see cref="GetValue{T}(string)"/>读取数据
    /// <br/>通过<see cref="SetValue{T}(string, T)"/>存储数据
    /// </summary>
    public interface IDataProvider
    {
        T GetValue<T>(string key);
        void SetValue<T>(string key, T value);
    }
    public interface IFileProvider
    {

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
