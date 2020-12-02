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

        public static string[] SplitSpace(this string input)
            => input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        public static string[] SplitNewLine(this string input)
            => input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

    }
}