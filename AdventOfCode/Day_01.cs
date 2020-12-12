using AoCHelper;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day_01 : BaseDay
    {
        public Day_01()
        {
        }
        
        public override string Solve_1()
        { var inputNumbers = _input.ParseAsArray().OrderBy(x => x).ToList();

            for (int i = 0; i < inputNumbers.Count; i++)
            {
                for (int y = inputNumbers.Count - 1; y > 0; y--)
                {
                    var inputNumberY = inputNumbers[y];
                    if (inputNumberY > 2020)
                    {
                        continue;
                    }


                    var inputNumberX = inputNumbers[i];
                    if (inputNumberX + inputNumberY == 2020)
                    {
                        return (inputNumberX * inputNumberY).ToString();
                    }
                }
            }

            return string.Empty;
        }


        public string Solve_1_Linq()
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