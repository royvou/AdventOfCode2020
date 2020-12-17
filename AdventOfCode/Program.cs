using AoCHelper;

var config = new SolverConfiguration()
{
    ShowConstructorElapsedTime = true,
    ShowTotalElapsedTimePerDay = true,
    ShowOverallResults = true,
};
#if DEBUG
Solver.SolveLast(config);
#else
Solver.SolveAll(config);
#endif