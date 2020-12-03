using AoCHelper;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day_01 : BaseDay
    {
        private readonly string _input;

        public Day_01()
        {
            _input = File.ReadAllText(InputFilePath);
        }

        public override string Solve_1()
        {
            var inputNumbers = _input.ParseAsArray();

            var result = from a in inputNumbers
                where a < 2020
                from b in inputNumbers
                where b < 2020
                where a + b == 2020
                select a * b;

            return result.FirstOrDefault().ToString();
        }

        public override string Solve_2()
        {
            var inputNumbers = _input.ParseAsArray();

            var result =
                from a in inputNumbers
                from b in inputNumbers
                where a + b <= 2020
                from c in inputNumbers
                where a + b + c == 2020
                select a * b * c;

            return result.FirstOrDefault().ToString();
        }
    }
}