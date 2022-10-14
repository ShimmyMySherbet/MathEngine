using System;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    public class FuncFloor : INumericalYield
    {
        public INumericalYield Value { get; }

        public FuncFloor(INumericalYield value)
        {
            Value = value;
        }

        public double Calculate()
        {
            return Math.Floor(Value.Calculate());
        }

        public override string ToString()
        {
            return $"Floor({Value})";
        }
    }
}