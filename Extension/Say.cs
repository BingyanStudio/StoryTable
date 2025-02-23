using System.Text.RegularExpressions;
using System.Linq;

namespace StoryTable
{
    [Statement("SAY")]
    public class Say : Statement
    {
        public Say(ArgParser parser) : base(parser)
        {
            character = parser.String();
            sprite = parser.String();
            dialogue = parser.String();
        }
        public override ExecuteMode Mode => ExecuteMode.Pause;
        public override void Execute(Executor executor)
        {
            var matches = Regex.Matches(dialogue, @"(?<=\{)[^}]*(?=\})").Cast<Match>().ToList();
            string copy = dialogue;
            foreach (var match in matches)
                copy = copy.Replace("{" + match + "}", Provider.Data.GetString(match.ToString()));
            Provider.Visual.Say(character, sprite, copy, executor);
        }

        private readonly string character, sprite, dialogue;
    }
}
