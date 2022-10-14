using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Operations
{
    [Operation(symbol: '+', order: 20)]
    public class OpPlus : INumericalYield
    {
        public INumericalYield Left { get; }
        public INumericalYield Right { get; }

        public OpPlus([Left] INumericalYield left, [Right] INumericalYield right)
        {
            Left = left;
            Right = right;
        }

        public double Calculate()
        {
            return Left.Calculate() + Right.Calculate();
        }

        public override string ToString()
        {
            return $"Plus({Left}, {Right})";
        }
    }
}