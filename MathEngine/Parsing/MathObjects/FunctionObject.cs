using System;
using System.Reflection;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Parsing.MathObjects
{
    public class FunctionObject : IMathsObject
    {
        public Type FunctionType { get; }

        public FunctionAttribute FunctionData { get; }

        public FunctionObject(Type functionType)
        {
            FunctionType = functionType;
            FunctionData = functionType.GetCustomAttribute<FunctionAttribute>();
        }

        public override string ToString()
        {
            return $"Func:{FunctionData.Name}";
        }
    }
}