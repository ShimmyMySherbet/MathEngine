using MathEngine.Models.Exceptions;
using MathEngine.Models.Interfaces;

namespace MathEngine.Parsing.MathObjects
{
    public class UnresolvedObject : IMathsObject, INumericalYield
    {
        public IMathsObject Value { get; }

        public UnresolvedObject(IMathsObject value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"{Value}";
        }

        public double Calculate()
        {
            throw new BadEquationException($"Cannot calculate unresolved object wrapping {Value}");
        }
    }
}