using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return $"?'{Value}'";
        }

        public double Calculate()
        {
            throw new NotImplementedException();
        }
    }
}
