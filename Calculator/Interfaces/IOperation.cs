using System;
using System.Collections.Generic;

namespace Calculator.Interfaces
{
    public interface IOperation
    {
        Func<double, double, double> GetOperation(Token token);
        int GetPrecedence(Token token);
        Dictionary<string, Token> GetPatterns();
    }
}