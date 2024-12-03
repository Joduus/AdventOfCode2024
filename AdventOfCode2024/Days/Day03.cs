using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode2024.Days;

public class Day03 : BaseDay
{
    private StreamReader InputFileStream => new StreamReader(File.Open(InputFilePath, FileMode.Open));

    private static Regex _mulRegex = new(@"mul\((\d+),(\d+)\)", RegexOptions.Compiled | RegexOptions.NonBacktracking | RegexOptions.CultureInvariant);

    public override ValueTask<string> Solve_1()
    {
        var input = InputFileStream.ReadToEnd();
        var mulMatches = _mulRegex.Matches(input);

        var mulSum = 0;
        foreach (Match match in mulMatches)
        {
            mulSum += Convert.ToInt32(match.Groups[1].Value) * Convert.ToInt32(match.Groups[2].Value);
        }

        return new ValueTask<string>(mulSum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        throw new NotImplementedException();
    }
}
