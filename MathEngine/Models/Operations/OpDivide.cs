using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Operations
{
    [Operation(symbol: '/', order: 10)]
    public class OpDivide : INumericalYield
    {
        private INumericalYield Left { get; }
        private INumericalYield Right { get; }

        public OpDivide([Left] INumericalYield left, [Right] INumericalYield right)
        {
            Left = left;
            Right = right;
        }

        public double Calculate()
        {
            return Left.Calculate() / Right.Calculate();
        }

        public override string ToString()
        {
            return $"Divide({Left}, {Right})";
        }
    }
}