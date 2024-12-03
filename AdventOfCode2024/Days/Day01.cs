using AoCHelper;

namespace AdventOfCode2024.Days;

public class Day01 : BaseDay
{
    private readonly List<int> _leftList = [];
    private readonly List<int> _rightList = [];

    public Day01()
    {
        ParseInput();
    }

    public override ValueTask<string> Solve_1()
    {
        var totalDistance = 0;
        for (var i = 0; i < _leftList.Count; i++)
        {
            var distance = Math.Abs(_rightList[i] - _leftList[i]);
            totalDistance += distance;
        }

        return new ValueTask<string>(totalDistance.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var duplicatesList = _leftList.Intersect(_rightList).ToList();

        var totalSimilarity = 0;
        for (var i = 0; i < duplicatesList.Count; i++)
        {
            var duplicateNumber = duplicatesList[i];
            var duplicateNumberCount = _rightList.FindAll(x => x == duplicateNumber).Count;
            totalSimilarity += duplicateNumber * duplicateNumberCount;
        }

        return new ValueTask<string>(totalSimilarity.ToString());
    }

    private void ParseInput()
    {
        var inputFile = File.Open(InputFilePath, FileMode.Open);
        var reader = new StreamReader(inputFile);

        for (var line = reader.ReadLine(); line != null; line = reader.ReadLine())
        {
            var list1Number = line.AsSpan(0, 5);
            var list2Number = line.AsSpan(8, 5);

            _leftList.Add(int.Parse(list1Number));
            _rightList.Add(int.Parse(list2Number));
        }

        _leftList.Sort();
        _rightList.Sort();
    }
}
