using System;
using System.Collections.Generic;
using System.Linq;

namespace StoryTable
{
    [Statement("IF")]
    public class IfStatement : Statement
    {
        public IfStatement(ArgParser parser) : base(parser)
        {
            var cond = parser.Raw();
            target = parser.String();

            conditions = new();
            char[] signals = new[] { '>', '<', '=' };
            foreach (var info in cond.Split(Separators.PARAMETER))
            {
                string[] infos = info.Split(signals);
                if (infos.Length != 2) parser.Err($"条件 {info} 有误！");
                else conditions.Add(new(infos[0], info[info.IndexOfAny(signals)], infos[1]));
            }
        }
        public override ExecuteMode Mode => ExecuteMode.Next;
        public override void Execute(ExecutorBase executor)
        {
            if (conditions.Count == 0 || conditions.All(Meet))
                if (IntermediateFile.Tags.TryGetValue(target, out Locator locator)) executor.Locate(locator);
                else throw new KeyNotFoundException($"未找到跳转标签 {target}");
            executor.Complete();
        }

        private readonly List<Condition> conditions;
        private readonly string target;
        public readonly struct Condition
        {
            public Condition(string var1, char signal, string var2)
            {
                Var1 = var1;
                Signal = signal;
                Var2 = var2;
            }
            public readonly string Var1, Var2;
            public readonly char Signal;
        }
        private bool Meet(Condition condition)
        {
            if (!float.TryParse(condition.Var1, out float v1))
                v1 = Provider.Data.GetInt(condition.Var1);
            if (!float.TryParse(condition.Var2, out float v2))
                v2 = Provider.Data.GetInt(condition.Var2);
            return condition.Signal switch
            {
                '>' => v1 - v2 > 0,
                '<' => v1 - v2 < 0,
                '=' => MathF.Abs(v1 - v2) < .01f,
                _ => false
            };
        }
    }
}
