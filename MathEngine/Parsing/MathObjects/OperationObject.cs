using System;
using System.Reflection;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Parsing.MathObjects
{
    public class OperationObject : IMathsObject
    {
        public Type OperationType { get; }
        public OperationAttribute Symbol;

        public OperationObject(Type operation)
        {
            OperationType = operation;
            Symbol = operation.GetCustomAttribute<OperationAttribute>();
        }

        public override string ToString()
        {
            return $"{Symbol.Symbol}";
        }
    }
}