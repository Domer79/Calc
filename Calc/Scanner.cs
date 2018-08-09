using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Calc.Patterns;

namespace Calc
{
    /// <summary>
    /// Сканер входной строки.
    /// Производит поэтапное сканирование входной строки, согласно загруженным шаблонам. 
    /// Приоритет шаблонов определяется порядком их загрузки в сканер
    /// </summary>
    public class Scanner
    {
        private string _input;
        private readonly ExpressionTree _expressionTree = new ExpressionTree();
        private static readonly List<Type> Patterns = new List<Type>();

        public Scanner(string input) : this(input, new Type[] { })
        {
        }

        public Scanner(string input, Type[] patterns)
        {
            Patterns.AddRange(patterns ?? throw new ArgumentNullException(nameof(patterns)));
            _input = input.Replace(" ", "");
        }

        public static void AddPattern<T>() where T : BasePattern
        {
            AddPattern(typeof(T));
        }

        private static void AddPattern(Type pattern)
        {
            Patterns.Add(pattern ?? throw new ArgumentNullException(nameof(pattern)));
        }

        public static void AddRangePatterns(Type[] patterns)
        {
            Patterns.AddRange(patterns ?? throw new ArgumentNullException(nameof(patterns)));
        }

        public ExpressionTree ExpressionTree => _expressionTree;

        public Scanner Pipe(BasePattern pattern)
        {
            while (pattern.IsMatch(_input))
            {
                _input = pattern.CreateExpression(_input);
            }

            return this;
        }

        public Scanner Scan()
        {
            var patterns = Patterns.ToArray();
            foreach (var pattern in patterns)
            {
                Pipe((BasePattern) Activator.CreateInstance(pattern, this));
            }

            if (!ExpressionTree.ExistKey(_input))
                throw new ArgumentException("Неверный формат входной строки");

            return this;
        }

        public Expression GetExpression()
        {
            return _expressionTree.GetLastExpression();
        }

        public object Compute()
        {
            var lambda = Expression.Lambda<Func<float>>(GetExpression());
            return lambda.Compile()();
        }
    }
}