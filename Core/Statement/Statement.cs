namespace StoryTable
{
    public enum ExecuteMode
    {
        Next, // 立刻执行下一句
        Lock, // 等待当前语句执行结束
        Pause, // 等待用户输入
    }
    public abstract class Statement
    {
        public Statement(string[] args) { }
        public abstract ExecuteMode Mode { get; init; }
        public abstract void Execute(Executor executor);
    }

    [Parse("END")]
    public class End : Statement
    {
        private readonly string result;

        public End(string[] args) : base(args)
        {
            if (args.Length > 0) result = args[0];
            else result = string.Empty;

            Mode = ExecuteMode.Pause;
        }
        public override ExecuteMode Mode { get; init; }
        public override void Execute(Executor executor)
        {
            executor.EndWith(result);
            executor.Pause = true;
            executor.Complete();
        }
    }
}
