using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("Clamp")]
    public class FuncClamp : INumericalYield
    {
        public INumericalYield Value { get; }
        public INumericalYield Min { get; }
        public INumericalYield Max { get; }

        public FuncClamp(INumericalYield value, INumericalYield min, INumericalYield max)
        {
            Value = value;
            Min = min;
            Max = max;
        }

        public double Calculate()
        {
            return Math.Clamp(Value.Calculate(), Min.Calculate(), Max.Calculate());
        }
        public override string ToString()
        {
            return $"Clamp({Value}, {Min}, {Max})";
        }
    }
}
