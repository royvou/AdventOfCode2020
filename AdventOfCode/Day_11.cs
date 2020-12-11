using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AoCHelper;
using Microsoft.VisualBasic;

namespace AdventOfCode
{
    public class Day_11 : BaseDay
    {
        private string _input;

        public Day_11()
        {
            //.Replace(".txt", ".sample.txt")
            _input = File.ReadAllText(InputFilePath);
        }

        public override string Solve_1()
        {
            try
            {
                var map = _input.Trim().SplitNewLine().Select(x => x.ToCharArray()).ToArray();
                var currentMap = map;

                var loop = 0l;
                var hasCHanges = false;
                do
                {
                    loop += 1;
                    hasCHanges = CalculateNextMap(currentMap, out var newMap);
                    currentMap = newMap;
                } while (hasCHanges);

                return currentMap.SelectMany(x => x).Count(x => x == '#').ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return "";
        }

        public bool CalculateNextMap(char[][] map, out char[][] newMap)
        {
            newMap = CreateMap(map.Length, map[0].Length);
            var hasCHanges = false;
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    switch (map[y][x])
                    {
                        case 'L':
                            if (NumberSurroundings(map, x, y) == 0)
                            {
                                newMap[y][x] = '#';
                                hasCHanges = true;
                                break;
                            }

                            newMap[y][x] = map[y][x];
                            break;
                        case '#':
                            if (NumberSurroundings(map, x, y) >= 4)
                            {
                                newMap[y][x] = 'L';
                                hasCHanges = true;
                                break;
                            }

                            newMap[y][x] = map[y][x];
                            break;
                        default:
                            newMap[y][x] = map[y][x];

                            break;
                    }
                }
            }


            return hasCHanges;
        }

        private char[][] CreateMap(in int mapLength, in int length)
        {
            var result = new char[mapLength][];
            for (int i = 0; i < mapLength; i++)
            {
                result[i] = new char[length];
            }

            return result;
        }

        private int NumberSurroundings(in char[][] map, in int x, in int y)
        {
            int result = 0;
            for (int yy = y - 1; yy <= y + 1; yy++)
            {
                for (int xx = x - 1; xx <= x + 1; xx++)
                {
                    if (
                        !(xx == x && yy == y) // Skip Index of self
                        && xx >= 0
                        && yy >= 0
                        && xx < map[0].Length
                        && yy < map.Length
                        && map[yy][xx] == '#'
                    )
                    {
                        result += 1;
                    }
                }
            }

            return result;
        }

        public override string Solve_2()
        {
            throw new NotImplementedException();
        }
    }
}