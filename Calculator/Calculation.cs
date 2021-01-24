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
            precedences.Add(Token.LeftBracket, 1);
            precedences.Add(Token.RightBracket, 1);
        }

        public double Run(string expression)
        {
            var output = new List<Symbol>();
            var operatorStack = new Stack<Symbol>();

            Parser parser = new Parser();


            List<Symbol> symbols = parser.Parse(expression);

            // TODO: validation

            foreach (var symbol in symbols)
            {
                DistributeSymbols(symbol, output, operatorStack);
            }
            while (operatorStack.Any())
            {
                output.Add(operatorStack.Pop());
            }
            return Compute(output);
        }

        private double Compute(List<Symbol> symbols)
        {
            var result = symbols.Aggregate(new Stack<Symbol>(), (numbers, symbol) =>
            {
                UpdateResultStack(numbers, symbol);
                return numbers;
            });
            return Convert.ToDouble(result.Pop().Value);
        }

        private void UpdateResultStack(Stack<Symbol> numbers, Symbol symbol)
        {
            if (symbol.Token == Token.Number)
            {
                numbers.Push(symbol);
                return;
            }
            var y = Convert.ToDouble(numbers.Pop().Value);
            var x = Convert.ToDouble(numbers.Pop().Value);
            var result = operations[symbol.Token](x, y);
            numbers.Push(new Symbol() { Token = Token.Number, Value = result.ToString() });
        }

        private bool CurrentHasHigherPrecendence(Symbol previousSymbol, Symbol currentSymbol)
        {
            if (!precedences.ContainsKey(previousSymbol.Token) |
                !precedences.ContainsKey(currentSymbol.Token))
            {
                throw new Exception("Token not found");
            }
            return precedences[previousSymbol.Token] < precedences[currentSymbol.Token];
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
                operatorStack.Pop();
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
                    & operatorStack.Any())
                {
                    output.Add(operatorStack.Pop());
                }
                operatorStack.Push(symbol);
            }
        }
    }
}
