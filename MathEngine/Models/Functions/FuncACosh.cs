using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("ACosh")]
    public class FuncACosh : INumericalYield
    {
        public INumericalYield Value { get; }

        public FuncACosh(INumericalYield value)
        {
            Value = value;
        }

        public double Calculate()
        {
            return Math.Acosh(Value.Calculate());
        }

        public override string ToString()
        {
            return $"ACosh({Value})";
        }
    }
}