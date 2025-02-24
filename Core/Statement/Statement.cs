using System.Collections.Generic;

namespace StoryTable
{
    public enum ExecuteMode
    {
        Next, // 立刻执行下一句
        Wait, // 等待当前语句执行结束
        Pause, // 等待用户输入
    }
    public abstract class Statement
    {
        public Statement(ArgParser parser) { }
        public abstract ExecuteMode Mode { get; }
        public abstract void Execute(ExecutorBase executor);
    }

    [Statement("END")]
    public class EndStatement : Statement
    {
        private readonly string result;

        public EndStatement(ArgParser parser) : base(parser)
        {
            // if (args.Length > 0) result = args[0];
            // else result = string.Empty;
            result = parser.StringOr(string.Empty);
        }
        public override ExecuteMode Mode => ExecuteMode.Pause;
        public override void Execute(ExecutorBase executor)
        {
            executor.EndWith(result);
            executor.Complete();
        }
    }

    [Statement("FILE")]
    public class FileStatement : Statement
    {
        private readonly string path;
        public FileStatement(ArgParser parser) : base(parser)
        {
            path = parser.String();
        }
        public override ExecuteMode Mode => ExecuteMode.Next;
        public override void Execute(ExecutorBase executor)
        {
            executor.Locate(path);
            executor.Complete();
        }
    }

    [Statement("TAG")]
    public class TagStatement : Statement
    {
        private readonly string target;
        public TagStatement(ArgParser parser) : base(parser)
        {
            target = parser.String();
        }
        public override ExecuteMode Mode => ExecuteMode.Next;
        public override void Execute(ExecutorBase executor)
        {
            if (IntermediateFile.Tags.TryGetValue(target, out var locator)) executor.Locate(locator);
            else throw new KeyNotFoundException($"未找到跳转标签 {target}");
            executor.Complete();
        }
    }
}
