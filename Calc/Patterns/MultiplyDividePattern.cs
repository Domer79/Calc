using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Calc.Patterns
{
    public class MultiplyDividePattern : BasePattern
    {
        public MultiplyDividePattern(Scanner scanner) 
            : base(scanner)
        {

        }

        protected override string Pattern => @"(?<left>([\d]+(\.\d+)*|\w{1}\d+))(?<operand>[*\/]{1})(?<right>([\d]+(\.\d+)*|\w{1}\d+))";

        public override string CreateExpression(string input)
        {
            var matchInput = GetMatchInput(input);
            var operand = GetOperand(matchInput);
            var methodInfo = typeof(Expression).GetMethod(operand.ToString(), new[] {typeof(Expression), typeof(Expression)});

            var left = GetLeft(matchInput);
            var right = GetRight(matchInput);
            var expression = (BinaryExpression)methodInfo.Invoke(null, new object[] {left, right });
            Scanner.ExpressionTree.Add(expression);

            return new Regex(Pattern).Replace(input, Scanner.ExpressionTree.GetLastAddKey(), 1);
        }

        private Expression GetLeft(string input)
        {
            var value = Regex.Match(input, @"^([\d]+(\.\d+)*|\w{1}\d+)").Groups[0].Value;
            if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var result))
                return Expression.Constant(result);

            return Scanner.ExpressionTree[value];
        }

        private Expression GetRight(string input)
        {
            var value = Regex.Match(input, @"([\d]+(\.\d+)*|\w{1}\d+)$").Groups[0].Value;
            if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var result))
                return Expression.Constant(result);

            return Scanner.ExpressionTree[value];
        }

        private Operand GetOperand(string input)
        {
            var operand = Regex.Match(input, Pattern).Groups["operand"].Value;
            return operand == "*" ? Operand.Multiply : Operand.Divide;
        }

        enum Operand
        {
            Multiply,
            Divide
        }
    }
}