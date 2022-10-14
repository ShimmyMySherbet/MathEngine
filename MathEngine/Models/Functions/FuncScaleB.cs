using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("ScaleB")]
    public class FuncScaleB : INumericalYield
    {
        public INumericalYield X { get; }
        public INumericalYield N { get; }

        public FuncScaleB(INumericalYield x, INumericalYield n)
        {
            X = x;
            N = n;
        }

        public double Calculate()
        {
            return Math.ScaleB(X.Calculate(), (int)N.Calculate());
        }

        public override string ToString()
        {
            return $"ScaleB::{X}*2^{N}";
        }
    }
}