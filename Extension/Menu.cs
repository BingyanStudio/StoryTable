using System;

namespace StoryParser
{
    [Parse("MENU")]
    public class Menu : Statement
    {
        public Menu(string[] args) : base(args)
        {
            content = args[1];
            target = int.Parse(args[2]);
        }
        public override void Execute(Executor executor) => Provider.Menu(content, target, executor);
        private readonly string content;
        private readonly int target;
    }
}
