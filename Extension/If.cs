namespace StoryParser
{
    [Parse("IF")]
    public class If : Statement
    {
        public If(string[] args) : base(args)
        {
            if (args.Length != 3)
                throw new ArgumentException(string.Format("{0}数组长度有误", args), nameof(args));

            conditions = new();
            char[] signals = new char[] { '>', '<', '=' };
            foreach (var info in args[1].Split(Separators.Parameter))
            {
                string[] infos = info.Split(signals);
                if (infos.Length != 2)
                    throw new ArgumentException(string.Format("{0}条件声明有误", args), nameof(args));
                conditions.Add(new Condition(infos[0], info[info.IndexOfAny(signals)], infos[1]));
            }
        }

        public override void Execute()
        {
            if (conditions.Count == 0 || conditions.All(Meet))
                Executor.Locate(target - 1);
            Executor.Complete();
        }

        private readonly List<Condition> conditions;
        private readonly int target;
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
