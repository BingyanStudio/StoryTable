namespace StoryParser
{
    public class Line
    {
        internal Line(string line)
        {
            statement = StatementFactory.Create(line);
            if (statement != null) mode = statement.Mode;
        }
        private Line() { }
        internal static Line Empty => new();
        private readonly Statement? statement;
        private readonly ExecuteMode mode;
        internal bool Execute(Executor executor)
        {
            statement?.Execute(executor);
            switch (mode)
            {
                case ExecuteMode.Pause:
                    executor.Pause = true;
                    return false;
                case ExecuteMode.Lock:
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
        internal void AddLine(string line) => lines.Add(new Line(line));
        public Line this[int index] => lines[index];
    }
}
