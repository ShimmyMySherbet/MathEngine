namespace MathEngine.Parsing.MathObjects
{
    public class UnknownObject : IMathsObject
    {
        public char Unknown { get; }

        public UnknownObject(char unknown)
        {
            Unknown = unknown;
        }

        public override string ToString()
        {
            return $"${Unknown}$";
        }
    }
}