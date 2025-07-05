using System.Collections.Generic;

namespace StoryTable
{
    [Statement("MENU")]
    public class MenuStatement : Statement
    {
        public MenuStatement(ArgParser parser) : base(parser)
        {
            content = parser.String();
            target = parser.String();
        }

        public override ExecuteMode Mode => ExecuteMode.Next;

        public override void Execute(ExecutorBase executor)
        {
            if (!IntermediateFile.Tags.TryGetValue(target, out Locator locator))
                throw new KeyNotFoundException($"未找到跳转标签 {target}");
            executor.Provider.Visual.Menu(content, locator, executor);
        }

        private readonly string content;
        private readonly string target;
    }
}
