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
        }

        public override void Execute()
        {
            float v1 = float.TryParse(var1, out float f1) ? f1 : float.Parse(Commands.GetValue(var1).ToString()!);
            float v2 = float.TryParse(var1, out float f2) ? f2 : float.Parse(Commands.GetValue(var2).ToString()!);
            Commands.SetValue(key, (int)(.5f + operation switch
            {
                "ADD" => v1 + v2,
                "SUB" => v1 - v2,
                "MUL" => v1 * v2,
                "DIV" => v1 / v2,
                _ => 0
            }));
            Executor.Complete();
        }

        private readonly string operation, key, var1, var2;
    }
}
