using System;

namespace MathEngine.Models.Attributes
{
    public class FunctionAttribute : Attribute
    {
        public string Name { get; }

        public FunctionAttribute(string name)
        {
            Name = name;
        }
    }
}