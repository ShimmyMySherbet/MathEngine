using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("CopySign")]
    public class FuncCopySign : INumericalYield
    {
        public INumericalYield X { get; }
        public INumericalYield Y { get; }

        public FuncCopySign(INumericalYield x, INumericalYield y)
        {
            X = x;
            Y = y;
        }

        public double Calculate()
        {
            return Math.CopySign(X.Calculate(), Y.Calculate());
        }

        public override string ToString()
        {
            return $"CopySign({X}, {Y})";
        }
    }
}