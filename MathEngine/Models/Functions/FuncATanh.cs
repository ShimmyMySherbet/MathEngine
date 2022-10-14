using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("ATanh")]
    public class FuncATanh : INumericalYield
    {
        public INumericalYield Value { get; }

        public FuncATanh(INumericalYield value)
        {
            Value = value;
        }

        public double Calculate()
        {
            return Math.Atanh(Value.Calculate());
        }
        public override string ToString()
        {
            return $"Tan-1({Value})";
        }
    }
}
