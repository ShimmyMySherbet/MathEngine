using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("ATan2")]
    public class FuncATan2 : INumericalYield
    {
        public INumericalYield Value1 { get; }
        public INumericalYield Value2 { get; }

        public FuncATan2(INumericalYield value1, INumericalYield value2)
        {
            Value1 = value1;
            Value2 = value2;
        }

        public double Calculate()
        {
            return Math.Atan2(Value1.Calculate(), Value2.Calculate());
        }

        public override string ToString()
        {
            return $"ATan2({Value1}, {Value2})";
        }
    }
}