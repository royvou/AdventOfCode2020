using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_17 : BaseDay
    {
        public Day_17()
        {
        }

        public Day_17(string input) : base(input)
        {
        }

        public override string Solve_1()
        {
            var map = new Dictionary<(int x, int y, int z), bool>();
            var lines = _input.SplitNewLine();
            var z = 0;
            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[y].Length; x++)
                {
                    map.Add((x, y, z), lines[y][x] == '#');
                }
            }

            var initialBoardSize = lines.Length;

            var currentMap = map;
            var calculateOffset = 0;
            for (var loop = 1; loop <= 6; loop++)
            {
                var newMap = new Dictionary<(int x, int y, int z), bool>();

                for (var minZ = 0 - loop - calculateOffset; minZ <= 0 + loop + calculateOffset; minZ++)
                {
                    for (var minY = 0 - loop - calculateOffset; minY <= 0 + loop + initialBoardSize + calculateOffset; minY++)
                    {
                        for (var minX = 0 - loop - calculateOffset; minX <= 0 + loop + initialBoardSize + calculateOffset; minX++)
                        {
                            var currentlyChecked = currentMap.TryGetValue((minX, minY, minZ), out var isChecked) && isChecked;

                            var numberOfNeighbours = GetNeighbours(currentMap, minX, minY, minZ);

                            if (currentlyChecked && (numberOfNeighbours == 2 || numberOfNeighbours == 3))
                            {
                                newMap[(minX, minY, minZ)] = true;
                            }
                            else if (!currentlyChecked && (numberOfNeighbours == 3))
                            {
                                newMap[(minX, minY, minZ)] = true;
                            }
                        }
                    }
                }

                currentMap = newMap;
            }

            return currentMap.Values.Count(x => x).ToString();
        }

        private int GetNeighbours(Dictionary<(int x, int y, int z), bool> map, in int locX, in int locY, in int locZ)
        {
            var result = 0;
            for (var z = locZ - 1; z <= locZ + 1; z++)
            {
                for (var y = locY - 1; y <= locY + 1; y++)
                {
                    for (var x = locX - 1; x <= locX + 1; x++)
                    {
                        if (locX == x && locY == y && locZ == z)
                            continue;

                        if (map.TryGetValue((x, y, z), out var match) && match)
                        {
                            result += 1;
                        }
                    }
                }
            }

            return result;
        }

        public override string Solve_2()
        {
            var map = new Dictionary<(int x, int y, int z, int w), bool>();
            var lines = _input.SplitNewLine();
            var z = 0;
            int w = 0;
            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[y].Length; x++)
                {
                    map.Add((x, y, z, w), lines[y][x] == '#');
                }
            }

            var initialBoardSize = lines.Length;

            var currentMap = map;
            var calculateOffset = 0;
            for (var loop = 1; loop <= 6; loop++)
            {
                var newMap = new Dictionary<(int x, int y, int z, int w), bool>();

                for (var minW = 0 - loop - calculateOffset; minW <= 0 + loop + calculateOffset; minW++)
                {
                    for (var minZ = 0 - loop - calculateOffset; minZ <= 0 + loop + calculateOffset; minZ++)
                    {
                        for (var minY = 0 - loop - calculateOffset; minY <= 0 + loop + initialBoardSize + calculateOffset; minY++)
                        {
                            for (var minX = 0 - loop - calculateOffset; minX <= 0 + loop + initialBoardSize + calculateOffset; minX++)
                            {
                                var currentlyChecked = currentMap.TryGetValue((minX, minY, minZ, minW), out var isChecked) && isChecked;

                                var numberOfNeighbours = GetNeighbours2(currentMap, minX, minY, minZ, minW);

                                if (currentlyChecked && (numberOfNeighbours == 2 || numberOfNeighbours == 3))
                                {
                                    newMap[(minX, minY, minZ, minW)] = true;
                                }
                                else if (!currentlyChecked && (numberOfNeighbours == 3))
                                {
                                    newMap[(minX, minY, minZ, minW)] = true;
                                }
                            }
                        }
                    }
                }

                currentMap = newMap;
            }

            return currentMap.Values.Count(x => x).ToString();
        }

        private int GetNeighbours2(Dictionary<(int x, int y, int z, int w), bool> map, in int locX, in int locY, in int locZ, in int locW)
        {
            var result = 0;
            for (var w = locW - 1; w <= locW + 1; w++)
            {
                for (var z = locZ - 1; z <= locZ + 1; z++)
                {
                    for (var y = locY - 1; y <= locY + 1; y++)
                    {
                        for (var x = locX - 1; x <= locX + 1; x++)
                        {
                            if (locX == x && locY == y && locZ == z && locW == w)
                                continue;

                            if (map.TryGetValue((x, y, z, w), out var match) && match)
                            {
                                result += 1;
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}