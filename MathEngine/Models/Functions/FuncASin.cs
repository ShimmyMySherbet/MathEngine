using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("ASin")]
    public class FuncASin : INumericalYield
    {
        public INumericalYield Value { get; }

        public FuncASin(INumericalYield value)
        {
            Value = value;
        }

        public double Calculate()
        {
            return Math.Asin(Value.Calculate()) * 180 / Math.PI;
        }

        public override string ToString()
        {
            return $"Sin-1({Value})";
        }
    }
}