using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator
{
    public class Calculation
    {
        public Dictionary<Token, Func<double, double, double>> operations;
        public Dictionary<Token, int> precedences;


        public Calculation()
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
        }

        public double Compute(string expression)
        {
            var result = 0.0;
            var output = new List<Symbol>();
            var operatorStack = new Stack<Symbol>();

            Parser parser = new Parser();


            List<Symbol> symbols = parser.Parse(expression);

            // TODO: validation

            foreach (var symbol in symbols)
            {
                DistributeSymbols(symbol, output, operatorStack);
            }


            return result;
        }

        private bool CurrentHasHigherPrecendence(Symbol previousSymbol, Symbol currentSymbol)
        {
            if (!precedences.ContainsKey(previousSymbol.Token) |
                !precedences.ContainsKey(currentSymbol.Token))
            {
                throw new Exception("Token not found");
            }
            return precedences[previousSymbol.Token] <= precedences[currentSymbol.Token];
        }

        private void DistributeSymbols(Symbol symbol, List<Symbol> output, Stack<Symbol> operatorStack)
        {
            var token = symbol.Token;
            if (token == Token.Number)
            {
                output.Add(symbol);
            }
            else if (token == Token.LeftBracket)
            {
                operatorStack.Push(symbol);
            }
            else if (token == Token.RightBracket)
            {
                while (operatorStack.Peek().Token != Token.LeftBracket)
                {
                    output.Add(operatorStack.Pop());
                }
            }
            else if (!operatorStack.Any() || operatorStack.Peek().Token == Token.LeftBracket)
            {
                operatorStack.Push(symbol);
            }
            else if (CurrentHasHigherPrecendence(operatorStack.Peek(), symbol))
            {
                operatorStack.Push(symbol);
            }
            else
            {
                while (!CurrentHasHigherPrecendence(operatorStack.Peek(), symbol) 
                    & operatorStack.Count != 0)
                {
                    output.Add(operatorStack.Pop());
                }
                operatorStack.Push(symbol);
            }
        }
    }
}
