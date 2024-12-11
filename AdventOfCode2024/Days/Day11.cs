using AoCHelper;

namespace AdventOfCode2024.Days;

public sealed class Day11 : BaseDay
{
    private const int Part1Blinks = 25;

    private readonly long[] _input;

    public Day11()
    {
        _input = File.ReadLines(InputFilePath).SelectMany(s => s.Split(' ').Select(long.Parse)).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        var stones = _input.ToList();
        for (var i = 0; i < Part1Blinks; i++)
        {
            stones = stones.SelectMany(stone => stone switch
            {
                0 => [1],
                _ => stone.ToString().Length % 2 == 0
                    ? [int.Parse(stone.ToString().AsSpan(0, stone.ToString().Length / 2)), int.Parse(stone.ToString().AsSpan(stone.ToString().Length / 2))]
                    : new[] {stone * 2024}
            }).ToList();
        }

        return new ValueTask<string>(stones.Count.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        throw new NotImplementedException();
    }
}
