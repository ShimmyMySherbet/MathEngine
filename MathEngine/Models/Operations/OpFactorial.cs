using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Operations
{
    [Operation(symbol: '!', order: 0)]
    public class OpFactorial : INumericalYield
    {
        public INumericalYield Value { get; }

        public OpFactorial([Left] INumericalYield value)
        {
            Value = value;
        }

        public double Calculate()
        {
            var floor = Math.Floor(Value.Calculate());
            var total = floor;

            for (double i = floor - 1; i > 0; i--)
            {
                total *= i;
            }

            return total;
        }

        public override string ToString()
        {
            return $"Factorial({Value})";
        }
    }
}