using System;

namespace MathEngine.Models.Exceptions
{
    /// <summary>
    /// Occurs when there was an error trying to parse an equation or value.
    /// </summary>
    public class BadEquationException : Exception
    {
        public BadEquationException(string error) : base(error)
        {
        }
    }
}