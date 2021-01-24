using System;
using Calculator;

namespace ConsoleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your expression for calculation:");
            var expression = Console.ReadLine();
            Calculation calculation = new Calculation();
            try
            {
                var result = calculation.GetResult(expression);
                Console.WriteLine("Resutlt: " + result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }
    }
}
