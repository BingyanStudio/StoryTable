using System;

namespace StoryParser
{
    public class Pause : Statement
    {
        public Pause(string[] args) : base(args) { }

        public override void Execute()
        {
            Executor.Pause = true;
            Executor.Complete();
        }        // public IStatement Dispatch(string[] parameters) => new Pause();
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

        public override void Execute()
        {
            Executor.EndWith(result);
            Executor.Pause = true;
            Executor.Complete();
        }
    }
}
