using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Operations
{
    [Operation(symbol: '^', order: 0)]
    public class OpExponent : INumericalYield
    {
        private INumericalYield Left { get; }
        private INumericalYield Right { get; }

        public OpExponent([Left] INumericalYield left, [Right] INumericalYield right)
        {
            Left = left;
            Right = right;
        }

        public double Calculate()
        {
            return Math.Pow(Left.Calculate(), Right.Calculate());
        }

        public override string ToString()
        {
            return $"Exponent({Left}, {Right})";
        }
    }
}