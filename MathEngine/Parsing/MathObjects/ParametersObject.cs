using System.Collections.Generic;
using MathEngine.Models.Interfaces;

namespace MathEngine.Parsing.MathObjects
{
    public class ParametersObject : IMathsObject
    {
        public List<INumericalYield> Yields { get; }

        public ParametersObject(List<INumericalYield> yields)
        {
            Yields = yields;
        }

        public override string ToString()
        {
            return $"[{string.Join(", ", Yields)}]";
        }
    }
}