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
        public override void Execute() => Commands.Menu(content, target);
        private readonly string content;
        private readonly int target;
    }
}
