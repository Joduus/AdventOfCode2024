using AoCHelper;

namespace AdventOfCode2024.Days;

public sealed class Day01 : BaseDay
{
    private readonly List<int> _leftList = [];
    private readonly List<int> _rightList = [];

    public Day01()
    {
        _leftList = File.ReadLines(InputFilePath).Select(x => int.Parse(x.AsSpan(0, 5))).ToList();
        _rightList = File.ReadLines(InputFilePath).Select(x => int.Parse(x.AsSpan(8, 5))).ToList();

        _leftList.Sort();
        _rightList.Sort();
    }

    public override ValueTask<string> Solve_1()
    {
        var totalDistance = _leftList.Zip(_rightList, (left, right) => Math.Abs(left - right)).Sum();

        return new ValueTask<string>(totalDistance.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var totalSimilarity = _leftList.Intersect(_rightList).Sum(duplicateNumber => duplicateNumber * _rightList.Count(x => x == duplicateNumber));

        return new ValueTask<string>(totalSimilarity.ToString());
    }
}
