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

            long orMask = 0L, andMask = 0L;
            foreach (var line in inputLines)
            {
                if (line.StartsWith("mask = "))
                {
                    (orMask, andMask) = ParseMask1(line.Substring("mask = ".Length));
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

        private (long orMask, long andMask) ParseMask1(string mask)
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

        private (long orMask, long[] xorMasks) ParseMask2(string mask)
        {
            long orMask = 0;
            var xorMasks = new long[MathUtilities.PowerOf2(mask.Count(x => x == 'X'))];
            var nextXorIndex = 0;

            for (var i = 0; i < mask.Length; i++)
            {
                var digit = mask[i];
                orMask <<= 1;
                switch (digit)
                {
                    case '1':
                        orMask |= 1;
                        break;
                    case '0':
                        break;
                    default:
                        var xOrMask = MathUtilities.PowerOf2(mask.Length - 1 - i);
                        if (nextXorIndex > 0)
                        {
                            Array.Copy(xorMasks, 0, xorMasks, nextXorIndex, nextXorIndex);
                            for (var xix = nextXorIndex; xix < nextXorIndex * 2; xix++)
                            {
                                xorMasks[xix] |= xOrMask;
                            }

                            nextXorIndex *= 2;
                        }
                        else
                        {
                            xorMasks[1] = xOrMask;
                            nextXorIndex = 2;
                        }

                        break;
                }
            }

            return (orMask, xorMasks);
        }

        public override string Solve_2()
        {
            var memory = new Dictionary<long, long>();
            var inputLines = _input.SplitNewLine();

            long orMask = 0L;
            long[] andMasks = null;
            foreach (var line in inputLines)
            {
                if (line.StartsWith("mask = "))
                {
                    (orMask, andMasks) = ParseMask2(line.Substring("mask = ".Length));
                }
                else
                {
                    var matches = Regex.Match(line, @"mem\[(\d*)\] = (\d*)");
                    var memoryLocation = int.Parse(matches.Groups[1].Value);
                    var memoryValue = long.Parse(matches.Groups[2].Value);

                    foreach (var andMask in andMasks)
                    {
                        memory[((memoryLocation | orMask) ^ andMask)] = memoryValue;
                    }
                }
            }

            return memory.Values.Sum().ToString();
        }
    }
}