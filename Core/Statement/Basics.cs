using System;

namespace StoryParser
{
    public class Pause : Statement
    {
        public Pause(string[] args) : base(args) { }

        public override void Execute(Executor executor)
        {
            executor.Pause = true;
            executor.Complete();
        }
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
