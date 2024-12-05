using System.Buffers;
using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode2024.Days;

public sealed class Day03 : BaseDay
{
    private readonly string _input;

    private static Regex _mulRegexSimple = new(@"mul\((\d+),(\d+)\)",
        RegexOptions.Compiled | RegexOptions.NonBacktracking | RegexOptions.CultureInvariant);

    private static Regex _mulRegexWithInstructions = new(@"(mul(?=\((\d+),(\d+)\))|do(?:n't)?(?=\(\)))",
        RegexOptions.Compiled  | RegexOptions.CultureInvariant);

    public Day03()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var mulSum = _mulRegexSimple.Matches(_input)
            .Sum(match => Convert.ToInt32(match.Groups[1].Value) * Convert.ToInt32(match.Groups[2].Value));

        return new ValueTask<string>(mulSum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var mulSum = _mulRegexWithInstructions.Matches(_input)
            .Aggregate((sum: 0, multiplicationAllowed: true), (acc, match) =>
        {
            switch (match.Groups[1].Value)
            {
                case "do":
                        acc.multiplicationAllowed = true;
                    break;
                case "don't":
                        acc.multiplicationAllowed = false;
                    break;
                case "mul":
                        if (acc.multiplicationAllowed)
                        {
                            acc.sum += Convert.ToInt32(match.Groups[2].Value) * Convert.ToInt32(match.Groups[3].Value);
                        }
                        break;
            }
                return acc;
            }).sum;

        return new ValueTask<string>(mulSum.ToString());
    }
}
