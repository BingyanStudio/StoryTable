namespace StoryTable
{
    [Statement("VARY")]
    public class VaryStatement : Statement
    {
        public VaryStatement(ArgParser parser) : base(parser)
        {
            operation = parser.Enum<Operation>();
            key = parser.String();
            var1 = parser.String();
            var2 = parser.String();
        }

        public override ExecuteMode Mode => ExecuteMode.Next;

        public override void Execute(ExecutorBase executor)
        {
            float v1 = float.TryParse(var1, out float f1) ? f1 : executor.Provider.Data.GetInt(var1);
            float v2 = float.TryParse(var2, out float f2) ? f2 : executor.Provider.Data.GetInt(var2);
            executor.Provider.Data.SetInt(key, (int)(.5f + operation switch
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
