using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("Abs")]
    public class FuncAbs : INumericalYield
    {
        public INumericalYield Value { get; }

        public FuncAbs(INumericalYield value)
        {
            Value = value;
        }

        public double Calculate()
        {
            return Math.Abs(Value.Calculate());
        }

        public override string ToString()
        {
            return $"Abs({Value})";
        }
    }
}