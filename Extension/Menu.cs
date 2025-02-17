using System;

namespace StoryTable
{
    [Statement("MENU")]
    public class Menu : Statement
    {
        public Menu(ArgParser parser) : base(parser)
        {
            content = parser.String();
            target = parser.Int();

            Mode = ExecuteMode.Next;
        }
        public override ExecuteMode Mode { get; init; }
        public override void Execute(Executor executor) => Provider.Menu(content, target, executor);
        private readonly string content;
        private readonly int target;
    }
}
