namespace StoryTable
{
    public enum ExecuteMode
    {
        Next, // 立刻执行下一句
        Wait, // 等待当前语句执行结束
        Pause, // 等待用户输入
    }
    public abstract class Statement
    {
        public Statement(ArgParser parser) { }
        public abstract ExecuteMode Mode { get; }
        public abstract void Execute(Executor executor);
    }

    [Statement("END")]
    public class End : Statement
    {
        private readonly string result;

        public End(ArgParser parser) : base(parser)
        {
            // if (args.Length > 0) result = args[0];
            // else result = string.Empty;
            result = parser.StringOr(string.Empty);
        }
        public override ExecuteMode Mode => ExecuteMode.Pause;
        public override void Execute(Executor executor)
        {
            executor.EndWith(result);
            executor.Pause = true;
            executor.Complete();
        }
    }
}
