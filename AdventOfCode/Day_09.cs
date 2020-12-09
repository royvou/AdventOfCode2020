using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode
{
    public class Day_09 : BaseDay
    {
        private string _input;

        public Day_09()
        {
            _input = File.ReadAllText(InputFilePath);
        }

        public override string Solve_1()
        {
            var list = _input.TrimEnd().ParseAsLongArray().ToArray();
            
            var result = GetInvalidNumbers(list, 25);
            return result.FirstOrDefault().ToString();
        }

        private IEnumerable<long> GetInvalidNumbers(IList<long> list, int preamble)
        {
            var validNumbers = new Queue<long>();
            for (int i = 0; i < preamble - 1; i++)
            {
                for (int y = 1; y < preamble; y++)
                {
                    validNumbers.Enqueue(list[i] + list[y]);
                }
            }
            
            
            for (int i = preamble; i < list.Count; i++)
            {
                var number = list[i];

                if (validNumbers.Contains(number))
                {
                    //Ok
                }
                else
                {
                    yield return number;
                }

                Enumerable.Range(0, preamble).Select(x => validNumbers.TryDequeue(out var _)).ToList();
                Enumerable.Range(1, preamble).Reverse().Select(x =>
                {
                    validNumbers.Enqueue(list[i] + list[i - x]);
                    return x;
                }).ToList();
            }
        }

        public override string Solve_2()
        {
            throw new NotImplementedException();
        }
    }
}