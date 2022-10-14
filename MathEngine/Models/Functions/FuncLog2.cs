using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("Log2")]
    public class FuncLog2 : INumericalYield
    {
        public INumericalYield Value { get; }

        public FuncLog2(INumericalYield value)
        {
            Value = value;
        }

        public double Calculate()
        {
            return Math.Log2(Value.Calculate());
        }

        public override string ToString()
        {
            return $"Log2({Value})";
        }
    }
}