using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("Log10")]
    public class FuncLog10 : INumericalYield
    {
        public INumericalYield Value { get; }

        public FuncLog10(INumericalYield value)
        {
            Value = value;
        }

        public double Calculate()
        {
            return Math.Log10(Value.Calculate());
        }

        public override string ToString()
        {
            return $"Log10({Value})";
        }
    }
}