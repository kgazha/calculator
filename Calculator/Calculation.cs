using Calculator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator
{
    public class Calculation
    {
        IOperation operation;
        IParser parser;

        public Calculation(IParser parser, IOperation operation)
        {
            this.parser = parser;
            this.operation = operation;
        }

        public double GetResult(string expression)
        {
            List<Symbol> symbols = parser.Parse(expression, operation);

            var output = new List<Symbol>();
            var operatorStack = new Stack<Symbol>();
            foreach (var symbol in symbols)
            {
                DistributeSymbols(symbol, output, operatorStack);  // другой класс. связывает как контроллер...
            }
            while (operatorStack.Any())
            {
                output.Add(operatorStack.Pop());
            }
            return Compute(output);
        }

        public double Compute(List<Symbol> symbols)
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
            if (numbers.Count >= 2)
            {
                var y = Convert.ToDouble(numbers.Pop().Value);
                var x = Convert.ToDouble(numbers.Pop().Value);
                var result = operation.GetOperation(symbol.Token)(x, y);
                numbers.Push(new Symbol() { Token = Token.Number, Value = result.ToString() });
            }
            else if (symbol.Token == Token.Subtraction & numbers.Any())
            {
                var y = Convert.ToDouble(numbers.Pop().Value);
                numbers.Push(new Symbol() { 
                    Token = Token.Number, 
                    Value = (-y).ToString()
                });
            }
        }

        public bool CurrentHasSameOrHigherPrecendence(Symbol previousSymbol, Symbol currentSymbol)
        {
            return operation.GetPrecedence(previousSymbol.Token) < operation.GetPrecedence(currentSymbol.Token);
        }

        public void DistributeSymbols(Symbol symbol, List<Symbol> output, Stack<Symbol> operatorStack)
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
            else if (CurrentHasSameOrHigherPrecendence(operatorStack.Peek(), symbol))
            {
                operatorStack.Push(symbol);
            }
            else
            {
                while (!CurrentHasSameOrHigherPrecendence(operatorStack.Peek(), symbol))
                {
                    output.Add(operatorStack.Pop());
                    if (!operatorStack.Any())
                        break;
                }
                operatorStack.Push(symbol);
            }
        }
    }
}
