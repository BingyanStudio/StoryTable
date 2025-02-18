namespace StoryTable
{
    public static class StatementFactory
    {
        private static readonly Dictionary<string, Type> statementTypes = new();

        static StatementFactory()
        {
            foreach (var type in AppDomain.CurrentDomain.GetAssemblies()
                                    .SelectMany(i => i.GetTypes())
                                    .Where(i => i.IsSubclassOf(typeof(Statement)) && !i.IsAbstract))
            {
                foreach (var item in type.GetCustomAttributes(false))
                {
                    if (item is StatementAttribute parse)
                    {
                        statementTypes.Add(parse.Name, type);
                        break;
                    }
                }
            }
        }

        public static Statement? Create(string line)
        {
            var tokens = line.Split(Separators.STATEMENT);
            if (tokens.Length == 0) throw new ArgumentException("Invalid Statement");
            if (statementTypes.TryGetValue(tokens[0], out var type))
                    try
                    {
                        return Activator.CreateInstance(type, new ArgParser(tokens[1..])) as Statement;
                    }
                    catch (ArgumentException e)
                    {
                        throw new ArgumentException($"处理表格第 {line} 行时出错: \n{e.Message}");
                    }
            throw new KeyNotFoundException($"Statement Type {tokens[0]} Not Found");
        }
    }
}
