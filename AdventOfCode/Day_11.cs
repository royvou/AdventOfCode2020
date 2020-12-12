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

        public bool CalculateNextMap(char[][] map, out char[][] newMap)
        {
            newMap = DuplicateMap(map.Length, map[0].Length);
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

        private char[][] DuplicateMap(in int mapLength, in int length)
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
            var map = _input.Trim().SplitNewLine().Select(x => x.ToCharArray()).ToArray();
            var currentMap = map;

            var loop = 0l;
            var hasCHanges = false;
            do
            {
                loop += 1;
                hasCHanges = CalculateNextMap2(currentMap, out var newMap);
                currentMap = newMap;
            } while (hasCHanges);

            return currentMap.SelectMany(x => x).Count(x => x == '#').ToString();
        }

        public bool CalculateNextMap2(char[][] map, out char[][] newMap)
        {
            newMap = DuplicateMap(map.Length, map[0].Length);
            var hasCHanges = false;
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    switch (map[y][x])
                    {
                        case 'L':
                            if (NumberSurroundings2(map, x, y) == 0)
                            {
                                newMap[y][x] = '#';
                                hasCHanges = true;
                                break;
                            }

                            newMap[y][x] = map[y][x];
                            break;
                        case '#':
                            if (NumberSurroundings2(map, x, y) >= 5)
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

        private int NumberSurroundings2(in char[][] map, in int x, in int y)
        {
            var multipliers = new[]
            {
                (x: -1, y: -1),
                (x: -1, y: 0),
                (x: -1, y: 1),
                (x: 0, y: -1),
                //(0, 0),
                (x: 0, y: 1),
                (x: 1, y: -1),
                (x: 1, y: 0),
                (x: 1, y: 1),
            };

            int result = 0;
            foreach (var multiplier in multipliers)
            {
                for (int i = 1; i < 100; i++)
                {
                    var xx = x + (multiplier.x * i);
                    var yy = y + (multiplier.y * i);

                    if (
                        xx >= 0
                        && yy >= 0
                        && xx < map[0].Length
                        && yy < map.Length)
                    {
                        var a = (map[yy][xx]) switch
                        {
                            '#' => result += 1,
                            'L' => 1,
                            _ => -1,
                        };

                        if (a > 0)
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return result;
        }
    }
}