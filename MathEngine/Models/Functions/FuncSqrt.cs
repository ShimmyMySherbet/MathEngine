using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("SQRT")]
    public class FuncSqrt : INumericalYield
    {
        public INumericalYield Value { get; }

        public FuncSqrt(INumericalYield value)
        {
            Value = value;
        }

        public double Calculate()
        {
            return Math.Sqrt(Value.Calculate());
        }

        public override string ToString()
        {
            return $"Sqrt({Value})";
        }
    }
}