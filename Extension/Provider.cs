namespace StoryTable
{
    public partial class Provider
    {
        public IVisualProvider Visual { get; set; }
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
}
