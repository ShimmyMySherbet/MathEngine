using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using MathEngine.Models;
using MathEngine.Models.Attributes;
using MathEngine.Models.Exceptions;
using MathEngine.Models.Expression;
using MathEngine.Models.Interfaces;
using MathEngine.Models.Operations;
using MathEngine.Parsing.MathObjects;

namespace MathEngine.Parsing
{
    public class MathBuilder
    {
        /// <summary>
        /// Builds the calculatable equation from the provided parsed equation
        /// </summary>
        /// <exception cref="BadEquationException">Raised when the method is given invalid data</exception>
        /// <exception cref="SyntaxException">Raised when a syntax error is detected in the equation</exception>
        public INumericalYield Build(MathObject parsedEquation, MathContext context, bool funcParams = false)
        {
            var workingList = new List<INumericalYield>();

            if (parsedEquation.Children != null && parsedEquation.Children.Count > 0)
            {
                var lastIsFunc = false;
                for (int i = 0; i < parsedEquation.Children.Count; i++)
                {
                    var o = parsedEquation.Children[i];
                    if (o.Children != null && o.Children.Count > 0)
                    {
                        workingList.Add(Build(o, context, lastIsFunc));
                    }
                    else
                    {
                        workingList.Add(new UnresolvedObject(o.Content));
                        lastIsFunc = o.Content is FunctionObject;
                    }
                }
            }
            else
            {
                workingList.Add(new UnresolvedObject(parsedEquation.Content));
            }

            // replace constant values
            for (int i = 0; i < workingList.Count; i++)
            {
                var item = workingList[i];

                if (item is UnresolvedObject unr)
                {
                    if (unr.Value is NumericObject no)
                    {
                        workingList[i] = new ConstantValue(no.Value);
                    }
                    else if (unr.Value is UnknownObject unkn)
                    {
                        if (context.Variables.TryGetValue(unkn.Unknown, out var constant))
                        {
                            workingList[i] = new ConstantValue(constant);
                        }

                        if (i != 0)
                        {
                            var prev = workingList[i - 1];
                            if (prev is not UnresolvedObject unrr || unrr.Value is not OperationObject)
                            {
                                workingList.Insert(i, new UnresolvedObject(new OperationObject(typeof(OpMultiply))));
                                i += 1;
                            }
                        }

                        if (i < workingList.Count - 1)
                        {
                            var next = workingList[i + 1];

                            if (next is not UnresolvedObject unrr || unrr.Value is not OperationObject)
                            {
                                workingList.Insert(i + 1, new UnresolvedObject(new OperationObject(typeof(OpMultiply))));
                            }
                        }
                    }
                }
            }

            var orders = new List<int>();
            var orderMap = new Dictionary<Type, int>();

            for (int i = 0; i < workingList.Count; i++)
            {
                if (workingList[i] is UnresolvedObject unresolved && unresolved.Value is OperationObject op)
                {
                    if (!orderMap.ContainsKey(op.OperationType))
                    {
                        var operation = op.OperationType.GetCustomAttribute<OperationAttribute>();

                        orders.Add(operation.Order);
                        orderMap[op.OperationType] = operation.Order;
                    }
                }
            }


            // Functions
            for (int i = 0; i < workingList.Count; i++)
            {
                if (workingList[i] is UnresolvedObject unresolved && unresolved.Value is FunctionObject)
                {
                    SetFunction(workingList, ref i);
                }
            }

            // Operations

            foreach (var order in orders.OrderBy(x => x))
            {
                for (int i = 0; i < workingList.Count; i++)
                {
                    if (workingList[i] is UnresolvedObject unresolved
                        && unresolved.Value is OperationObject operation)
                    {
                        var operationOrder = orderMap[operation.OperationType];

                        if (order == operationOrder)
                        {
                            SetOperation(workingList, ref i);
                        }
                    }
                }
            }
            
            for (int i = 0; i < workingList.Count; i++)
            {
                if (workingList[i] is UnresolvedObject unresolved)
                {
                    throw new SyntaxException($"Syntax error in equation near {unresolved.Value}");
                }
            }

            if (funcParams)
            {
                return new UnresolvedObject(new ParametersObject(workingList));
            }

            if (workingList.Count > 1)
            {
                throw new SyntaxException($"Equation does not formulate into a single function. Check the equation syntax. Invalid formula: [{string.Join(", ", workingList)}]");
            }

            return workingList[0];
        }

