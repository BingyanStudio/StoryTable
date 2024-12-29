namespace StoryParser
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
                    if (item is ParseAttribute parse)
                    {
                        statementTypes.Add(parse.Name, type);
                        break;
                    }
                }
            }
        }

        public static Statement? Create(string line)
        {
            var tokens = line.Split(Separators.Parameter);
            if (tokens.Length < 1) throw new ArgumentException("No Statement Name Provided");
            if (statementTypes.TryGetValue(tokens[0], out var type))
                return Activator.CreateInstance(type, tokens) as Statement;
            throw new KeyNotFoundException($"Statement Type {tokens[0]} Not Found");
        }
    }
}
