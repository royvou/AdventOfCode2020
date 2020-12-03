using System;
using System.IO;
using System.Linq;
using AoCHelper;

namespace AdventOfCode
{
    public class Day_03 : BaseDay
    {
        private string _input;

        private char[][] _map;
        public Day_03()
        {
            _input = File.ReadAllText(InputFilePath);
            _map = _input.SplitNewLine().Select(x => x.ToCharArray()).ToArray();
        }

        public override string Solve_1()
        {
            var trees = CalculateTreeEncounters(3,1);

            return trees.ToString();
        }

        private int CalculateTreeEncounters(int xPlus, int yPlus)
        {
            int trees = 0;
          
            int x = 0;
            for (int y = 0; y < _map.Length; y += yPlus)
            {
                if (_map[y][x % _map[y].Length] == '#')
                {
                    trees += 1;
                }

                ;
                x += xPlus;
            }

            return trees;
        }

        public override string Solve_2()
        {
            var slopes = new[]
            {
                (1, 1),
                (3, 1),
                (5, 1),
                (7, 1),
                (1, 2)
            };

            return slopes.Select(x => CalculateTreeEncounters(x.Item1, x.Item2)).Aggregate((x, y) => x * y).ToString();
        }
    }
}