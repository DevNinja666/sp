// ======================= CalcLibrary =======================
// File: CalculatorService.cs
using System;

namespace CalcLibrary
{
    public class CalculatorService : MarshalByRefObject
    {
        public double Calculate(string expression)
        {
            expression = expression.Replace(" ", "");

            if (expression.Contains("+"))
            {
                var p = expression.Split('+');
                return double.Parse(p[0]) + double.Parse(p[1]);
            }
            if (expression.Contains("-"))
            {
                var p = expression.Split('-');
                return double.Parse(p[0]) - double.Parse(p[1]);
            }
            if (expression.Contains("*"))
            {
                var p = expression.Split('*');
                return double.Parse(p[0]) * double.Parse(p[1]);
            }
            if (expression.Contains("/"))
            {
                var p = expression.Split('/');
                return double.Parse(p[0]) / double.Parse(p[1]);
            }

            throw new Exception("Invalid expression");
        }
    }
}
