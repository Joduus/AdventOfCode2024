using System.Numerics;
using AoCHelper;

namespace AdventOfCode2024.Days;

public sealed class Day06 : BaseDay
{
    private readonly MapTileType[][] _map;
    private readonly Vector2 _startingPosition;

    public Day06()
    {
        var fileLines = File.ReadLines(InputFilePath).ToArray();

        _map = fileLines
            .Select(s => s.Select(c => c switch
            {
                '.' => MapTileType.EmptyRoom,
                '#' => MapTileType.Obstacle,
                '^' => MapTileType.Guard,
                _ => throw new ArgumentOutOfRangeException()
            }).ToArray())
            .ToArray();

        _startingPosition = _map
            .Select((row, y) => (row, y))
            .SelectMany(t => t.row.Select((tile, x) => (tile, x, t.y)))
            .Where(t => t.tile == MapTileType.Guard)
            .Select(t => new Vector2(t.x, t.y))
            .Single();
    }

    public override ValueTask<string> Solve_1()
    {
        var guardPosition = _startingPosition;
        var guardDirection = -Vector2.UnitY;

        var visitedFields = new HashSet<Vector2>();
        while (IsInMap(guardPosition))
        {
            visitedFields.Add(guardPosition);
            if (ObstacleInFront(guardPosition, guardDirection))
            {
                RotateDirection(ref guardDirection, 90);
            }
            else
            {
                MoveForward(ref guardPosition, guardDirection);
            }
        }

        return new ValueTask<string>(visitedFields.Count.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        throw new NotImplementedException();
    }

    private bool IsInMap(Vector2 position)
    {
        return position.X >= 0 && position.X < _map[0].Length && position.Y >= 0 && position.Y < _map.Length;
    }

    private bool ObstacleInFront(Vector2 currentPosition, Vector2 direction)
    {
        var nextPosition = currentPosition + direction;
        if (!IsInMap(nextPosition))
        {
            return false;
        }

        return _map[(int)nextPosition.Y][(int)nextPosition.X] == MapTileType.Obstacle;
    }

    private void RotateDirection(ref Vector2 direction, int degrees)
    {
        var radians = float.DegreesToRadians(degrees);
        direction = Vector2.Transform(direction, Matrix3x2.CreateRotation(radians));
    }

    private void MoveForward(ref Vector2 position, Vector2 direction)
    {
        position += direction;
    }
}

public enum MapTileType
{
    EmptyRoom,
    Obstacle,
    Guard,
}
