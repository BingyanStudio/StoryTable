using System;

namespace StoryTable
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class StatementAttribute : Attribute
    {
        public readonly string Name;

        public StatementAttribute(string name)
        {
            Name = name;
        }
    }

    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
    public class AliasAttribute : Attribute
    {
        public readonly string[] Alias;

        public AliasAttribute(params string[] alias)
        {
            Alias = alias;
        }
    }
}
