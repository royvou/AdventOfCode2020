using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode
{
    public class Day_10 : BaseDay
    {
        private string _input;

        public Day_10()
        {
            _input = File.ReadAllText(InputFilePath);
        }

        public override string Solve_1()
        {
            var numbers = _input.ParseAsArray();
            var result = numbers.OrderBy(x => x).Append(numbers.Max() + 3).Prepend(0).Aggregate(
                (acc: 0,dif1: 0,dif3: 0),
                (acc, next) =>
                {
                    return (next -acc.acc ) switch
                    {
                        3 => (next, acc.dif1 , acc.dif3 +1),
                        1 => (next, acc.dif1 + 1, acc.dif3),
                        _ => (next, acc.dif1, acc.dif3),
                    };
                    
                });

            return (result.dif1 * result.dif3).ToString();
        }

        public override string Solve_2()
        {
            throw new NotImplementedException();
        }
    }
}