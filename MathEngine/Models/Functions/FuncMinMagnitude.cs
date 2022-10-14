using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("MinMagnitude")]
    public class FuncMinMagnitude : INumericalYield
    {
        public INumericalYield X { get; }
        public INumericalYield Y { get; }

        public FuncMinMagnitude(INumericalYield x, INumericalYield y)
        {
            X = x;
            Y = y;
        }

        public double Calculate()
        {
            return Math.MinMagnitude(X.Calculate(), Y.Calculate());
        }

        public override string ToString()
        {
            return $"MinMag({X}, {Y})";
        }
    }
}