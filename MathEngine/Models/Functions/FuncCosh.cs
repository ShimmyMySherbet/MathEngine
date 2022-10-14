using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("Cosh")]
    public class FuncCosh : INumericalYield
    {
        public INumericalYield Value { get; }

        public FuncCosh(INumericalYield value)
        {
            Value = value;
        }

        public double Calculate()
        {
            return Math.Cosh(Value.Calculate());
        }

        public override string ToString()
        {
            return $"Cos-1({Value})";
        }
    }
}