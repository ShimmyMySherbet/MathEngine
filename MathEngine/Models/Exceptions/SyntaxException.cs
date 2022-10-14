using System;

namespace MathEngine.Models.Exceptions
{
    /// <summary>
    /// Occurs when a syntax error is detected in an equation
    /// </summary>
    public class SyntaxException : Exception
    {
        public SyntaxException(string error) : base(error)
        {
        }
    }
}