using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Interfaces
{
    public interface IParser
    {
        List<Symbol> Parse(string expression, IOperation operation);
    }
}
