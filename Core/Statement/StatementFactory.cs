using System;
using System.Linq;
using System.Collections.Generic;

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
                        statementTypes.Add(parse.Name.ToLower(), type);
                        break;
                    }
                }
            }
        }

        public static Statement Create(ArgParser parser)
        {
            string name = parser.String();
            if (name == string.Empty)
            {
                parser.Err("语句名称为空！");
                return null;
            }
            if (statementTypes.TryGetValue(name.ToLower(), out var type)) return Activator.CreateInstance(type, parser) as Statement;
            parser.Err($"找不到{name}语句！");
            return null;
        }
    }
}
