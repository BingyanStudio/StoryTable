using System.Text.RegularExpressions;
using System.Linq;

namespace StoryTable
{
    [Statement("SAY")]
    public class SayStatement : Statement
    {
        public SayStatement(ArgParser parser) : base(parser)
        {
            character = parser.String();
            sprite = parser.StringOr(string.Empty);
            dialogue = parser.String();
        }

        public override ExecuteMode Mode => ExecuteMode.Pause;

        public override void Execute(ExecutorBase executor)
        {
            var matches = Regex.Matches(dialogue, @"(?<=\{)[^}]*(?=\})").Cast<Match>().ToList();
            string copy = dialogue;
            foreach (var match in matches)
                copy = copy.Replace("{" + match + "}", executor.Provider.Data.GetString(match.ToString()));
            executor.Provider.Visual.Say(character, sprite, copy, executor);
        }

        private readonly string character, sprite, dialogue;
    }
}
