using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("Truncate")]
    public class FuncTruncate : INumericalYield
    {
        public INumericalYield Value { get; }

        public FuncTruncate(INumericalYield value)
        {
            Value = value;
        }

        public double Calculate()
        {
            return Math.Truncate(Value.Calculate());
        }

        public override string ToString()
        {
            return $"Truncate({Value})";
        }
    }
}