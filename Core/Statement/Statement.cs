namespace StoryParser
{
    public abstract class Statement
    {
        public Statement(string[] args)
        {
            Parse(args);
        }

        protected abstract void Parse(string[] args);
        public abstract void Execute();
    }
}
