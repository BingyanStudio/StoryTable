namespace StoryTable
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ParseAttribute : Attribute
    {
        public ParseAttribute(string name)
        {
            Name = name;
        }

        public readonly string Name;
    }
}
