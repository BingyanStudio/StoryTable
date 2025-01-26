namespace StoryParser
{
    [Parse("VARY")]
    public class Vary : Statement
    {
        public Vary(string[] args) : base(args)
        {
            if (args[1] != "ADD" || args[1] != "SUB" || args[1] != "MUL" || args[1] != "DIV")
                throw new ArgumentException(string.Format("{0}操作声明有误", args[1]));

            operation = args[0];
            key = args[1];
            var1 = args[2];
            var2 = args[3];

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
