using System.Text.RegularExpressions;

namespace Calc.Patterns
{
    public class BracketsPattern: BasePattern
    {
        public BracketsPattern(Scanner scanner) : base(scanner)
        {
        }

        protected override string Pattern => @"\((?<body>.+)\)";

        public override string CreateExpression(string input)
        {
            var matchInput = GetMatchInput(input);
            var scanner = new Scanner(matchInput);
            Scanner.ExpressionTree.Add(scanner.Scan().GetExpression());
            return new Regex(Pattern).Replace(input, Scanner.ExpressionTree.GetLastAddKey(), 1);
        }

        protected override string GetMatchInput(string input)
        {
            return Regex.Match(input, Pattern).Groups["body"].Value;
        }
    }
}