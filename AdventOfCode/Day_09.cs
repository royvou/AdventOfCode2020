﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode
{
    public class Day_09 : BaseDay
    {
        public Day_09()
        {
        }

        public override string Solve_1()
        {
            var list = _input.TrimEnd().ParseAsLongArray().ToArray();

            var result = GetInvalidNumbers(list, 25);
            return result.FirstOrDefault().ToString();
        }

        private IEnumerable<long> GetInvalidNumbers(IList<long> list, int preamble)
        {
            // Precalculated queue with all valid numbers
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
            var list = _input.TrimEnd().ParseAsLongArray().ToArray();

            var result = GetInvalidNumbers(list, 25);
            var invalidNumber = result.FirstOrDefault();

            var result2 = GetNumbersWithSumGroupSized(list, invalidNumber);
            return (result2.Min() + result2.Max()).ToString();
        }

        private IEnumerable<long> GetNumbersWithSumGroupSized(long[] list, long invalidNumber, int groupSize = 2)
        {
            return Enumerable.Range(2, 100).Select(x => GetNumbersWithSum(list, invalidNumber, x)).FirstOrDefault(x => x != null);
        }

        private IEnumerable<long> GetNumbersWithSum(long[] list, in long invalidNumber, int groupSize = 2)
        {
            long sum = list.Take(groupSize).Sum();

            for (int i = groupSize; i < list.Length; i++)
            {
                if (sum == invalidNumber)
                {
                    return list.Skip(i - groupSize).Take(groupSize);
                }


                sum += list[i];
                sum -= list[i - groupSize];
            }

            return null;
        }
    }
}