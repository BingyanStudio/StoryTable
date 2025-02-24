using System.Collections.Generic;

namespace StoryTable
{
    public class Line
    {
        internal Line(ArgParser parser)
        {
            string mode = parser.String();

            statement = StatementFactory.Create(parser);
            this.mode = statement.Mode;

            if (mode != string.Empty) switch (mode)
                {
                    case "Next": this.mode = ExecuteMode.Next; break;
                    case "Wait": this.mode = ExecuteMode.Wait; break;
                    case "Pause": this.mode = ExecuteMode.Pause; break;
                    default: parser.Err("出现了Next, Wait, Pause之外的执行方式标识！"); break;
                }
        }
        private Line() { }
        internal static Line Empty => new();
        private readonly Statement statement;
        private readonly ExecuteMode mode;
        internal ExecuteMode Execute(ExecutorBase executor)
        {
            statement?.Execute(executor);
            return mode;
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
