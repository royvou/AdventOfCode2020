using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class StringExtensions
    {
        public static IEnumerable<int> ParseAsArray(this string input)
            => input.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x));

    }
}