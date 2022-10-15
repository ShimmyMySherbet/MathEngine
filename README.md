# MathEngine
An engine that can parse and calculate math equations, with support for custom operators, functions, and variables

## Examples

```cs
var engine = new MathEngine();
var result = engine.Calculate("Sqrt(7^2+10^2)");
```
```cs
var result = engine.Calculate("(sin(46)*20)/2")
```

```cs
var equation = engine.Build("sqrt(10^2+27.7^2)");
Console.WriteLine(equation);
Console.WriteLine(equation.Calculate());

// Sqrt(Plus(Exponent(10, 2), Exponent(27.7, 2)))
// 29.449787775126666
```

## Features
* Follows Order of Operations
* More than 30 prefefined functions
* Syntax Detection/Checking
* Support for equation variables
* Support for custom functions
* Support for custom operators
* Order of operations settings for custom operators
* View the structure of a built equation
