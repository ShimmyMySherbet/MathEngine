using System.Collections.Generic;

namespace MathEngine.Tokenizing
{
    /// <summary>
    /// Tokenized form of an equation. Call <see cref="ToString"/> for a human readable representation
    /// </summary>
    public class ExpressionToken
    {
        public string Content;
        public List<ExpressionToken> Children;

        public ExpressionToken(string content)
        {
            Content = content;
        }

        public ExpressionToken(List<ExpressionToken> children)
        {
            Children = children;
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Content))
            {
                return Content;
            }
            else
            {
                return $"[{string.Join(",", Children)}]";
            }
        }
    }
}