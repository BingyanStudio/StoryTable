using System.Collections.Generic;

namespace StoryParser
{
    public class Line
    {
        internal Line(string line)
        {
            statements = new();
            if (line[0] == Separators.Comment) return;
            foreach (string statement in line.Split(Separators.Statement))
            {
                var s = StatementFactory.Create(statement);
                if (s != null) statements.Add(s);
            }
        }

        private readonly List<Statement> statements;
        public int Length => statements.Count;
        internal void Execute(Executor executor) => statements.ForEach(s => s.Execute(executor));
    }
    public class File
    {
        private readonly List<Line> lines = new() { new Line(Separators.Comment.ToString()) };
        public int Length => lines.Count;
        internal void AddLine(string line) => lines.Add(new Line(line));
        public Line this[int index] => lines[index];
    }
}
