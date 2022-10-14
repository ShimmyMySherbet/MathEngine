using System;

namespace MathEngine.Models.Attributes
{
    /// <summary>
    /// Designates a class as an operator type, and declares it's symbol and order of operations
    /// Operators must implement <seealso cref="Models.Interfaces.INumericalYield"/>, and provide a single constructor.
    /// The constructor can take multiple values.
    /// Parameter names must include either 'left' or 'right' to designate what side of the operator the value should be taken from.
    /// </summary>
    public class OperationAttribute : Attribute
    {
        /// <summary>
        /// The symbol that represents the operation, as it appears in an equation
        /// </summary>
        public char Symbol { get; }

        /// <summary>
        /// The operator's order
        /// </summary>
        public int Order { get; }

        /// <summary>
        /// Designates a class as an orperator type
        /// </summary>
        /// <param name="symbol">The symbol that represents this operation type in an equation</param>
        /// <param name="order">
        /// Designates the order of operations for this operation. The lower the value the earlier this operation is performed.
        /// Operations of equal order are done at the same time from left to right
        /// </param>
        public OperationAttribute(char symbol, int order = 0)
        {
            Symbol = symbol;
            Order = order;
        }
    }
}