using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Exceptions;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("ILogB")]
    public class FuncILogB : INumericalYield
    {
        public INumericalYield Value { get; }

        public FuncILogB(INumericalYield value)
        {
            Value = value;
        }

        public double Calculate()
        {
            return Math.ILogB(Value.Calculate());
        }

        public override string ToString()
        {
            return $"ILogB({Value})";
        }
    }
}