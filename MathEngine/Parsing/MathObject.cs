using System.Collections.Generic;
using System.Linq;

namespace MathEngine.Parsing
{
    public class MathObject
    {
        public IMathsObject Content;

        public List<MathObject> Children;

        public MathObject(IMathsObject value)
        {
            Content = value;
        }

        public MathObject(List<MathObject> objects)
        {
            Children = objects;
        }

        public override string ToString()
        {
            if (Content != null)
            {
                return Content.ToString();
            }
            else if (Children != null)
            {
                return '(' + string.Join(", ", Children.Select(x => $"{x}")) + ')';
            }
            else
            {
                return "@Nil";
            }
        }
    }
}