using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode
{
    public class Day_06 : BaseDay
    {
        public Day_06()
        {
        }

        public override string Solve_1()
        {
            return _input.SplitDoubleNewLine().Select(x => x.Replace(Environment.NewLine, string.Empty).Distinct().Count()).Sum().ToString();
        }

        public override string Solve_2()
        {
            return _input.SplitDoubleNewLine().Select(x =>
            {
                var a = x.SplitNewLine();
                return x.Replace(Environment.NewLine, string.Empty).GroupBy(y => y).Count(y => y.Count() == a.Length);
            }).Sum().ToString();
        }
    }
}