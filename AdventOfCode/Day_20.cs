using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

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

        public override string Solve_1()
        {
            var rawMaps = _input.SplitDoubleNewLine();

            var maps = rawMaps.Select(x =>
            {
                var lines = x.SplitNewLine();

                var id = long.Parse(lines[0].Substring(5).TrimEnd(':'));

                var board = lines[1..].Select(x => x.ToCharArray()).ToArray();

                return new Day20Map(id, board);
            }).ToArray();

            var solvedMap = new Dictionary<(int x, int y), Day20Map>();

            var solved = Solve(maps, solvedMap);

            if (solved)
            {
                var coorMax = GetCoord(maps.Length - 1, maps.Length);

                return (
                    solvedMap[(0, 0)].Number *
                    solvedMap[(coorMax.X, 0)].Number *
                    solvedMap[(0, coorMax.Y)].Number *
                    solvedMap[coorMax].Number).ToString();
            }

            return false.ToString();
            //solvedMap[0,0] + solvedMap[solvedMap / ]
        }

        private static readonly ModificationMode[] Turns =
        {
            ModificationMode.None, ModificationMode.Turn90, ModificationMode.Turn270
        };

        private static readonly ModificationMode[] Flips =
        {
            ModificationMode.None, ModificationMode.FlipX, ModificationMode.FlipY
        };


        private static readonly (Side Side, bool Reverse)[] Sides =
        {
            (Side.Bottom, false),
            (Side.Top, false),
            (Side.Left, false),
            (Side.Right, false),


            (Side.Bottom, true),
            (Side.Top, true),
            (Side.Left, true),
            (Side.Right, true),
        };

        private bool Solve(in Day20Map[] allMaps, Dictionary<(int x, int y), Day20Map> currentMap)
        {
            var sidesLookup = allMaps.ToDictionary(x => x.Number, y => Sides.Select(x => x.Reverse ? y.GetSide(x.Side).Reverse().ToArray() : y.GetSide(x.Side)).ToArray());

            return Solve(allMaps, currentMap, sidesLookup);
        }

        //BackTracking Algorithm
        private bool Solve(in Day20Map[] allMaps, Dictionary<(int x, int y), Day20Map> currentMap, Dictionary<long, char[][]> sidesLookup)
        {
            var posNumber = currentMap.Count - 1;
            if (currentMap.Count == allMaps.Length)
            {
                return true;
            }

            var (nextX, nextY) = GetCoord(posNumber + 1, allMaps.Length);

            var toTry = allMaps.Where(map =>
            {
                var isNotAlreadyAdded = currentMap.All(y => y.Value.Number != map.Number);


                bool isAllowedLeftOfme = true;
                var mySides = sidesLookup[map.Number];
                if (currentMap.TryGetValue((nextX - 1, nextY), out var leftOfMe))
                {
                    // isAllowedLeftOfme = sidesLookup[leftOfMe.Number].Any(lom => mySides.Any(ms => ms.SequenceEqual(lom)));
                    isAllowedLeftOfme = false;
                    foreach (var lom in sidesLookup[leftOfMe.Number])
                    {
                        bool any = false;
                        foreach (var ms in mySides)
                        {
                            if (ms.SequenceEqual(lom))
                            {
                                any = true;
                                break;
                            }
                        }

                        if (any)
                        {
                            isAllowedLeftOfme = true;
                            break;
                        }
                    }
                }

                bool isAllowedTopOfMe = true;
                if (currentMap.TryGetValue((nextX, nextY - 1), out var topOfMe))
                {
                    // isAllowedTopOfMe = sidesLookup[topOfMe.Number].Any(lom => mySides.Any(ms => ms.SequenceEqual(lom)));
                    isAllowedTopOfMe = false;
                    foreach (var lom in sidesLookup[topOfMe.Number])
                    {
                        bool any = false;
                        foreach (var ms in mySides)
                        {
                            if (ms.SequenceEqual(lom))
                            {
                                any = true;
                                break;
                            }
                        }

                        if (any)
                        {
                            isAllowedTopOfMe = true;
                            break;
                        }
                    }
                }


                return isNotAlreadyAdded; // && isAllowedLeftOfme && isAllowedTopOfMe;
            }).ToList();

            var board = Day20Map.CreateBoard(10, 10);
            var board2 = Day20Map.CreateBoard(10, 10);

            for (int possibility = 0; possibility < toTry.Count; possibility++)
            {
                foreach (var flip in Flips)
                {
                    foreach (var turn in Turns)
                    {
                        var originalMap = toTry[possibility];
                        currentMap[(nextX, nextY)] = originalMap.ModifyMap(flip, board).ModifyMap(turn, board2);
                        if (!IsLastPosValid(currentMap, allMaps.Length))
                        {
                            continue;
                        }

                        var solved = Solve(allMaps, currentMap, sidesLookup);
                        if (solved)
                        {
                            return true;
                        }
                        else
                        {
                            currentMap.Remove((nextX, nextY));
                        }
                    }
                }
            }

            currentMap.Remove((nextX, nextY));

            //Remove Last Coord
            return false;
        }

        private static readonly List<(int x, int y, Side current, Side mapToCheck)> SidesToCompare = new List<(int x, int y, Side current, Side mapToCheck)>
        {
            (-1, 0, Side.Left, Side.Right),
            (1, 0, Side.Right, Side.Left),

            (0, 1, Side.Bottom, Side.Top),
            (0, -1, Side.Top, Side.Bottom),
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private (int X, int Y) GetCoord(int number, int mapSize)
            => ((int) ((number) % Math.Sqrt(mapSize)), (int) ((number) / Math.Sqrt(mapSize)));

        public override string Solve_2() => throw new NotImplementedException();
    }

    public record Day20Map(long Number, char[][] Board)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public char[] GetSide(Side side) =>
            side switch
            {
                Side.Left => BoardGetX(Board, 0),
                Side.Right => BoardGetX(Board, Board.Length - 1),
                Side.Top => Board[0],
                Side.Bottom => Board[^1],
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private char[] BoardGetX(char[][] board, int x)
        {
            var result = new char[board.Length];
            for (int i = 0; i < board.Length; i++)
            {
                result[i] = board[i][x];
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Day20Map ModifyMap(ModificationMode modificationMode, char[][] buffer = default) =>
            modificationMode switch
            {
                ModificationMode.None => this,
                ModificationMode.Turn90 => this with { Board = Turn90(Board, buffer)}, //new Day20Map(Number, Turn90(Board, buffer));
                /*case ModificationMode.Turn180:
                    return new Day20Map(Number, Turn90(Turn90(Board, buffer), buffer));*/
                ModificationMode.Turn270 => this with { Board = Turn270(Board, buffer)},
                ModificationMode.FlipX => this with { Board = FlipX(Board, buffer)},
                ModificationMode.FlipY => this with { Board = FlipY(Board, buffer)},
                _ => throw new ArgumentOutOfRangeException(nameof(modificationMode), modificationMode, null)
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static char[][] CreateBoard(int sizeX, int sizeY)
        {
            var lines = new char[sizeY][];
            for (int i = 0; i < sizeY; i++)
            {
                lines[i] = new char[sizeX];
            }

            return lines;
        }
    }

    public enum Side
    {
        Left,
        Right,
        Top,
        Bottom,
    }

    public enum ModificationMode
    {
        None,
        Turn90,

        //Turn180,
        Turn270,
        FlipX,
        FlipY
    }
}