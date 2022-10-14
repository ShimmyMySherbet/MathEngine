using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("MaxMagnitude")]
    public class FuncMaxMangitude : INumericalYield
    {
        public INumericalYield X { get; }
        public INumericalYield Y { get; }

        public FuncMaxMangitude(INumericalYield x, INumericalYield y)
        {
            X = x;
            Y = y;
        }

        public double Calculate()
        {
            return Math.MaxMagnitude(X.Calculate(), Y.Calculate());
        }

        public override string ToString()
        {
            return $"MaxMag({X}, {Y})";
        }
    }
}