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
        
        //FUN FACT: This is the Chinese remainder theorem :) 
        public override string Solve_2()
        {
            var splittedInput = _input.SplitNewLine();
            var busIds = splittedInput.Last().
                Split(',').
                Select((x, index) => new Day13Bus(int.TryParse(x, out var parsedX) ? parsedX : -1, index, x == "x")).Where(x => !x.IsIgnored).
                OrderBy(x => x.Id).
                ToList();

            //Must be longs as the time exeeds int.MaxValue :)
            //We lock in 1 bus at a time
            long time = busIds[0].Id;
            long increment = 1;
            for(int bussesCompleted = 0 ; 
                bussesCompleted < busIds.Count || time< 0; )
            {
                var bussesCorrect = busIds.TakeWhile(bus => (time + bus.Index) % bus.Id == 0).Count();
                
                if (bussesCorrect > bussesCompleted)
                {
                    bussesCompleted = bussesCorrect;
                    increment *= busIds[bussesCompleted -1 ].Id;
                }

                if (bussesCompleted == busIds.Count)
                {
                    return time.ToString();
                }

                time += increment;
            }

            return time.ToString();
        }
    }

    public record Day13Bus(int Id, int Index, bool IsIgnored);
}