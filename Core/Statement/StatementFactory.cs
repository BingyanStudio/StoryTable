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
            if (tokens.Length == 0) throw new ArgumentException("Invalid Statement");
            if (statementTypes.TryGetValue(tokens[0], out var type))
                if (tokens.Length > 1) return Activator.CreateInstance(type, tokens[1..]) as Statement;
                else return Activator.CreateInstance(type, Array.Empty<string>()) as Statement;
            throw new KeyNotFoundException($"Statement Type {tokens[0]} Not Found");
        }
    }
}
