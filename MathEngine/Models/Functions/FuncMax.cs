using System.Linq;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("Max")]
    public class FuncMax : INumericalYield
    {
        public INumericalYield[] Values { get; }

        public FuncMax(INumericalYield[] values)
        {
            Values = values;
        }

        public double Calculate()
        {
            double max = double.NaN;
            var set = false;

            foreach (var value in Values)
            {
                var newValue = value.Calculate();
                if (!set || newValue > max)
                {
                    set = true;
                    max = newValue;
                }
            }
            return max;
        }

        public override string ToString()
        {
            return $"Max({string.Join(", ", Values.Select(x => x.ToString()))})";
        }
    }
}