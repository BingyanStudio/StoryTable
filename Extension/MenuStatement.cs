using System;

namespace StoryTable
{
    [Statement("MENU")]
    public class MenuStatement : Statement
    {
        public MenuStatement(ArgParser parser) : base(parser)
        {
            content = parser.String();
            target = parser.Int();
        }
        public override ExecuteMode Mode => ExecuteMode.Next;
        public override void Execute(Executor executor)
        {
            Provider.Visual.Menu(content, target, executor);
            executor.Complete();
        }
        private readonly string content;
        private readonly int target;
    }
}
