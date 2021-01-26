using Calculator;
using System.Collections.Generic;
using Xunit;

namespace CalculatorTests
{
    public class CalculationTest
    {
        Calculation calculation = new Calculation(new Parser(), new Operation());

        [Fact]
        public void GetResultTest()
        {
            string expression = "2 + (3 * 8 - 4)*2";
            var result = calculation.GetResult(expression);
            Assert.Equal(42, result);
        }

        [Fact]
        public void DistributeSymbolsTest()
        {
            Symbol addition = new Symbol() { Token = Token.Addition, Value = "+" };
            Symbol number = new Symbol() { Token = Token.Number, Value = "42" };
            List<Symbol> output = new List<Symbol>();
            Stack<Symbol> operatorStack = new Stack<Symbol>();
            calculation.DistributeSymbols(addition, output, operatorStack);
            calculation.DistributeSymbols(number, output, operatorStack);
            Assert.Equal(number.Value, output[0].Value);
            Assert.Equal(number.Token, output[0].Token);
            Assert.Equal(addition.Value, operatorStack.Peek().Value);
            Assert.Equal(addition.Token, operatorStack.Peek().Token);
        }

        [Fact]
        public void CurrentHasSameOrHigherPrecendenceTest()
        {
            Symbol previousSymbol = new Symbol { Token = Token.Addition };
            Symbol currentSymbol = new Symbol { Token = Token.Division };
            var expected = true;
            var actual = calculation.CurrentHasSameOrHigherPrecendence(previousSymbol, currentSymbol);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ComputeTest()
        {
            double expected = 26.01;
            var symbols = new List<Symbol>() {
                new Symbol() { Token = Token.Number, Value = "26" },
                new Symbol() { Token = Token.Number, Value = "0,01" },
                new Symbol() { Token = Token.Addition, Value = "+" },
            };
            var actual = calculation.Compute(symbols);
            Assert.Equal(expected, actual);
        }
    }
}
