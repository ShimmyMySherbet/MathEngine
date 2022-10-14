using System.Collections.Generic;
using MathEngine.Models;
using MathEngine.Models.Exceptions;
using MathEngine.Parsing.MathObjects;
using MathEngine.Tokenizing;

namespace MathEngine.Parsing
{
    public class MathParser
    {
        public MathObject Parse(ExpressionToken token, MathContext context)
        {
            if (token.Children != null && token.Children.Count > 0)
            {
                var lst = new List<MathObject>();

                foreach (var obj in token.Children)
                {
                    lst.Add(Parse(obj, context));
                }

                return new MathObject(lst);
            }
            var content = token.Content;

            if (double.TryParse(content, out var value))
            {
                return new MathObject(new NumericObject(value));
            }

            if (content.Length == 1)
            {
                if (context.Operations.TryGetValue(content[0], out var op))
                {
                    return new MathObject(new OperationObject(op));
                }
            }

            if (content.Length == 1)
            {
                return new MathObject(new UnknownObject(content[0]));
            }

            if (context.Functions.TryGetValue(content, out var func))
            {
                return new MathObject(new FunctionObject(func));
            }

            throw new SyntaxException($"Unknown object '{content}'");
        }
    }
}