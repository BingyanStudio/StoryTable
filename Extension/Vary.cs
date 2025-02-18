namespace StoryTable
{
    [Statement("VARY")]
    public class Vary : Statement
    {
        public Vary(ArgParser parser) : base(parser)
        {
            // if (args[0] != "ADD" || args[0] != "SUB" || args[0] != "MUL" || args[0] != "DIV")
            //     throw new ArgumentException(string.Format("{0}操作声明有误", args[0]));

            // operation = args[0];
            // key = args[1];
            // var1 = args[2];
            // var2 = args[3];

            operation = parser.Enum<Operation>();
            key = parser.String();
            var1 = parser.String();
            var2 = parser.String();

            Mode = ExecuteMode.Next;
        }

        public override ExecuteMode Mode { get; init; }
        public override void Execute(Executor executor)
        {
            float v1 = float.TryParse(var1, out float f1) ? f1 : Provider.Data.GetValue<float>(var1);
            float v2 = float.TryParse(var1, out float f2) ? f2 : Provider.Data.GetValue<float>(var2);
            Provider.Data.SetValue(key, (int)(.5f + operation switch
            {
                Operation.Add => v1 + v2,
                Operation.Sub => v1 - v2,
                Operation.Mul => v1 * v2,
                Operation.Div => v1 / v2,
                _ => 0
            }));
            executor.Complete();
        }

        private readonly Operation operation;
        private readonly string key, var1, var2;

        private enum Operation
        {
            Add, Sub, Mul, Div
        }
    }
}
