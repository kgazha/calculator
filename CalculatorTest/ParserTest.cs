using Calculator;
using System;
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
            var expectedResult = "3+4*4-(-3.4)";
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
