using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions.Constants
{
    [Function("Pi")]
    public class ConstPi : INumericalYield
    {
        public double Calculate() => Math.PI;
    }
}