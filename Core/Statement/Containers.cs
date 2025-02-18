namespace StoryTable
{
    public class Line
    {
        internal Line(ArgParser parser)
        {
            string mode = parser.String();

            statement = StatementFactory.Create(parser);
            this.mode = statement.Mode;

            if (mode != string.Empty) this.mode = mode switch
            {
                "Next" => ExecuteMode.Next,
                "Wait" => ExecuteMode.Wait,
                "Pause" => ExecuteMode.Pause,
                _ => throw new ArgumentException($"处理表格第 {IntermediateFile.TableLine} 行时出错: \n出现了Next, Wait, Pause之外的执行方式标识！")
            };
        }
        private Line() { }
        internal static Line Empty => new();
        private readonly Statement statement;
        private readonly ExecuteMode mode;
        internal bool Execute(Executor executor)
        {
            statement?.Execute(executor);
            switch (mode)
            {
                case ExecuteMode.Pause:
                    executor.Pause = true;
                    return false;
                case ExecuteMode.Wait:
                    return false;
                default:
                    return true;
            }
        }
    }
    public class File
    {
        private readonly List<Line> lines = new() { Line.Empty };
        public int Length => lines.Count;
        internal void AddLine(ArgParser parser) => lines.Add(new(parser));
        public Line this[int index] => lines[index];
    }
}
