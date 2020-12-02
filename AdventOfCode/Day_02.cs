using System;
using System.IO;
using System.Linq;
using AoCHelper;

namespace AdventOfCode
{
    public class Day_02 : BaseDay
    {
        private string _input;

        public Day_02()
        {
            _input = File.ReadAllText(InputFilePath);
        }

        public override string Solve_1()
        {
            var result = _input.SplitNewLine().Select(x =>
            {
                var item = x.SplitSpace();
                var itemRnage = item[0].Split("-");

                return (Min: int.Parse(itemRnage[0]), Max: int.Parse(itemRnage[1]), Char: item[1].TrimEnd(':')[0], Password: item[2]);
            }).Where(x =>
            {
                var count = x.Password.Count(y => y == x.Char);
                return Utilities.Between(count, x.Min, x.Max);
            }).Count().ToString();

            return result;
        }

        public override string Solve_2()
        {
            var result = _input.SplitNewLine().Select(x =>
            {
                var item = x.SplitSpace();
                var itemRnage = item[0].Split("-");

                return (Min: int.Parse(itemRnage[0]), Max: int.Parse(itemRnage[1]), Char: item[1].TrimEnd(':')[0], Password: item[2]);
            }).Where(x =>
            {
                return x.Password.Length >= x.Max && (Utilities.CharAt(x.Password, x.Char, x.Min) ^ Utilities.CharAt(x.Password, x.Char, x.Max));

            }).Count().ToString();

            return result;
        }
    }
}