using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_14 : BaseDay
    {
        public Day_14()
        {
        }

        public Day_14(string input) : base(input)
        {
        }

        public override string Solve_1()
        {
            var memory = new Dictionary<int, long>();
            var inputLines = _input.SplitNewLine();

            long orMask = 0l, andMask = 0l;
            foreach (var line in inputLines)
            {
                if (line.StartsWith("mask = "))
                {
                    (orMask, andMask) = ParseMask(line.Substring("mask = ".Length));
                }
                else
                {
                    var matches = Regex.Match(line, @"mem\[(\d*)\] = (\d*)");
                    var memoryLocation = int.Parse(matches.Groups[1].Value);
                    var memoryValue = long.Parse(matches.Groups[2].Value);

                    memory[memoryLocation] = (memoryValue | orMask) & andMask;
                }
            }

            return memory.Values.Sum().ToString();
        }

        private (long orMask, long andMask) ParseMask(string mask)
        {
            long orMask = 0, andMask = 0;
            foreach (var digit in mask)
            {
                orMask <<= 1;
                andMask <<= 1;
                switch (digit)
                {
                    case '1':
                        orMask |= 1;
                        andMask |= 1;
                        break;
                    case '0':
                        orMask |= 1;
                        break;
                    default:
                        andMask |= 1;
                        break;
                }
            }

            return (orMask, andMask);
        }

        public override string Solve_2()
        {
            throw new NotImplementedException();
        }
    }
}