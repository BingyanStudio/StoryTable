namespace StoryParser
{
    public static class Provider
    {
        private static IVisualProvider? visualProvider;
        public static void SetVisual(IVisualProvider provider) => visualProvider = provider;
        public static void Menu(string content, int target) => visualProvider!.Menu(content, target);
        public static void Say(string character, string sprite, string dialogue) => visualProvider!.Say(character, sprite, dialogue);
        private static IDataProvider? dataProvider;
        public static void SetData(IDataProvider provider) => dataProvider = provider;
        public static T GetValue<T>(string key) => dataProvider!.GetValue<T>(key);
        public static void SetValue<T>(string key, T value) => dataProvider!.SetValue(key, value);
        private static IFileProvider? fileProvider;
        public static void SetFile(IFileProvider provider) => fileProvider = provider;
    }
    /// <summary>
    /// 提供视觉表现相关的方法
    /// <br/>通过<see cref="Menu(string, int)"/>处理选项
    /// <br/>通过<see cref="Say(string, string, string)"/>处理对话
    /// </summary>
    public interface IVisualProvider
    {
        void Menu(string content, int target);
        void Say(string character, string sprite, string dialogue);
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
}
