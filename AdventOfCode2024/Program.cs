using AoCHelper;

if (args.Length == 0)
{
    await Solver.SolveLast();
}
else
{
    if (args[0] == "all")
    {
        await Solver.SolveAll();
    }
    else
    {
        await Solver.Solve(args[1..].Select(s => Convert.ToUInt32(s)).ToArray());
    }
}
