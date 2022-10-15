using System;
using System.Reflection;
using MathEngine.Models;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;
using MathEngine.Parsing;
using MathEngine.Tokenizing;

namespace MathEngine
{
    /// <summary>
    /// Allows the parsing, building, and calculating of equations in text form.
    /// </summary>
    public class MathEngine
    {
        public MathContext Context { get; set; }
        public MathTokenizer Tokenizer { get; init; } = new MathTokenizer();
        public MathParser Parser { get; init; } = new MathParser();
        public MathBuilder Builder { get; init; } = new MathBuilder();

        /// <summary>
        /// Creates a new instance of the Math Engine with the default context.
        /// The default context provides all built-in functions and operators.
        /// </summary>
        public MathEngine()
        {
            Context = new MathContext();
            Context.LoadDefault();
        }

        /// <summary>
        /// Creates a new instance of the Math Engine with the specified context
        /// </summary>
        /// <param name="context"></param>
        public MathEngine(MathContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Parses and builds an equation. This can be used to calculate the equation, and also to provide a human-readable form of the built equation with ToString()
        /// </summary>
        /// <param name="equation">The equation in text format to build</param>
        /// <returns>The built equation. Call <seealso cref="INumericalYield.Calculate"/> to calculate the result of the equation,
        /// or call <seealso cref="INumericalYield.ToString"/> for a structural view of the equation</returns>
        /// <exception cref="Models.Exceptions.SyntaxException">Raised when a syntax error is detected in the equation</exception>
        public INumericalYield Build(string equation)
        {
            var tokens = Tokenizer.Tokenize(Context, equation);
            var parsed = Parser.Parse(tokens, Context);
            return Builder.Build(parsed, Context);
        }

        /// <summary>
        /// Parses, builds, and executes an equation
        /// </summary>
        /// <param name="equation">The equation in text form to calculate</param>
        /// <returns>The result of the equation</returns>
        /// <exception cref="Models.Exceptions.SyntaxException">Raised when a syntax error is detected in the equation</exception>
        public double Calculate(string equation)
        {
            return Build(equation).Calculate();
        }

        /// <summary>
        /// Sets a variable that can be used in the parsing of equations
        /// </summary>
        /// <param name="name">A character that represents the value in the equation</param>
        /// <param name="value">The value of the variable</param>
        public void SetVariable(char name, double value)
        {
            Context.Variables[name] = value;
        }

        /// <summary>
        /// Registers an operator that can be used in parsing, building and executing an equation.
        /// Operators must inherit <seealso cref="INumericalYield"/>, have the <seealso cref="OperationAttribute"/>, and provide a constructor.
        /// Constructor argument names must contain either 'left' or 'right' in their name. These values will provide the value to the left or right of the operator.
        /// It is also recommended to implement ToString() to provide a human readable form of the built structure
        /// </summary>
        /// <param name="operatorType">The class type of the operator</param>
        public void RegisterOperator(Type operatorType)
        {
            if (!typeof(INumericalYield).IsAssignableFrom(operatorType))
            {
                throw new ArgumentException("Operator type does not implement INumericalYield");
            }

            var opValue = operatorType.GetCustomAttribute<OperationAttribute>();

            if (opValue == null)
            {
                throw new ArgumentException($"Provided operator type does not provide attribute 'OperationAttribute'");
            }

            if (operatorType.IsInterface || operatorType.IsAbstract)
            {
                throw new ArgumentException("Provided operator type cannot be abstract or an interface");
            }

            Context.Operations[opValue.Symbol] = operatorType;
        }

        /// <summary>
        /// Registers an operator that can be used in parsing, building and executing an equation.
        /// Operators must inherit <seealso cref="INumericalYield"/>, provide <seealso cref="OperationAttribute"/>, and provide a constructor.
        /// Constructor argument names must contain either 'left' or 'right' in their name. These values will provide the value to the left or right of the operator.
        /// It is also recommended to implement ToString() to provide a human readable form of the built structure
        /// </summary>
        /// <param name="operatorType">The class type of the operator</param>
        public void RegisterOperator<T>() where T : INumericalYield
        {
            RegisterOperator(typeof(T));
        }

        /// <summary>
        /// Registers a function that can be used in parsing, building, and calculating equations. Function classes must inherit <seealso cref="INumericalYield"/>,
        /// and provide <seealso cref="FunctionAttribute"/>.
        /// Constructors can have no arguments for a constant value, any number of <seealso cref="INumericalYield"/> to accept different numbers of inputs,
        /// or an array of <seealso cref="INumericalYield"/> to accept any number of inputs.
        /// It is also recommended to implement ToString() to provide a human readable form of the built structure
        /// </summary>
        /// <param name="functionType">The class type of the function</param>
        public void RegisterFunction(Type functionType)
        {
            if (!typeof(INumericalYield).IsAssignableFrom(functionType))
            {
                throw new ArgumentException("Function type does not implement INumericalYield");
            }

            var funcValue = functionType.GetCustomAttribute<FunctionAttribute>();

            if (funcValue == null)
            {
                throw new ArgumentException($"Provided function type does not provide attribute 'FunctionAttribute'");
            }

            if (functionType.IsInterface || functionType.IsAbstract)
            {
                throw new ArgumentException("Provided function type cannot be abstract or an interface");
            }

            Context.Functions[funcValue.Name] = functionType;
        }

        /// <summary>
        /// Registers a function that can be used in parsing, building, and calculating equations. Function classes must inherit <seealso cref="INumericalYield"/>,
        /// and provide <seealso cref="FunctionAttribute"/>.
        /// Constructors can have no arguments for a constant value, any number of <seealso cref="INumericalYield"/> to accept different numbers of inputs,
        /// or an array of <seealso cref="INumericalYield"/> to accept any number of inputs.
        /// It is also recommended to implement ToString() to provide a human readable form of the built structure
        /// </summary>
        /// <param name="functionType">The class type of the function</param>
        public void RegisterFunction<T>() where T : INumericalYield
        {
            RegisterFunction(typeof(T));
        }

        /// <summary>
        /// Deregisters an operator from the Maths Context.
        /// Note: This can also deregister elementary operators such as + and *, breaking equations
        /// </summary>
        /// <param name="op">The symbol that represents the operator to remove</param>
        public void DeregisterOperator(char op)
        {
            Context.Operations.Remove(op);
        }

        /// <summary>
        /// Deregisters a function from the Maths Context.
        /// Note: This can also deregister common functions sucn as Sin and Tan, which can break equations that rely on them
        /// </summary>
        /// <param name="op">The name of the function to remove</param>
        public void DeregisterFunction(string functionName)
        {
            Context.Functions.Remove(functionName);
        }
    }
}