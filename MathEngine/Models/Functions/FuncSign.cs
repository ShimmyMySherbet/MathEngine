using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("Sign")]
    public class FuncSign : INumericalYield
    {
        public INumericalYield Value { get; }

        public FuncSign(INumericalYield value)
        {
            Value = value;
        }

        public double Calculate()
        {
            return Math.Sign(Value.Calculate());
        }

        public override string ToString()
        {
            return $"Sign({Value})";
        }
    }
}