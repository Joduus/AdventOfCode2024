using AoCHelper;

namespace AdventOfCode2024.Days;

public sealed class Day05 : BaseDay
{
    private readonly ILookup<int, int> _rulesLookup;
    private readonly int[][] _updates;

    public Day05()
    {
        var fileLines = File.ReadLines(InputFilePath).ToArray();

        _rulesLookup = fileLines
            .TakeWhile(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => s.Split('|').Select(int.Parse).ToArray())
            .Select(ints => Tuple.Create(ints[0], ints[1]))
            .ToLookup(rule => rule.Item1, rule => rule.Item2);

        _updates = fileLines
            .SkipWhile(s => !string.IsNullOrWhiteSpace(s))
            .Skip(1)
            .Select(s => s.Split(',').Select(int.Parse).ToArray())
            .ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        var middlePageSum = _updates
            .Where(IsUpdateValid)
            .Sum(GetMiddleOfArray);

        return new ValueTask<string>(middlePageSum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var middlePageSum = _updates
            .Where(updatesArr => !IsUpdateValid(updatesArr))
            .Select(updatesArr =>
            {
                var relevantRules = GetRelevantRules(updatesArr)
                    .ToLookup(tuple => tuple.before, tuple => tuple.after);

                var sortedPage = TopologicalSort(updatesArr, update => relevantRules[update]).ToArray();

                return sortedPage;
            })
            .Sum(GetMiddleOfArray);

        return new ValueTask<string>(middlePageSum.ToString());
    }

    // https://en.wikipedia.org/wiki/Topological_sorting#Depth-first_search
    private static List<T> TopologicalSort<T>(IEnumerable<T> nodes, Func<T, IEnumerable<T>> children)
    {
        HashSet<T> unmarked = nodes.ToHashSet();
        HashSet<T> permanentMark = new();
        HashSet<T> temporaryMark = new();
        List<T> result = new();

        void Visit(T n)
        {
            if (permanentMark.Contains(n))
            {
                return;
            }
            if (temporaryMark.Contains(n))
            {
                throw new Exception("Graph has cycle");
            }
            temporaryMark.Add(n);
            foreach (var m in children(n))
            {
                Visit(m);
            }
            temporaryMark.Remove(n);
            unmarked.Remove(n);
            permanentMark.Add(n);
            result.Add(n);
        }

        while (unmarked.Any())
        {
            var pick = unmarked.First();
            Visit(pick);
        }
        result.Reverse();
        return result;
    }

    private bool IsUpdateValid(int[] updatesArr)
    {
        return GetRelevantRules(updatesArr)
            .All(rule => IsRuleValid(updatesArr, rule.before, rule.after));
    }

    private IEnumerable<(int before, int after)> GetRelevantRules(int[] updatesArr)
    {
        return updatesArr.SelectMany(update => _rulesLookup[update], (before, after) => (before, after))
            .Where(result => updatesArr.Contains(result.after));
    }

    private bool IsRuleValid(int[] updatesArr, int ruleBefore, int ruleAfter)
    {
        return Array.IndexOf(updatesArr, ruleBefore) < Array.IndexOf(updatesArr, ruleAfter);
    }

    private int GetMiddleOfArray(int[] arr)
    {
        return arr[arr.Length / 2];
    }
}
