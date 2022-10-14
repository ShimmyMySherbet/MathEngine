using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("Tanh")]
    public class FuncTanh : INumericalYield
    {
        public INumericalYield Value { get; }

        public FuncTanh(INumericalYield values)
        {
            Value = values;
        }

        public double Calculate()
        {
            return Math.Tanh(Value.Calculate());
        }

        public override string ToString()
        {
            return $"Tanh({Value})";
        }
    }
}