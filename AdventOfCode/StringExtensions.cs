using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class StringExtensions
    {

        public static int AsInt(this string input)
            => int.Parse(input);
        public static IEnumerable<int> AsInt(this IEnumerable<string> input)
            => input.Select(x => int.Parse(x));
        
        public static IEnumerable<int> ParseAsArray(this string input)
            => input.SplitNewLine().AsInt();

        public static IEnumerable<long> ParseAsLongArray(this string input)
            => input.SplitNewLine().Select(x => long.Parse(x));

        public static string[] SplitSpace(this string input)
            => input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        public static string[] SplitNewLine(this string input)
            => input.Split(new[]
            {
                Environment.NewLine,
                "\n" //Used as newline in unit tests
            }, StringSplitOptions.RemoveEmptyEntries);

        private static readonly string doubleNewLine = Environment.NewLine + Environment.NewLine;

        public static string[] SplitDoubleNewLine(this string input)
            => input.Split(doubleNewLine, StringSplitOptions.RemoveEmptyEntries);
    }
}