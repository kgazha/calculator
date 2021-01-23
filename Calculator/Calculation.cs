using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public class Calculation
    {

        public Dictionary<Token, Func<double, double, double>> operations = new Dictionary<Token, Func<double, double, double>>();

        public Calculation()
        {
            operations.Add(Token.Addition, (double x, double y) => x + y);
            operations.Add(Token.Subtraction, (double x, double y) => x - y);
            operations.Add(Token.Multiplication, (double x, double y) => x * y);
            operations.Add(Token.Division, (double x, double y) => x / y);
        }
        
    }
}
