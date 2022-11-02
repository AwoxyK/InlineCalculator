# Inline Calculator

This project is the **C#** Library. There is the method in this library that takes string and solves it as math problem. Every newbie can type simple program that accepts two numbers and an operator. But I did that calculator, but in one line to input with many of math actions.

## Examples

```c#
using System;
using InlineCalculator;

string mathProblem = "(12*4)-(6*4)/2";
float result = Calculator.Solve(mathProblem)

Console.WriteLine(result); // 36.0f
```

```c#
using System;
using InlineCalculator;

string mathProblem = "5/2";
float result = Calculator.Solve(mathProblem)

Console.WriteLine(result); // 2.5f
```

## Authors

- [@AwoxyK](https://www.github.com/AwoxyK)

[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/)
