using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class Parser
    {
        public Dictionary<string, Token> patterns;

        public Parser()
        {
            patterns = new Dictionary<string, Token>();
            patterns.Add(@"^-?\d+(\.\d+)?", Token.Number);
            patterns.Add(@"^\(", Token.LeftBracket);
            patterns.Add(@"^\)", Token.RightBracket);
            patterns.Add(@"^\+", Token.Addition);
            patterns.Add(@"^\-", Token.Subtraction);
            patterns.Add(@"^\*", Token.Multiplication);
            patterns.Add(@"^\/", Token.Division);
        }

        public string PrepareExpression(string expression)
        {
            return Regex.Replace(expression, @"\s+", "");
        }

        public List<Symbol> Parse(string expression)
        {
            expression = PrepareExpression(expression);
            List<Symbol> symbols = new List<Symbol>();
            while (expression != "")
            {
                bool patternFound = false;
                foreach (var pattern in patterns)
                {
                    var result = Regex.Match(expression, pattern.Key);
                    if (result.Success)
                    {
                        Symbol symbol = new Symbol { Value = result.Value, Token = pattern.Value };
                        symbols.Add(symbol);
                        expression = expression.Substring(result.Value.Length);
                        patternFound = true;
                        break;
                    }
                }
                if (!patternFound)
                {
                    throw new Exception("Failed to parse input expression! " + expression);
                }
            }
            return symbols;
        }
    }
}
