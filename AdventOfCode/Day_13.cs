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
            var splittedInput = _input.SplitNewLine();
            var busIds = splittedInput.Last().Split(',').Select((x,index) => new Day13Bus(int.TryParse(x, out var parsedX) ? parsedX : -1,  index, x == "x")).Where(x => !x.IsIgnored).ToList();
            
            var bus1 = busIds[0];
            long time = 0;
            for (;; time += bus1.Id)
            {
                if (busIds.All(bus => (time + bus.Index) % bus.Id ==0))
                {
                    return time.ToString();
                }
                
            }
        }
    }

    public record Day13Bus(int Id, int Index, bool IsIgnored);
}