using System;
using System.Linq;

namespace AdventOfCode
{
    public class Day_13 : BaseDay
    {
        public Day_13()
        {
        }

        public Day_13(string input) : base(input)
        {
        }

        public override string Solve_1()
        {
            var splittedInput = _input.SplitNewLine();
            var startTime = splittedInput[0].AsInt();
            var busIds = splittedInput[1].Split(',').Where(x => x != "x").AsInt();

            var result = busIds.Select(x => (WaitTime: x - (startTime % x), busIds: x)).OrderBy(x => x.WaitTime).FirstOrDefault();
            return (result.busIds * result.WaitTime).ToString();
        }

        public override string Solve_2()
        {
            throw new NotImplementedException();
        }
    }
}