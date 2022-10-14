using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("Ceiling")]
    public class FuncCeiling : INumericalYield
    {
        public INumericalYield Value { get; }

        public FuncCeiling(INumericalYield value)
        {
            Value = value;
        }

        public double Calculate()
        {
            return Math.Ceiling(Value.Calculate());
        }

        public override string ToString()
        {
            return $"Ceiling({Value})";
        }
    }
}