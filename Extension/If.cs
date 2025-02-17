namespace StoryTable
{
    [Parse("IF")]
    public class If : Statement
    {
        public If(string[] args) : base(args)
        {
            conditions = new();
            char[] signals = new[] { '>', '<', '=' };
            foreach (var info in args[0].Split(Separators.PARAMETER))
            {
                string[] infos = info.Split(signals);
                if (infos.Length != 2)
                    throw new ArgumentException(string.Format("{0}条件声明有误", args), nameof(args));
                conditions.Add(new(infos[0], info[info.IndexOfAny(signals)], infos[1]));
            }
            target = args[1];

            Mode = ExecuteMode.Next;
        }
        public override ExecuteMode Mode { get; init; }
        public override void Execute(Executor executor)
        {
            if (conditions.Count == 0 || conditions.All(Meet))
                if (IntermediateFile.Tags.TryGetValue(target, out Locator locator)) executor.Locate(locator);
                else throw new KeyNotFoundException($"Tag {target} doesn't exist!");
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
                v1 = Provider.GetValue<float>(condition.Var1);
            if (!float.TryParse(condition.Var2, out float v2))
                v2 = Provider.GetValue<float>(condition.Var2);
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
