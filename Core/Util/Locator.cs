namespace StoryTable
{
    public readonly struct Locator
    {
        public Locator(string name, int index)
        {
            FileName = name;
            LineIndex = index;
        }
        public string FileName { get; init; }
        public int LineIndex { get; init; }
    }
}
