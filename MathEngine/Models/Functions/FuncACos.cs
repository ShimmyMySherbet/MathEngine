using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("ACos")]
    public class FuncACos : INumericalYield
    {
        public INumericalYield Value { get; }

        public FuncACos(INumericalYield value)
        {
            Value = value;
        }

        public double Calculate()
        {
            return Math.Acos(Value.Calculate()) * 180 / Math.PI;
        }

        public override string ToString()
        {
            return $"Cos-1({Value})";
        }
    }
}