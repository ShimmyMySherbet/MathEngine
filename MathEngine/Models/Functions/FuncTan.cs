using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("Tan")]
    public class FuncTan : INumericalYield
    {
        public INumericalYield Value { get; }

        public FuncTan(INumericalYield value)
        {
            Value = value;
        }

        public double Calculate()
        {
            return Math.Tan(Value.Calculate());
        }

        public override string ToString()
        {
            return $"Tan({Value})";
        }
    }
}