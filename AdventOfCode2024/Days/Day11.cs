using System.Collections.Concurrent;
using AoCHelper;

namespace AdventOfCode2024.Days;

public sealed class Day11 : BaseDay
{
    private const int Part1Blinks = 25;
    private const int Part2Blinks = 75;

    private readonly long[] _input;

    public Day11()
    {
        _input = File.ReadLines(InputFilePath)
            .SelectMany(s => s.Split(' ').Select(long.Parse))
            .ToArray();
    }

    public override ValueTask<string> Solve_1() => new(CountStones(_input, Part1Blinks).ToString());

    public override ValueTask<string> Solve_2() => new(CountStones(_input, Part2Blinks).ToString());

    private static long CountStones(long[] startingStones, int blinks)
    {
        var memory = new Dictionary<(long number, int blinks), long>();
        return startingStones.Sum(l => ProcessStone(l, blinks - 1, memory));
    }

    private static long ProcessStone(long stone, int blinks, IDictionary<(long number, int blinks), long> memory)
    {
        var stones = ApplyRules(stone);

        if (blinks == 0) return stones.Length;

        if (memory.TryGetValue((stone, blinks), out var value)) return value;

        var sum = 0L;
        foreach (var newStone in stones)
        {
            sum += ProcessStone(newStone, blinks - 1, memory);
        }

        memory[(stone, blinks)] = sum;

        return sum;
    }

    private static long[] ApplyRules(long stone) =>
        stone switch
        {
            0 => new[] { 1L },
            _ => stone.ToString().Length % 2 == 0
                ? new[]
                {
                    long.Parse(stone.ToString().AsSpan(0, stone.ToString().Length / 2)),
                    long.Parse(stone.ToString().AsSpan(stone.ToString().Length / 2))
                }
                : new[] { stone * 2024 }
        };
}
