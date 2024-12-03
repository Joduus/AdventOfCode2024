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
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public Day03()
    {
        var inputFileStream = File.Open(InputFilePath, FileMode.Open);
        _input = new StreamReader(inputFileStream).ReadToEnd();
    }

    public override ValueTask<string> Solve_1()
    {
        var mulMatches = _mulRegexSimple.Matches(_input);

        var mulSum = 0;
        foreach (Match match in mulMatches)
        {
            mulSum += Convert.ToInt32(match.Groups[1].Value) * Convert.ToInt32(match.Groups[2].Value);
        }

        return new ValueTask<string>(mulSum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var instructionMatches = _mulRegexWithInstructions.Matches(_input);

        var mulSum = 0;
        var multiplicationAllowed = true;
        foreach (Match match in instructionMatches)
        {
            switch (match.Groups[1].Value)
            {
                case "do":
                    multiplicationAllowed = true;
                    break;
                case "don't":
                    multiplicationAllowed = false;
                    break;
                case "mul":
                    if (multiplicationAllowed)
                    {
                        mulSum += Convert.ToInt32(match.Groups[2].Value) * Convert.ToInt32(match.Groups[3].Value);
                    }

                    break;
            }
        }

        return new ValueTask<string>(mulSum.ToString());
    }
}
