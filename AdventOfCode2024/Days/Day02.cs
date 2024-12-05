using AoCHelper;

namespace AdventOfCode2024.Days;

public sealed class Day02 : BaseDay
{
    private readonly int[][] _levelLines;

    public Day02()
    {
        _levelLines = File.ReadLines(InputFilePath).Select(line => line.Split(' ').Select(int.Parse).ToArray())
            .ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        var safeCounter = _levelLines.Count(IsSafe);

        return new ValueTask<string>(safeCounter.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var safeCounter = _levelLines.Count(levels => IsSafe(levels) || WithProblemDampener(levels));

        return new ValueTask<string>(safeCounter.ToString());
    }

    bool IsSafe(int[] numbers)
    {
        var isAscending = numbers.Zip(numbers.Skip(1)).All(pair => pair.Second - pair.First is >= 1 and <= 3);
        var isDescending = numbers.Zip(numbers.Skip(1)).All(pair => pair.First - pair.Second is >= 1 and <= 3);

        return isAscending || isDescending;
    }

    bool WithProblemDampener(int[] numbers)
    {
        return numbers.Select((_, i) => numbers.Where((_, index) => index != i).ToArray()).Any(IsSafe);
    }
}
