using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("ASinh")]
    public class FuncASinh : INumericalYield
    {
        public INumericalYield Value { get; }

        public FuncASinh(INumericalYield value)
        {
            Value = value;
        }

        public double Calculate()
        {
            return Math.Asinh(Value.Calculate());
        }
        public override string ToString()
        {
            return $"ASinh({Value})";
        }
    }
}
