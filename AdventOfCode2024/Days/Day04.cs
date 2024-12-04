using AoCHelper;

namespace AdventOfCode2024.Days;

public sealed class Day04 : BaseDay
{
    private const string XmasWord = "XMAS";

    private readonly char[][] _input;

    public Day04()
    {
        _input = File.ReadLines(InputFilePath).Select(x => x.ToCharArray()).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        var xmasCounter = 0;

        var rowCount = _input.Length;
        for (var i = 0; i < rowCount; i++)
        {
            var columnCount = _input[i].Length;
            for (var j = 0; j < columnCount; j++)
            {
                var currentColumnIsX = _input[i][j] == 'X';
                if (!currentColumnIsX)
                {
                    continue;
                }

                xmasCounter += CheckAllDirectionsForWord(XmasWord, i, j, rowCount, columnCount);
            }
        }

        return new ValueTask<string>(xmasCounter.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        throw new NotImplementedException();
    }

    private int CheckAllDirectionsForWord(ReadOnlySpan<char> word, int currentRow, int currentColumn, int rowCount, int columnCount)
    {
        var wordLength = word.Length - 1;
        var hasWordSpaceUp = currentRow - wordLength >= 0;
        var hasWordSpaceDown = currentRow + wordLength < rowCount;
        var hasWordSpaceLeft = currentColumn - wordLength >= 0;
        var hasWordSpaceRight = currentColumn + wordLength < columnCount;

        var directionConditions = new Dictionary<Direction, Func<bool>>
        {
            {Direction.Up, () => hasWordSpaceUp},
            {Direction.Down, () => hasWordSpaceDown},
            {Direction.Left, () => hasWordSpaceLeft},
            {Direction.Right, () => hasWordSpaceRight},
            {Direction.UpperRight, () => hasWordSpaceUp && hasWordSpaceRight},
            {Direction.LowerRight, () => hasWordSpaceDown && hasWordSpaceRight},
            {Direction.LowerLeft, () => hasWordSpaceDown && hasWordSpaceLeft},
            {Direction.UpperLeft, () => hasWordSpaceUp && hasWordSpaceLeft},
        };

        var directionsWithWord = 0;

        foreach (var condition in directionConditions)
        {
            if (condition.Value() && CheckDirectionForWord(word, currentRow, currentColumn, condition.Key))
            {
                directionsWithWord++;
            }
        }

        return directionsWithWord;
    }

    private bool CheckDirectionForWord(ReadOnlySpan<char> word, int currentRow, int currentColumn,
        Direction directionToCheck)
    {
        var directionVectors = new Dictionary<Direction, (int row, int column)>
        {
            {Direction.Up, (-1, 0)},
            {Direction.Down, (1, 0)},
            {Direction.Left, (0, -1)},
            {Direction.Right, (0, 1)},
            {Direction.UpperRight, (-1, 1)},
            {Direction.LowerRight, (1, 1)},
            {Direction.LowerLeft, (1, -1)},
            {Direction.UpperLeft, (-1, -1)},
        };

        var (row, column) = directionVectors[directionToCheck];
        var wordLength = word.Length;
        Span<char> chars = stackalloc char[wordLength];
        for (var i = 0; i < wordLength; i++)
        {
            chars[i] = _input[currentRow + row * i][currentColumn + column * i];
        }

        return chars.SequenceEqual(word);
    }
}

[Flags]
enum Direction
{
    NoDirection = 0b00000000,
    Up = 0b00000001,
    Down = 0b00000010,
    Left = 0b00000100,
    Right = 0b00001000,
    UpperRight = 0b00010000,
    LowerRight = 0b00100000,
    LowerLeft = 0b01000000,
    UpperLeft = 0b10000000,
}
