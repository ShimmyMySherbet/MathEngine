using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("Cbrt")]
    public class FuncCbrt : INumericalYield
    {
        public INumericalYield Value { get; }

        public FuncCbrt(INumericalYield value)
        {
            Value = value;
        }

        public double Calculate()
        {
            return Math.Cbrt(Value.Calculate());
        }

        public override string ToString()
        {
            return $"Cbrt({Value})";
        }
    }
}