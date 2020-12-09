using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class StringExtensions
    {
        public static IEnumerable<int> ParseAsArray(this string input)
            => input.SplitNewLine().Select(x => int.Parse(x));

        public static IEnumerable<long> ParseAsLongArray(this string input)
            => input.SplitNewLine().Select(x => long.Parse(x));
        
        public static string[] SplitSpace(this string input)
            => input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        public static string[] SplitNewLine(this string input)
            => input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        private static readonly string doubleNewLine = Environment.NewLine + Environment.NewLine;
        public static string[] SplitDoubleNewLine(this string input)
            => input.Split(doubleNewLine, StringSplitOptions.RemoveEmptyEntries);
    }
}