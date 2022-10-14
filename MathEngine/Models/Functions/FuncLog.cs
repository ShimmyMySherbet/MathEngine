using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("Log")]
    public class FuncLog : INumericalYield
    {
        public INumericalYield Value { get; }

        public FuncLog(INumericalYield value)
        {
            Value = value;
        }

        public double Calculate()
        {
            return Math.Log(Value.Calculate());
        }

        public override string ToString()
        {
            return $"Log({Value})";
        }
    }
}