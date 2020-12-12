using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode
{
    public class Day_10 : BaseDay
    {
        public Day_10()
        {
        }

        public override string Solve_1()
        {
            var numbers = _input.ParseAsArray();
            var result = numbers.OrderBy(x => x).Append(numbers.Max() + 3).Prepend(0).Aggregate(
                (acc: 0, dif1: 0, dif3: 0),
                (acc, next) =>
                {
                    return (next - acc.acc) switch
                    {
                        3 => (next, acc.dif1, acc.dif3 + 1),
                        1 => (next, acc.dif1 + 1, acc.dif3),
                        _ => (next, acc.dif1, acc.dif3),
                    };
                });

            return (result.dif1 * result.dif3).ToString();
        }

        public override string Solve_2()
        {
            var numbers = _input.ParseAsArray();
            var result = numbers.OrderBy(x => x).Append(numbers.Max() + 3).Prepend(0).ToList();

            return NumberConnections(result).ToString();
        }

        private long NumberConnections(List<int> joltages)
        {
            var cache = new Dictionary<int, long>();
            cache[joltages.Count - 1] = 1;

            for (var k = joltages.Count - 2; k >= 0; k--)
            {
                long currentCount = 0;
                for (var connected = k + 1; connected < joltages.Count && joltages[connected] - joltages[k] <= 3; connected++)
                {
                    currentCount += cache[connected];
                }

                cache[k] = currentCount;
            }

            return cache[0];
        }
    }
}