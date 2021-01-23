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
            patterns.Add(@"", Token.Number);
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
                
            }
            return symbols;
        }
    }
}
