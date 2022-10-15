using System;
using System.Diagnostics;
using MathEngine.Models.Exceptions;

namespace MathEngine.Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var engine = new MathEngine();

            while (true)
            {
                Console.Write("Equation: ");
                var eq = Console.ReadLine();

                if (string.IsNullOrEmpty(eq))
                {
                    Console.Clear();
                    continue;
                }

                // Allow setting variables in equation
                char setOut = '\0';

                if (eq.IndexOf('=') != -1)
                {
                    var symbol = eq.Substring(0, eq.IndexOf('=')).Trim(' ');
                    eq = eq.Substring(symbol.Length + 1);

                    if (symbol.Length == 1)
                    {
                        setOut = symbol[0];
                    }
                }

                try
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    var built = engine.Build(eq);
                    sw.Stop();

                    Console.WriteLine();
                    Console.WriteLine($"Parsed Structure: {built}");
                    Console.WriteLine();

                    long buildTime = sw.ElapsedTicks;

                    sw.Restart();
                    var result = built.Calculate();
                    sw.Stop();

                    Console.WriteLine($"Result: {result}");
                    Console.WriteLine($"Calculation time: {Math.Round(sw.ElapsedTicks / 10000f, 3)}ms");
                    Console.WriteLine($"Build Time: {Math.Round(buildTime / 10000f, 3)}ms");
                    if (setOut != '\0')
                    {
                        engine.SetVariable(setOut, result);
                        Console.WriteLine($"Output stored in variable: {setOut}");
                    }

                    Console.WriteLine();
                    Console.WriteLine();
                }
                catch (SyntaxException syntax)
                {
                    Console.WriteLine($"Syntax Error: {syntax.Message}");
                }
#if RELEASE
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");

#endif
            }
        }
    }
}