using System;
using MathEngine.Models.Attributes;
using MathEngine.Models.Interfaces;

namespace MathEngine.Models.Functions
{
    [Function("Rand")]
    public class FuncRand : INumericalYield
    {
        public Random Random { get; } = new Random();

        public INumericalYield MaxValue { get; } = null;
        public INumericalYield MinValue { get; } = null;

        public FuncRand()
        {
        }

        public FuncRand(INumericalYield maxValue)
        {
            MaxValue = maxValue;
        }

        public FuncRand(INumericalYield maxValue, INumericalYield minValue)
        {
            MaxValue = maxValue;
            MinValue = minValue;
        }

        public double Calculate()
        {
            if (MinValue == null)
            {
                if (MaxValue == null)
                {
                    return Random.NextDouble();
                }
                return Random.Next((int)MaxValue.Calculate());
            }
            return Random.Next((int)MinValue.Calculate(), (int)MaxValue.Calculate());
        }

        public override string ToString()
        {
            if (MinValue != null)
            {
                if (MaxValue != null)
                {
                    return $"Rand({MaxValue}, {MinValue})";
                }
                return $"Rand({MaxValue}";
            }
            return "Rand()";
        }
    }
}