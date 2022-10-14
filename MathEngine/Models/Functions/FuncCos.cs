using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("Cos")]
    public class FuncCos : INumericalYield
    {
        public INumericalYield Value { get; }

        public FuncCos(INumericalYield value)
        {
            Value = value;
        }

        public double Calculate()
        {
            return Math.Cos(Value.Calculate());
        }

        public override string ToString()
        {
            return $"Cos({Value})";
        }
    }
}