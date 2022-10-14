using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Operations
{
    [Operation(symbol: '-', order: 20)]
    public class OpMinus : INumericalYield
    {
        private INumericalYield Left { get;  }
        private INumericalYield Right { get;  }

        public OpMinus([Left] INumericalYield left, [Right] INumericalYield right)
        {
            Left = left;
            Right = right;
        }

        public double Calculate()
        {
            return Left.Calculate() - Right.Calculate();
        }

        public override string ToString()
        {
            return $"Minus({Left}, {Right})";
        }
    }
}