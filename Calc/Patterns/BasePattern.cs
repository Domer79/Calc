using System;
using System.Text.RegularExpressions;

namespace Calc.Patterns
{
    public abstract class BasePattern
    {
        protected Scanner Scanner { get; }

        protected BasePattern(Scanner scanner)
        {
            Scanner = scanner ?? throw new ArgumentNullException(nameof(scanner));
        }

        protected abstract string Pattern { get; }

        public abstract string CreateExpression(string input);

        public virtual bool IsMatch(string input)
        {
            return Regex.IsMatch(input, Pattern);
        }

        protected virtual string GetMatchInput(string input)
        {
            var matchInput = Regex.Match(input, Pattern).Value;
            return matchInput;
        }
    }
}