using Calculator.Interfaces;
using System;
using System.Collections.Generic;

namespace Calculator
{
    public class Operation : IOperation
    {
        public Dictionary<Token, Func<double, double, double>> operations;
        public Dictionary<Token, int> precedences;
        public Dictionary<string, Token> patterns;

        public Operation()
        {
            operations = new Dictionary<Token, Func<double, double, double>>();
            operations.Add(Token.Addition, (double x, double y) => x + y);
            operations.Add(Token.Subtraction, (double x, double y) => x - y);
            operations.Add(Token.Multiplication, (double x, double y) => x * y);
            operations.Add(Token.Division, (double x, double y) => x / y);

            precedences = new Dictionary<Token, int>();
            precedences.Add(Token.Multiplication, 3);
            precedences.Add(Token.Division, 3);
            precedences.Add(Token.Addition, 2);
            precedences.Add(Token.Subtraction, 2);
            precedences.Add(Token.LeftBracket, 1);
            precedences.Add(Token.RightBracket, 1);

            patterns = new Dictionary<string, Token>();
            patterns.Add(@"^-?\d+(\,\d+)?", Token.Number);  // Positive or negative real number
            patterns.Add(@"^\(", Token.LeftBracket);
            patterns.Add(@"^\)", Token.RightBracket);
            patterns.Add(@"^\+", Token.Addition);
            patterns.Add(@"^\-", Token.Subtraction);
            patterns.Add(@"^\*", Token.Multiplication);
            patterns.Add(@"^\/", Token.Division);
        }

        public Func<double, double, double> GetOperation(Token token)
        {
            if (!operations.ContainsKey(token))
            {
                throw new Exception("Token not found");
            }
            return operations[token];
        }

        public Dictionary<string, Token> GetPatterns()
        {
            return patterns;
        }

        public int GetPrecedence(Token token)
        {
            if (!precedences.ContainsKey(token))
            {
                throw new Exception("Token not found");
            }
            return precedences[token];
        }
    }
}
