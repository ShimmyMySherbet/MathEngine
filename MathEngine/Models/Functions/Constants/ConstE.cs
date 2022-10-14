using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions.Constants
{
    [Function("E")]
    public class ConstE : INumericalYield
    {
        public double Calculate() => Math.E;
    }
}