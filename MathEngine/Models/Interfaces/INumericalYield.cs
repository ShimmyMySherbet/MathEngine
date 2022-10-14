namespace MathEngine.Models.Interfaces
{
    public interface INumericalYield
    {
        /// <summary>
        /// Returns the calculated value for this instance.
        /// </summary>
        double Calculate();

        /// <summary>
        /// Provides a human readable form of the equation.
        /// This should provide something like $"SUM({Value1}, {Value2})".
        /// Call ToString on depended values to represent them.
        /// </summary>
        /// <returns>A human readable form of the equation's structure</returns>
        string ToString();
    }
}