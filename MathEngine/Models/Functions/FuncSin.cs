using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("Sin")]
    public class FuncSin : INumericalYield
    {
        public INumericalYield Value { get; }

        public FuncSin(INumericalYield value)
        {
            Value = value;
        }

        public double Calculate()
        {
            return Math.Sin(Value.Calculate());
        }

        public override string ToString()
        {
            return $"Sin({Value})";
        }
    }
}