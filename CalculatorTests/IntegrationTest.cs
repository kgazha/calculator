using Calculator;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CalculatorTests
{
    public class IntegrationTest
    {
        [Fact]
        public void SubtractionTest()
        {
            string expression = "3-3-3";
            Calculation calculation = new Calculation(new Parser(), new Operation());
            var result = calculation.GetResult(expression);
            Assert.Equal(-3, result);
        }

        [Fact]
        public void RealNumberTest()
        {
            string expression = "77.7-42";
            Calculation calculation = new Calculation(new Parser(), new Operation());
            var result = calculation.GetResult(expression);
            Assert.Equal(35.7, result);
        }

        [Fact]
        public void UnarMinusTest()
        {
            string expression = "-(5 + 4)";
            Calculation calculation = new Calculation(new Parser(), new Operation());
            var result = calculation.GetResult(expression);
            Assert.Equal(-9, result);
        }
    }
}
