using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Expression;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("Round")]
    public class FuncRound : INumericalYield
    {
        public INumericalYield Value { get; }

        public INumericalYield Places { get; }

        public FuncRound(INumericalYield value)
        {
            Value = value;
            Places = ConstantValue.Zero;
        }

        public FuncRound(INumericalYield value, INumericalYield places)
        {
            Value = value;
            Places = places;
        }

        public double Calculate()
        {
            return Math.Round(Value.Calculate(), (int)Places.Calculate());
        }

        public override string ToString()
        {
            return $"Round({Value})";
        }
    }
}