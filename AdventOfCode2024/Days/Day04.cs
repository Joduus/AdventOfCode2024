using AoCHelper;

namespace AdventOfCode2024.Days;

public sealed class Day04 : BaseDay
{
    private const string XmasWord = "XMAS";

    private readonly char[][] _input;

    private readonly Dictionary<Direction, (int x, int y)> _directionVectors = new()
    {
        { Direction.Up, (0, -1) },
        { Direction.Down, (0, 1) },
        { Direction.Left, (-1, 0) },
        { Direction.Right, (1, 0) },
        { Direction.UpperRight, (1, -1) },
        { Direction.LowerRight, (1, 1) },
        { Direction.LowerLeft, (-1, 1) },
        { Direction.UpperLeft, (-1, -1) },
    };

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
        var xmasCounter = 0;

        var rowCount = _input.Length;
        for (var i = 0; i < rowCount; i++)
        {
            var columnCount = _input[i].Length;
            for (var j = 0; j < columnCount; j++)
            {
                var currentColumnIsA = _input[i][j] == 'A';
                if (!currentColumnIsA)
                {
                    continue;
                }

                var xmasCounterTemp = IsXmas(i, j, rowCount, columnCount);
                xmasCounter += Convert.ToInt32(xmasCounterTemp);
            }
        }

        return new ValueTask<string>(xmasCounter.ToString());
    }

    private bool IsXmas(int currentRow, int currentColumn, int rowCount, int columnCount)
    {
        if (!CanMoveInDirection(currentRow, currentColumn, Direction.Up, 1)
            || !CanMoveInDirection(currentRow, currentColumn, Direction.Down, 1)
            || !CanMoveInDirection(currentRow, currentColumn, Direction.Left, 1)
            || !CanMoveInDirection(currentRow, currentColumn, Direction.Right, 1)
           )
        {
            return false;
        }

        var upperLeft = _input[currentRow - 1][currentColumn - 1];
        var upperRight = _input[currentRow - 1][currentColumn + 1];
        var lowerLeft = _input[currentRow + 1][currentColumn - 1];
        var lowerRight = _input[currentRow + 1][currentColumn + 1];

        var upperLeftLowerRightX = (upperLeft == 'M' && lowerRight == 'S') || (upperLeft == 'S' && lowerRight == 'M');
        var upperRightLowerLeftX = (upperRight == 'M' && lowerLeft == 'S') || (upperRight == 'S' && lowerLeft == 'M');

        return upperLeftLowerRightX && upperRightLowerLeftX;
    }

    private int CheckAllDirectionsForWord(ReadOnlySpan<char> word, int currentRow, int currentColumn, int rowCount,
        int columnCount)
    {
        var wordLength = word.Length - 1;
        var directionsWithWord = 0;
        foreach (var direction in _directionVectors)
        {
            if (CanMoveInDirection(currentRow, currentColumn, direction.Key, wordLength)
                && CheckDirectionForWord(word, currentRow, currentColumn, direction.Key))
            {
                directionsWithWord++;
            }
        }

        return directionsWithWord;
    }

    private bool CanMoveInDirection(int currentRow, int currentColumn, Direction direction, int length)
    {
        var (x, y) = _directionVectors[direction];
        var newRow = currentRow + y * length;
        var newColumn = currentColumn + x * length;
        return newRow >= 0 && newRow < _input.Length && newColumn >= 0 && newColumn < _input[newRow].Length;
    }

    private bool CheckDirectionForWord(ReadOnlySpan<char> word, int currentRow, int currentColumn,
        Direction directionToCheck)
    {
        var (x, y) = _directionVectors[directionToCheck];
        var wordLength = word.Length;
        Span<char> chars = stackalloc char[wordLength];
        for (var i = 0; i < wordLength; i++)
        {
            chars[i] = _input[currentRow + y * i][currentColumn + x * i];
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
