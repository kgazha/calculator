using Calculator;
using System;
using System.Collections.Generic;
using Xunit;

namespace CalculatorTests
{
    public class CalculationTest
    {
        [Fact]
        public void ComputeTest()
        {
            string expression = "2 + (3 * 8 - 4)*2";
            Calculation calculation = new Calculation();
            var result = calculation.GetResult(expression);
            Assert.Equal(42, result);
        }

        [Fact]
        public void SubtractionTest()
        {
            string expression = "3-3-3";
            Calculation calculation = new Calculation();
            var result = calculation.GetResult(expression);
            Assert.Equal(-3, result);
        }

        [Fact]
        public void RealNumberTest()
        {
            string expression = "77.7-42";
            Calculation calculation = new Calculation();
            var result = calculation.GetResult(expression);
            Assert.Equal(35.7, result);
        }
    }
}
