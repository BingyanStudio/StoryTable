using System.Text.RegularExpressions;

namespace StoryTable
{
    [Statement("SAY")]
    public class Say : Statement
    {
        public Say(string[] args) : base(args)
        {
            character = args[0];
            sprite = args[1];
            dialogue = args[2];

            Mode = ExecuteMode.Pause;
        }
        public override ExecuteMode Mode { get; init; }
        public override void Execute(Executor executor)
        {
            var matches = Regex.Matches(dialogue, @"(?<=\{)[^}]*(?=\})").Cast<Match>().ToList();
            string copy = dialogue;
            foreach (var match in matches)
                copy = copy.Replace("{" + match + "}", Provider.GetValue<string>(match.ToString()));
            Provider.Say(character, sprite, copy, executor);
        }

        private readonly string character, sprite, dialogue;
    }
}
