namespace StoryParser
{
    public abstract class Statement
    {
        public Statement(string[] args) { }

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
        }

        public override void Execute(Executor executor)
        {
            executor.EndWith(result);
            executor.Pause = true;
            executor.Complete();
        }
    }
}
