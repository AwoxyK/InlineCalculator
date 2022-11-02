using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace InlineCalculator;

public static class Calculator
{
    private static float ExecuteOperator(string mathOperator, MatchCollection numbers)
    {
        return mathOperator switch
        {
            "+" => float.Parse(numbers[0].Value) + float.Parse(numbers[1].Value),
            "-" => float.Parse(numbers[0].Value) - float.Parse(numbers[1].Value),
            "*" => float.Parse(numbers[0].Value) * float.Parse(numbers[1].Value),
            "/" => float.Parse(numbers[0].Value) / float.Parse(numbers[1].Value),
            _ => throw new Exception("Current math question has no operators that this program can solve: (+ - * /)")
        };
    }
    private static IEnumerable<string> GetParenthesisContent(string mathQuestion)
    {
        var sb = new StringBuilder();
        var canWrite = false;

        foreach (var symbol in mathQuestion)
        {
            switch (symbol)
            {
                case '(':
                    canWrite = true;
                    continue;
                case ')':
                {
                    canWrite = false;
                    yield return sb.ToString();
                    sb.Clear();
                    break;
                }
            }

            if (canWrite)
            {
                sb.Append(symbol);
            }
        }
    }
    
    private static string ReplaceParenthesis(string mathQuestion)
    {
        var parenthesisList = new List<string>(GetParenthesisContent(mathQuestion));
        
        foreach (var content in parenthesisList)
        {
            var matches = Regex.Matches(content, @"(\d)+");
            var mathOperator = Regex.Match(content, @"[\+\-\*\/]");
            var result = ExecuteOperator(mathOperator.Value, matches);

            mathQuestion = mathQuestion.Replace($"({content})", $"{result}");
        }

        return mathQuestion;
    }
    
    public static float Solve(string mathQuestion)
    {
        mathQuestion = ReplaceParenthesis(mathQuestion);
        
        while (mathQuestion.Contains('*') || mathQuestion.Contains('/'))
        {
            var matching = Regex.Match(mathQuestion, @"(\d+)[(\*)|(/)](\d+)");
            var numbers = Regex.Matches(matching.Value, @"(\d+)");
            var mathOperator = Regex.Match(matching.Value, @"[\*\/]");
            var result = ExecuteOperator(mathOperator.Value, numbers);
            
            mathQuestion = mathQuestion.Replace(matching.Value, result.ToString(CultureInfo.InvariantCulture));
        }

        while (mathQuestion.Contains('+') || mathQuestion.Contains('-'))
        {
            var matching = Regex.Match(mathQuestion, @"(\d+)[(\+)|(\-)](\d+)");
            var numbers = Regex.Matches(matching.Value, @"(\d+)");
            var mathOperator = Regex.Match(matching.Value, @"[\+\-]");
            var result = ExecuteOperator(mathOperator.Value, numbers);

            mathQuestion = mathQuestion.Replace(matching.Value, result.ToString(CultureInfo.InvariantCulture));
        }
        
        return float.Parse(mathQuestion);
    }
}