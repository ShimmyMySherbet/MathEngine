using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Operations
{
    [Operation(symbol: '*', order: 10)]
    public class OpMultiply : INumericalYield
    {
        private INumericalYield Left { get; }
        private INumericalYield Right { get; }

        public OpMultiply([Left] INumericalYield left, [Right] INumericalYield right)
        {
            Left = left;
            Right = right;
        }

        public double Calculate()
        {
            return Left.Calculate() * Right.Calculate();
        }

        public override string ToString()
        {
            return $"Multiply({Left}, {Right})";
        }
    }
}