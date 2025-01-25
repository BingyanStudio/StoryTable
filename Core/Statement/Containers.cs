namespace StoryParser
{
    public class Line
    {
        internal Line(string line)
        {
            if (line[0] == Separators.COMMENT) return;
            statement = StatementFactory.Create(line);
        }
        private readonly Statement? statement;
        internal void Execute(Executor executor) => statement?.Execute(executor);
    }
    public class File
    {
        private readonly List<Line> lines = new() { new Line(Separators.COMMENT.ToString()) };
        public int Length => lines.Count;
        internal void AddLine(string line) => lines.Add(new Line(line));
        public Line this[int index] => lines[index];
    }
}
