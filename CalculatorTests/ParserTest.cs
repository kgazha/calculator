using Calculator;
using Calculator.Interfaces;
using System;
using System.Collections.Generic;
using Xunit;

namespace CalculatorTests
{
    public class ParserTest
    {
        Parser parser = new Parser();

        [Fact]
        public void PrepareExpressionTest()
        {
            string expression = "3 +  4*4-( -3.4)";
            var actualResult = parser.PrepareExpression(expression);
            var expectedResult = "3+4*4-(-3,4)";
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void SubstractionPositiveNumberTest()
        {
            string expression = "3-4";
            var actualResult = parser.Parse(expression, new Operation());
            var expectedResult = new List<Symbol>() {
                new Symbol() { Token=Token.Number, Value="3" },
                new Symbol() { Token=Token.Subtraction, Value="-" },
                new Symbol() { Token=Token.Number, Value="4" },
            };
            for (int i = 0; i < actualResult.Count; i++)
            {
                Assert.Equal(expectedResult[i].Token, actualResult[i].Token);
                Assert.Equal(expectedResult[i].Value, actualResult[i].Value);
            }
        }

        [Fact]
        public void SubstractionNegativeNumberTest()
        {
            string expression = "3--4";
            var actualResult = parser.Parse(expression, new Operation());
            var expectedResult = new List<Symbol>() {
                new Symbol() { Token=Token.Number, Value="3" },
                new Symbol() { Token=Token.Subtraction, Value="-" },
                new Symbol() { Token=Token.Number, Value="-4" },
            };
            for (int i = 0; i < actualResult.Count; i++)
            {
                Assert.Equal(expectedResult[i].Token, actualResult[i].Token);
                Assert.Equal(expectedResult[i].Value, actualResult[i].Value);
            }
        }

        [Fact]
        public void PraseTest()
        {
            string expression = "(34,5 + -26,5)*-2";
            var actualResult = parser.Parse(expression, new Operation());
            var expectedResult = new List<Symbol>() {
                new Symbol() { Token=Token.LeftBracket, Value="(" },
                new Symbol() { Token=Token.Number, Value="34,5" },
                new Symbol() { Token=Token.Addition, Value="+" },
                new Symbol() { Token=Token.Number, Value="-26,5" },
                new Symbol() { Token=Token.RightBracket, Value=")" },
                new Symbol() { Token=Token.Multiplication, Value="*" },
                new Symbol() { Token=Token.Number, Value="-2" },
            };
            for (int i = 0; i < actualResult.Count; i++)
            {
                Assert.Equal(expectedResult[i].Token, actualResult[i].Token);
                Assert.Equal(expectedResult[i].Value, actualResult[i].Value);
            }
        }
    }
}
