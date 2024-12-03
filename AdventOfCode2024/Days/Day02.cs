using AoCHelper;

namespace AdventOfCode2024.Days;

public class Day02 : BaseDay
{
    private readonly int[][] _numberLines = new int[1000][];

    public Day02()
    {
        ParseInput();
    }

    public override ValueTask<string> Solve_1()
    {
        var safeCounter = 0;
        foreach (var numbers in _numberLines)
        {
            if (IsSafe(numbers))
            {
                safeCounter++;
            }
        }

        return new ValueTask<string>(safeCounter.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var safeCounter = 0;

        foreach (var numbers in _numberLines)
        {
            if (IsSafe(numbers) || WithProblemDampener(numbers))
            {
                safeCounter++;
            }
        }

        return new ValueTask<string>(safeCounter.ToString());
    }

    private void ParseInput()
    {
        var inputFile = File.Open(InputFilePath, FileMode.Open);
        var reader = new StreamReader(inputFile);

        var lineNumber = 0;
        for (var line = reader.ReadLine(); line != null; line = reader.ReadLine())
        {
            var numbers = line.Split(" ").Select(x => int.Parse(x)).ToArray();
            _numberLines[lineNumber] = numbers;

            lineNumber++;
        }
    }

    bool IsSafe(int[] numbers)
    {
        var isAscending = true;
        var isDescending = true;

        for (var i = 0; i < numbers.Length - 1; i++)
        {
            var diff = numbers[i + 1] - numbers[i];

            if (diff is < 1 or > 3) isAscending = false;
            if (diff is > -1 or < -3) isDescending = false;
        }

        return isAscending || isDescending;
    }

    bool WithProblemDampener(int[] numbers)
    {
        for (var i = 0; i < numbers.Length; i++)
        {
            var modifiedNumbers = numbers.Where((_, index) => index != i).ToArray();

            if (IsSafe(modifiedNumbers))
            {
                return true;
            }
        }

        return false;
    }
}
