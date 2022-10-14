using System.Linq;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("Min")]
    public class FuncMin : INumericalYield
    {
        public INumericalYield[] Values { get; }

        public FuncMin(INumericalYield[] values)
        {
            Values = values;
        }

        public double Calculate()
        {
            double min = double.NaN;
            var set = false;

            foreach (var value in Values)
            {
                var newValue = value.Calculate();
                if (!set || newValue < min)
                {
                    set = true;
                    min = newValue;
                }
            }
            return min;
        }

        public override string ToString()
        {
            return $"Min({string.Join(", ", Values.Select(x => x.ToString()))})";
        }
    }
}