using System;
using System.IO;
using System.Linq;
using AoCHelper;

namespace AdventOfCode
{
    public class Day_03 : BaseDay
    {
        private string _input;

        public Day_03()
        {
            _input = File.ReadAllText(InputFilePath);
        }

        public override string Solve_1()
        {
            int trees = 0;
            var map = _input.SplitNewLine().Select(x => x.ToCharArray()).ToArray();
            
            int x = 0;
            for (int y = 0; y < map.Length; y++)
            {
                if (map[y][x % map[y].Length] == '#')
                {
                    trees += 1;
                } ;
                x += 3;
            }

            return trees.ToString();
        }

        public override string Solve_2()
        {
            return null;
        }
    }
}