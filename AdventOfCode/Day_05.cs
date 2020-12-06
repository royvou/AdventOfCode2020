using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode
{
    public class Day_05 : BaseDay
    {
        private string _input;

        public Day_05()
        {
            _input = File.ReadAllText(InputFilePath);
        }

        public override string Solve_1()
        {
            return _input.SplitNewLine().Select(x => GetSeatId(x)).Max().ToString();
        }

        public int GetSeatId(string info)
        {
            var rowInfo = info.Substring(0, 7);
            var row = GetRow(rowInfo);
            var columnInfo = info.Substring(7);
            var column = GetColumn(columnInfo);

            return (row * 8) + column;
        }
        
        public int GetColumn(string columnInfo)
        {
            int min = 0;
            int max = 7;
            foreach (var @char in columnInfo)
            {
                if (@char == 'L')
                {
                    max = ((min + max) / 2);
                }
                else if (@char == 'R')
                {
                    min = ((min + max) / 2) + 1;
                }
            }

            return min;
        }

        public int GetRow(string rowInfo)
        {
            int min = 0;
            int max = 127;
            foreach (var @char in rowInfo)
            {
                if (@char == 'F')
                {
                    max = ((min + max) / 2);
                }
                else if (@char == 'B')
                {
                    min = ((min + max) / 2) + 1;
                }
            }

            return min; 
        }

        public override string Solve_2()
        {
            var seatIds = _input.SplitNewLine().Select(x => GetSeatId(x)).OrderBy(x => x).ToList();

            for (int i = 0; i < seatIds.Count; i++)
            {
                var currentSeat = seatIds[i];

                if (seatIds[i] + 1 != seatIds[i + 1])
                {
                    return (seatIds[i] + 1).ToString();
                }
            }

            return null;
        }
    }
}