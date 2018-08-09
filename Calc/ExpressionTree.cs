using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Calc
{
    /// <summary>
    /// Дерево выражений. Хранит выражения в словаре и выдает их по ключу, который подставляется во входную строку формулы сохраненного выражения
    /// </summary>
    public class ExpressionTree
    {
        private readonly List<char> _alphabet = new List<char>();
        private readonly Dictionary<string, Expression> _expressionDictionary = new Dictionary<string, Expression>();
        private string _lastAddKey;

        public ExpressionTree()
        {
            for (var ch = 'a'; ch <= 'z'; ch++)
            {
                _alphabet.Add(ch);
            }
        }

        public Expression this[string key] => _expressionDictionary[key];

        public void Add(Expression expression)
        {
            var key = GetNextKey();
            _expressionDictionary.Add(key, expression);
            _lastAddKey = key;
        }

        private string GetNextKey()
        {
            var expressionCount = _expressionDictionary.Count;
            var number = expressionCount / 26;
            var symbol = _alphabet[expressionCount % 26];
            var key = new StringBuilder().Append(symbol).Append(number).ToString();
            return key;
        }

        public string GetLastAddKey()
        {
            return _lastAddKey;
        }

        public Expression GetLastExpression()
        {
            return _expressionDictionary.Last().Value;
        }

        public bool ExistKey(string key)
        {
            return _expressionDictionary.Keys.Contains(key);
        }
    }
}