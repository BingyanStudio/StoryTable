using System.Text.RegularExpressions;

namespace StoryParser
{
    [Parse("SAY")]
    public class Say : Statement
    {
        public Say(string[] args) : base(args)
        {
            if (args.Length != 4)
                throw new ArgumentException($"数组长度错误，应为 4, 实为 {args.Length}", nameof(args));

            character = args[1];
            sprite = args[2];
            dialogue = args[3];
        }

        public override void Execute()
        {
            var matches = Regex.Matches(dialogue, @"(?<=\{)[^}]*(?=\})").Cast<Match>().ToList();
            string copy = dialogue;
            foreach (var match in matches)
                copy = copy.Replace("{" + match + "}", Commands.GetValue(match.ToString()).ToString());
            Commands.Say(character, sprite, copy);
        }

        private readonly string? character, sprite;
        private readonly string dialogue;
    }
}