        /// <exception cref="BadEquationException">Raised when the method is given invalid data</exception>
        /// <exception cref="SyntaxException">Raised when a syntax error is detected in the equation</exception>
        private void SetFunction(List<INumericalYield> set, ref int index)
        {
            var opValue = set[index];

            if (opValue is not UnresolvedObject unresolved || unresolved.Value is not FunctionObject function)
            {
                throw new BadEquationException($"Cannot set function value at {index}. Type is {opValue.GetType().Name}, Expected: FunctionObject");
            }

            var constructors = function.FunctionType.GetConstructors();

            ParametersObject functionParameters = null;
            var nextItemNotParameters = true;

            if (index < set.Count - 1)
            {
                var next = set[index + 1];

                if (next is UnresolvedObject unresolvedArgs)
                {
                    if (unresolvedArgs.Value is ParametersObject parameters)
                    {
                        functionParameters = parameters;
                        set.RemoveAt(index + 1);
                        nextItemNotParameters = false;
                    }
                }
            }

            var suppliedArguments = functionParameters?.Yields?.Count ?? 0;

            ConstructorInfo selectedConstructor = null;
            ConstructorInfo dynamicConstructor = null;

            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters();
                var isDynamic = parameters.Length > 0 && parameters[0].ParameterType == typeof(INumericalYield[]);

                if (parameters.Length == suppliedArguments && !isDynamic)
                {
                    selectedConstructor = constructor;
                }

                if (isDynamic)
                {
                    dynamicConstructor = constructor;
                }
            }

            object[] constructorArguments;

            if (selectedConstructor == null)
            {
                if (dynamicConstructor == null)
                {
                    if (nextItemNotParameters)
                    {
                        throw new SyntaxException($"Expected arguments after function '{function.FunctionType.Name}'; Values missing brackets or arguments");
                    } else
                    {
                        throw new SyntaxException($"Bad amount of arguments for function '{function.FunctionType.Name}'. Arguments supplied: {suppliedArguments}");
                    }
                }
                else if (suppliedArguments == 0)
                {
                    if (nextItemNotParameters)
                    {
                        throw new SyntaxException($"Expected arguments after function '{function.FunctionType.Name}'; Values missing brackets or arguments");
                    }
                    else
                    {
                        throw new SyntaxException($"Expected arguments for function '{function.FunctionType.Name}'");
                    }
                }

                selectedConstructor = dynamicConstructor;
                constructorArguments = new object[1];

                constructorArguments[0] = functionParameters.Yields.ToArray();
            }
            else
            {
                var constructorParameters = selectedConstructor.GetParameters();
                constructorArguments = new object[constructorParameters.Length];

                for (int i = 0; i < constructorArguments.Length; i++)
                {
                    constructorArguments[i] = functionParameters.Yields[i];
                }
            }

            set[index] = (INumericalYield)selectedConstructor.Invoke(parameters: constructorArguments);
        }

        /// <exception cref="BadEquationException">Raised when the method is given invalid data</exception>
        /// <exception cref="SyntaxException">Raised when a syntax error is detected in the equation</exception>
        private void SetOperation(List<INumericalYield> set, ref int index)
        {
            var opValue = set[index];

            if (opValue is not UnresolvedObject unresolved || unresolved.Value is not OperationObject operation)
            {
                throw new BadEquationException($"Cannot set operation value at {index}. Type is {opValue.GetType().Name}, Expected: OperationObject");
            }

            var constructors = operation.OperationType.GetConstructors();
            var firstConstructor = constructors[0];

            var parameters = firstConstructor.GetParameters();
            var arguments = new object[parameters.Length];

            for (int i = 0; i < arguments.Length; i++)
            {
                var arg = parameters[i];
                if (arg.GetCustomAttribute<LeftAttribute>() != null)
                {
                    if (index == 0)
                    {
                        throw new SyntaxException($"Expected value before operator {operation.Symbol.Symbol}");
                    }

                    arguments[i] = set[index - 1];
                    set.RemoveAt(index - 1);
                    index -= 1;
                }
                else if (arg.GetCustomAttribute<RightAttribute>() != null)
                {
                    if (index >= set.Count - 1)
                    {
                        throw new SyntaxException($"Expected value after operator {operation.Symbol.Symbol}");
                    }

                    arguments[i] = set[index + 1];
                    set.RemoveAt(index + 1);
                }
                else
                {
                    throw new BadEquationException($"Cannot formulate constructor for {operation.OperationType.Name}: Argument {arg.Name} is not tagged with LeftAttribute or RightAttribute");
                }
            }

            // Syntax error checking
            for (int i = 0; i < arguments.Length; i++)
            {
                if (arguments[i] is UnresolvedObject unresolvedArgument)
                {
                    var first = "";
                    var rest = "";

                    if (arguments.Length >= 1)
                    {
                        first = arguments[0].ToString();
                    }

                    // For edge cases where a custom operator has multiple symbols
                    // lump the rest to the right of the operator in the ex message
                    if (arguments.Length > 1)
                    {
                        rest = string.Join(",", arguments.Skip(1));
                    }

                    throw new SyntaxException($"Syntax error for operation {operation.Symbol.Symbol}, value: \"{unresolvedArgument.Value}\". Near {first}{operation.Symbol.Symbol}{rest}");
                }
            }


            set[index] = (INumericalYield)Activator.CreateInstance(operation.OperationType, args: arguments);
        }
    }
}