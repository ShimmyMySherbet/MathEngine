namespace MathEngine.Parsing.MathObjects
{
    public class NumericObject : IMathsObject
    {
        public double Value { get; }

        public NumericObject(double value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}