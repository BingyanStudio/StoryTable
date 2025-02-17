using System;

namespace StoryTable
{
    [Parse("MENU")]
    public class Menu : Statement
    {
        public Menu(string[] args) : base(args)
        {
            content = args[0];
            target = int.Parse(args[1]);

            Mode = ExecuteMode.Next;
        }
        public override ExecuteMode Mode { get; init; }
        public override void Execute(Executor executor) => Provider.Menu(content, target, executor);
        private readonly string content;
        private readonly int target;
    }
}
