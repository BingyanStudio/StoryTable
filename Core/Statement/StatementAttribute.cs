namespace StoryTable
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class StatementAttribute : Attribute
    {
        public StatementAttribute(string name)
        {
            Name = name;
        }

        public readonly string Name;
    }
}
