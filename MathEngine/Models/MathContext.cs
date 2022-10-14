using System;
using System.Collections.Generic;
using System.Reflection;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models
{
    public class MathContext
    {
        public Dictionary<char, double> Variables = new Dictionary<char, double>();
        public Dictionary<char, Type> Operations = new Dictionary<char, Type>();
        public Dictionary<string, Type> Functions = new Dictionary<string, Type>(StringComparer.InvariantCultureIgnoreCase);

        public void LoadDefault()
        {
            foreach (var typ in typeof(MathContext).Assembly.GetTypes())
            {
                if (!typeof(INumericalYield).IsAssignableFrom(typ))
                {
                    continue;
                }

                var atr = typ.GetCustomAttribute<OperationAttribute>();
                if (atr != null)
                {
                    Operations[atr.Symbol] = typ;
                }

                var func = typ.GetCustomAttribute<FunctionAttribute>();
                if (func != null)
                {
                    Functions[func.Name] = typ;
                }
            }

            Variables['π'] = Math.PI;
        }
    }
}