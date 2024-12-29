namespace StoryParser
{
    public abstract class Statement
    {
        public Statement(string[] args) { }

        public abstract void Execute();
    }
}
