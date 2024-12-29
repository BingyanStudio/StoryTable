using System.Collections.Generic;

namespace StoryParser
{
    public class Line
    {
        internal Line(string line)
        {
            statements = new();
            if (line[0] == Separators.Comment) return;
            foreach (string statement in line.Split(Separators.Line))
            {
                var s = StatementFactory.Create(statement);
                if (s != null) statements.Add(s);
            }
        }

        private List<Statement> statements;
        public int Length => statements.Count;
        internal void Execute() => statements.ForEach(s => s.Execute());
    }
    public class File
    {
        private List<Line> lines = new() { new Line(Separators.Comment.ToString()) };
        public int Length => lines.Count;
        internal void AddLine(string line) => lines.Add(new Line(line));
        public Line this[int index] => lines[index];
    }
}
