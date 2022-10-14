using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Expression
{
    public class ConstantValue : INumericalYield
    {
        public static ConstantValue Zero => new ConstantValue(0);

        public double Value { get; }

        public ConstantValue(double value)
        {
            Value = value;
        }

        public double Calculate()
        {
            return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}