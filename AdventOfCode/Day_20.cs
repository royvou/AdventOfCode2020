using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode
{
    public class Day_20 : BaseDay
    {
        public Day_20()
        {
        }

        public Day_20(string input) : base(input)
        {
        }

        private static readonly Day20ModificationMode[] Turns =
        {
            Day20ModificationMode.None, Day20ModificationMode.Turn90, Day20ModificationMode.FlipX, Day20ModificationMode.Turn270
        };

        private static readonly Day20ModificationMode[] Flips =
        {
            Day20ModificationMode.None, Day20ModificationMode.FlipY
        };

        public override string Solve_1()
        {
            var maps = ParseMaps(_input);

            //Create lookup for matches left/top
            var mapsLeftLookp = new Dictionary<string, List<Day20Map>>();
            var mapsTopLookup = new Dictionary<string, List<Day20Map>>();
            foreach (var map in maps)
            {
                foreach (var turn in Turns)
                {
                    foreach (var flip in Flips)
                    {
                        var newMap = map.ModifyMap(turn).ModifyMap(flip);
                        var sideLeft = newMap.GetSide(Day20Side.Left);

                        if (mapsLeftLookp.TryGetValue(GetKey(sideLeft), out var sideLeftValue))
                        {
                            sideLeftValue.Add(newMap);
                        }
                        else
                        {
                            mapsLeftLookp.Add(GetKey(sideLeft), new List<Day20Map>() {newMap});
                        }

                        var sideBottom = newMap.GetSide(Day20Side.Top);

                        if (mapsTopLookup.TryGetValue(GetKey(sideBottom), out var sideBottomValue))
                        {
                            sideBottomValue.Add(newMap);
                        }
                        else
                        {
                            mapsTopLookup.Add(GetKey(sideBottom), new List<Day20Map>() {newMap});
                        }
                    }
                }
            }

            var solvedMap = new Dictionary<(int x, int y), Day20Map>();
            var solved = Solve(maps, solvedMap, mapsLeftLookp, mapsTopLookup);
            if (solved)
            {
                var coorMax = GetCoord(maps.Length - 1, maps.Length);

                return (
                    solvedMap[(0, 0)].Number *
                    solvedMap[(coorMax.X, 0)].Number *
                    solvedMap[(0, coorMax.Y)].Number *
                    solvedMap[coorMax].Number).ToString();
            }

            return null;
        }

        private static Day20Map[] ParseMaps(string input)
        {
            var rawMaps = input.SplitDoubleNewLine();
            return rawMaps.Select(x =>
            {
                var lines = x.SplitNewLine();

                var id = long.Parse(lines[0].Substring(5).TrimEnd(':'));

                var board = lines[1..].Select(x => x.ToCharArray()).ToArray();

                return new Day20Map(id, board);
            }).ToArray();
        }

        private bool Solve(in Day20Map[] allMaps, Dictionary<(int x, int y), Day20Map> currentMap, Dictionary<string, List<Day20Map>> sidesLeftLookup, Dictionary<string, List<Day20Map>> sidesBottomLookup)
        {
            var posNumber = currentMap.Count - 1;
            if (currentMap.Count == allMaps.Length)
            {
                return true;
            }

            var (nextX, nextY) = GetCoord(posNumber + 1, allMaps.Length);

            List<Day20Map> toTry = null;
            bool hasLeftFromMe = false;
            bool skip = false;
            if (currentMap.TryGetValue((nextX - 1, nextY), out var lefFromMe))
            {
                hasLeftFromMe = true;
                var toLookup = lefFromMe.GetSide(Day20Side.Right);

                toTry = sidesLeftLookup.ContainsKey(GetKey(toLookup)) ? sidesLeftLookup[GetKey(toLookup)] : null;

                skip = toTry == null;
            }

            if (!skip && currentMap.TryGetValue((nextX, nextY - 1), out var topFromMe))
            {
                var toLookup = topFromMe.GetSide(Day20Side.Bottom);

                var completeList = sidesBottomLookup.ContainsKey(GetKey(toLookup)) ? sidesBottomLookup[GetKey(toLookup)] : null;

                toTry = toTry == null
                    ? completeList.ToList()
                    : toTry.Where(x => completeList.Any(y => y.Number == x.Number)).ToList();
            }

            //toTry = toTry.Where(x => currentMap.Values.All(y => y.Number != x.Number)).ToList();
            if (currentMap.Count == 0)
            {
                toTry = allMaps.ToList();
            }

            toTry = toTry?.Where(x => currentMap.Values.All(y => y.Number != x.Number)).ToList();

            if (toTry == null)
            {
                return false;
            }

            for (int possibility = 0; possibility < toTry.Count; possibility++)
            {
                var originalMap = toTry[possibility];
                currentMap[(nextX, nextY)] = originalMap;
                if (!IsLastPosValid(currentMap, allMaps.Length))
                {
                    continue;
                }

                var solved = Solve(allMaps, currentMap, sidesLeftLookup, sidesBottomLookup);
                if (solved)
                {
                    return true;
                }
                else
                {
                    currentMap.Remove((nextX, nextY));
                }
            }

            currentMap.Remove((nextX, nextY));

            //Remove Last Coord
            return false;
        }

        private string GetKey(char[] input)
            => string.Join("", input);

        private static readonly List<(int x, int y, Day20Side current, Day20Side mapToCheck)> SidesToCompare = new()
        {
            (-1, 0, Day20Side.Left, Day20Side.Right),
            (1, 0, Day20Side.Right, Day20Side.Left),

            (0, 1, Day20Side.Bottom, Day20Side.Top),
            (0, -1, Day20Side.Top, Day20Side.Bottom),
        };

        private bool IsLastPosValid(IDictionary<(int x, int y), Day20Map> currentMap, int maxWidthMap)
        {
            var coordsToCheck = GetCoord(currentMap.Count - 1, maxWidthMap);

            if (!currentMap.TryGetValue((coordsToCheck.X, coordsToCheck.Y), out var currentmapcoord))
            {
                return true;
            }

            foreach (var sideToCheck in SidesToCompare)
            {
                if (currentMap.TryGetValue((coordsToCheck.X + sideToCheck.x, coordsToCheck.Y + sideToCheck.y), out var neighbor))
                {
                    if (!neighbor.GetSide(sideToCheck.mapToCheck).SequenceEqual(currentmapcoord.GetSide(sideToCheck.current)))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private (int X, int Y) GetCoord(int number, int mapSize)
            => ((int) ((number) % Math.Sqrt(mapSize)), (int) ((number) / Math.Sqrt(mapSize)));

        public override string Solve_2()
        {
            var maps = ParseMaps(_input);

            //Create lookup for matches left/top
            var mapsLeftLookp = new Dictionary<string, List<Day20Map>>();
            var mapsTopLookup = new Dictionary<string, List<Day20Map>>();
            foreach (var map in maps)
            {
                foreach (var turn in Turns)
                {
                    foreach (var flip in Flips)
                    {
                        var newMap = map.ModifyMap(turn).ModifyMap(flip);
                        var sideLeft = newMap.GetSide(Day20Side.Left);

                        if (mapsLeftLookp.TryGetValue(GetKey(sideLeft), out var sideLeftValue))
                        {
                            sideLeftValue.Add(newMap);
                        }
                        else
                        {
                            mapsLeftLookp.Add(GetKey(sideLeft), new List<Day20Map>() {newMap});
                        }

                        var sideBottom = newMap.GetSide(Day20Side.Top);

                        if (mapsTopLookup.TryGetValue(GetKey(sideBottom), out var sideBottomValue))
                        {
                            sideBottomValue.Add(newMap);
                        }
                        else
                        {
                            mapsTopLookup.Add(GetKey(sideBottom), new List<Day20Map>() {newMap});
                        }
                    }
                }
            }

            var solvedMap = new Dictionary<(int x, int y), Day20Map>();
            var solved = Solve(maps, solvedMap, mapsLeftLookp, mapsTopLookup);
            if (solved)
            {
                var slicedMap = solvedMap.ToDictionary(
                    x => x.Key,
                    y => y.Value.Slice(1));

                var fullMap = CreateFullMap(slicedMap);

                long result = 0L;
                foreach (var turn in Turns)
                {
                    foreach (var flip in Flips)
                    {
                        var abc = fullMap.ModifyMap(turn).ModifyMap(flip);
                        result += CountMonsters(abc);
                    }
                }

                return (CountWaves(fullMap) - (result * 15)).ToString();
            }


            return null;
        }

        private int CountWaves(Day20Map fullMap)
        {
            int result = 0;
            for (int y = 0; y < fullMap.Board.Length; y++)
            {
                for (int x = 0; x < fullMap.Board[y].Length; x++)
                {
                    if (fullMap.Board[y][x] == '#')
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        private int CountMonsters(Day20Map fullMap)
        {
            int result = 0;
            for (int y = 0; y < fullMap.Board.Length - 2 - 1; y++)
            {
                for (int x = 0; x < fullMap.Board[y].Length - 18 - 1; x++)
                {
                    if (fullMap.Board[x + 18][y] == '#' &&
                        fullMap.Board[x][y + 1] == '#' &&
                        fullMap.Board[x + 5][y + 1] == '#' &&
                        fullMap.Board[x + 6][y + 1] == '#' &&
                        fullMap.Board[x + 11][y + 1] == '#' &&
                        fullMap.Board[x + 12][y + 1] == '#' &&
                        fullMap.Board[x + 17][y + 1] == '#' &&
                        fullMap.Board[x + 18][y + 1] == '#' &&
                        fullMap.Board[x + 19][y + 1] == '#' &&
                        fullMap.Board[x + 1][y + 2] == '#' &&
                        fullMap.Board[x + 4][y + 2] == '#' &&
                        fullMap.Board[x + 7][y + 2] == '#' &&
                        fullMap.Board[x + 10][y + 2] == '#' &&
                        fullMap.Board[x + 13][y + 2] == '#' &&
                        fullMap.Board[x + 16][y + 2] == '#')
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        private Day20Map CreateFullMap(Dictionary<(int x, int y), Day20Map> solvedMap)
        {
            var boardLength = solvedMap[(0, 0)].Board.Length;
            int width = (int) (Math.Sqrt(solvedMap.Count) * solvedMap[(0, 0)].Board.Length);
            var board = Day20Map.CreateBoard(width, width);

            for (var y = 0; y < width; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    board[y][x] = solvedMap[(x / boardLength, y / boardLength)].Board[y % boardLength][x % boardLength];
                }
            }

            return new Day20Map(1, board);
        }
    }

    public record Day20Map(long Number, char[][] Board)
    {
        public char[] GetSide(Day20Side side) =>
            side switch
            {
                Day20Side.Left => BoardGetX(Board, 0),
                Day20Side.Right => BoardGetX(Board, Board.Length - 1),
                Day20Side.Top => Board[0],
                Day20Side.Bottom => Board[^1],
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            };

        private char[] BoardGetX(char[][] board, int x)
        {
            var result = new char[board.Length];
            for (int i = 0; i < board.Length; i++)
            {
                result[i] = board[i][x];
            }

            return result;
        }

        public Day20Map ModifyMap(Day20ModificationMode modificationMode, char[][] buffer = default) =>
            modificationMode switch
            {
                Day20ModificationMode.None => this,
                Day20ModificationMode.Turn90 => this with { Board = Turn90(Board, buffer)},
                Day20ModificationMode.Turn270 => this with { Board = Turn270(Board, buffer)},
                Day20ModificationMode.FlipX => this with { Board = FlipX(Board, buffer)},
                Day20ModificationMode.FlipY => this with { Board = FlipY(Board, buffer)},
                _ => throw new ArgumentOutOfRangeException(nameof(modificationMode), modificationMode, null)
            };

        public Day20Map Slice(int size)
            => this with { Board = Slice(Board, size)};

        private char[][] FlipY(char[][] board, char[][] buffer = default)
        {
            var newBoard = buffer ?? CreateBoard(board.Length, board[0].Length);
            for (int y = 0; y < board.Length; y++)
            {
                for (int x = 0; x < board[y].Length; x++)
                {
                    newBoard[board.Length - 1 - y][x] = board[y][x];
                }
            }

            return newBoard;
        }

        private char[][] FlipX(char[][] board, char[][] buffer = default)
        {
            var newBoard = buffer ?? CreateBoard(board.Length, board[0].Length);
            for (int y = 0; y < board.Length; y++)
            {
                for (int x = 0; x < board[y].Length; x++)
                {
                    newBoard[y][board[y].Length - 1 - x] = board[y][x];
                }
            }

            return newBoard;
        }

        private char[][] Turn90(char[][] board, char[][] buffer = default)
        {
            var newBoard = buffer ?? CreateBoard(board.Length, board[0].Length);
            for (int y = 0; y < board.Length; y++)
            {
                for (int x = 0; x < board[y].Length; x++)
                {
                    newBoard[x][board[y].Length - 1 - y] = board[y][x];
                }
            }

            return newBoard;
        }

        private char[][] Turn270(char[][] board, char[][] buffer = default)
        {
            var newBoard = buffer ?? CreateBoard(board.Length, board[0].Length);
            for (int y = 0; y < board.Length; y++)
            {
                for (int x = 0; x < board[y].Length; x++)
                {
                    newBoard[board[y].Length - 1 - x][y] = board[y][x];
                }
            }

            return newBoard;
        }

        private char[][] Slice(char[][] board, int size, char[][] buffer = default)
        {
            var newBoard = buffer ?? CreateBoard(board.Length - size - size, board[0].Length - size - size);

            for (int y = size; y < board.Length - size; y++)
            {
                for (int x = size; x < board[y].Length - size; x++)
                {
                    newBoard[y - size][x - size] = board[y][x];
                }
            }

            return newBoard;
        }

        public static char[][] CreateBoard(int sizeX, int sizeY)
        {
            var lines = new char[sizeY][];
            for (int i = 0; i < sizeY; i++)
            {
                lines[i] = new char[sizeX];
            }

            return lines;
        }

        public void PrintBoard()
            => PrintBoard(Board);

        private void PrintBoard(char[][] board)
        {
            Console.WriteLine("---------");

            for (int y = 0; y < board.Length; y++)
            {
                for (int x = 0; x < board[y].Length; x++)
                {
                    Console.Write(board[y][x]);
                }

                Console.WriteLine("");
            }

            Console.WriteLine("");
            Console.WriteLine("---------");
        }
    }

    public enum Day20Side
    {
        Left,
        Right,
        Top,
        Bottom,
    }

    public enum Day20ModificationMode
    {
        None,
        Turn90,

        //Turn180,
        Turn270,
        FlipX,
        FlipY
    }
}