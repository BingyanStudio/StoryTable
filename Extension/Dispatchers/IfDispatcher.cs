using StoryParser.Core.Statement;
using StoryParser.Core.Util;
using StoryParser.Extension.Statements;
using StoryParser.Extension.Util;

namespace StoryParser.Extension.Dispatchers
{
    public class IfDispatcher : IDispatcher
    {
        private char[] signals = new char[] { '>', '<', '=' };
        private List<Condition>? conditions;
        private string[]? infos;
        public IStatement Dispatch(string[] parameters)
        {
            if (parameters.Length != 3)
                throw new ArgumentException(string.Format("{0}数组长度有误", parameters), nameof(parameters));
            conditions = new();
            foreach (var info in parameters[1].Split(Separators.Parameter))
            {
                infos = info.Split(signals);
                conditions.Add(infos.Length switch
                {
                    1 => new Condition(info),
                    2 => new Condition(infos[0], info[info.IndexOfAny(signals)], int.Parse(infos[1])),
                    _ => throw new ArgumentException(string.Format("{0}条件声明有误", parameters), nameof(parameters)),
                });
            }
            return new IfStatement(conditions, parameters[2]);
        }
    }
}
