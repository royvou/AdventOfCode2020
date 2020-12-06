using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode
{
    public class Day_06 : BaseDay
    {
        private string _input;

        public Day_06()
        {
            _input = File.ReadAllText(InputFilePath);
        }

        public override string Solve_1()
        {
            return _input.Split(Environment.NewLine + Environment.NewLine).Select(x =>
            {
                return x.Replace(Environment.NewLine, "").Distinct().Count();
            }).Sum().ToString();
        }

        public override string Solve_2()
        {
            return _input.Split(Environment.NewLine + Environment.NewLine).Select(x =>
            {
                var a = x.SplitNewLine();

                return x.Replace(Environment.NewLine, "").GroupBy(y => y).Where(y => y.Count() == a.Count()).Count();
            }).Sum().ToString();
        }
    }
}