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

        public static Statement Create(ArgParser parser)
        {
            string name = parser.String();
            if(name == string.Empty) throw new ArgumentException($"处理表格第 {IntermediateFile.TableLine} 行时出错: \n指令名称为空！");
            if (statementTypes.TryGetValue(name, out var type))
                    try
                    {
                        return Activator.CreateInstance(type, parser) as Statement;
                    }
                    catch (ArgumentException e)
                    {
                        throw new ArgumentException($"处理表格第 {IntermediateFile.TableLine} 行时出错: \n{e.Message}");
                    }
            throw new KeyNotFoundException($"Statement Type {name} Not Found");
        }
    }
}
