using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions.Constants
{
    [Function("Tau")]
    public class ConstTau : INumericalYield
    {
        public double Calculate() => Math.Tau;
    }
}