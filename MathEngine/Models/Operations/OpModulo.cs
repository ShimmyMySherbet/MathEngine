using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Operations
{
    [Operation(symbol: '%', order: 10)]
    public class OpModulo : INumericalYield
    {
        public INumericalYield Value1 { get; }
        public INumericalYield Value2 { get; }

        public OpModulo([Left] INumericalYield value1, [Right] INumericalYield value2)
        {
            Value1 = value1;
            Value2 = value2;
        }

        public double Calculate()
        {
            return Value1.Calculate() % Value2.Calculate();
        }

        public override string ToString()
        {
            return $"Modulo({Value1}, {Value2})";
        }
    }
}