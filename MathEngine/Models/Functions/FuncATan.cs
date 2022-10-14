using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("ATan")]
    public class FuncATan : INumericalYield
    {
        public INumericalYield Right { get; }

        public FuncATan(INumericalYield right)
        {
            Right = right;
        }

        public double Calculate()
        {
            return Math.Atan(Right.Calculate()) * 180 / Math.PI;
        }
        public override string ToString()
        {
            return $"Tan-1({Right})";
        }
    }
}
