namespace StoryParser
{
    [Parse("VARY")]
    public class Vary : Statement
    {
        public Vary(string[] args) : base(args)
        {
            if (args.Length != 5)
                throw new ArgumentException(string.Format("{0}数组长度有误", args), nameof(args));
            if (args[2] != "ADD" || args[2] != "SUB" || args[2] != "MUL" || args[2] != "DIV")
                throw new ArgumentException(string.Format("{0}操作声明有误", args[2]));

            operation = args[1];
            key = args[2];
            var1 = args[3];
            var2 = args[4];

            Mode = ExecuteMode.Next;
        }
        public override ExecuteMode Mode { get; init; }
        public override void Execute(Executor executor)
        {
            float v1 = float.TryParse(var1, out float f1) ? f1 : Provider.GetValue<float>(var1);
            float v2 = float.TryParse(var1, out float f2) ? f2 : Provider.GetValue<float>(var2);
            Provider.SetValue(key, (int)(.5f + operation switch
            {
                "ADD" => v1 + v2,
                "SUB" => v1 - v2,
                "MUL" => v1 * v2,
                "DIV" => v1 / v2,
                _ => 0
            }));
            executor.Complete();
        }

        private readonly string operation, key, var1, var2;
    }
}
