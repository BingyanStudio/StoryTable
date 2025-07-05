using System;
using System.Collections.Generic;

namespace StoryTable
{
    [Statement("FUNC")]
    public class FuncStatement : Statement
    {
        public static Dictionary<string, (ExecuteMode, Action<ExecutorBase, string[]>)> Dict { get; } = new();

        private readonly string name;
        private readonly string[] args;

        public FuncStatement(ArgParser parser) : base(parser)
        {
            name = parser.String();
            args = parser.currentArgs[4..]; // 跳转标签,执行模式,语句名称,方法名称,参数列表
        }

        public override ExecuteMode Mode => Dict[name].Item1;

        public override void Execute(ExecutorBase executor)
        {
            Dict[name].Item2(executor, args);
        }
    }
}
