using System;
using System.Collections.Generic;
using System.Linq;

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


        //BackTracking Algorithm
        private bool Solve(Day20Map[] allMaps, IDictionary<(int x, int y), Day20Map> currentMap)
        {
            var posNumber = currentMap.Count - 1;
            if (currentMap.Count == allMaps.Length)
            {
                return true;
            }

            //Get Possible maps
            var toTry = allMaps.Where(x => { return currentMap.Values.All(y => x.Number != y.Number); }).ToList();

            var (nextX, nextY) = GetCoord(posNumber + 1, allMaps.Length);
            for (int possibility = 0; possibility < toTry.Count; possibility++)
            {
                foreach (var flip in Flips)
                {
                    foreach (var turn in Turns)
                    {
                        currentMap[(nextX, nextY)] = toTry[possibility].ModifyMap(flip).ModifyMap(turn);
                        if (!IsLastPosValid(currentMap, allMaps.Length))
                        {
                            continue;
                        }

                        var solved = Solve(allMaps, currentMap);
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

        private bool IsLastPosValid(IDictionary<(int x, int y), Day20Map> currentMap, int maxWidthMap)
        {
            var coordsToCheck = GetCoord(currentMap.Count - 1, maxWidthMap);
            var sidesTocheck = new List<(int x, int y, Side current, Side mapToCheck)>
            {
                (-1, 0, Side.Left, Side.Right),
                (1, 0, Side.Right, Side.Left),

                (0, 1, Side.Bottom, Side.Top),
                (0, -1, Side.Top, Side.Bottom),
            };

            if (!currentMap.TryGetValue((coordsToCheck.X, coordsToCheck.Y), out var currentmapcoord))
            {
                return true;
            }

            foreach (var sideToCheck in sidesTocheck)
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

        public override string Solve_2() => throw new NotImplementedException();
    }

    public record Day20Map(long Number, char[][] Board)
    {
        public char[] GetSide(Side side) =>
            side switch
            {
                Side.Left => Board.Select(x => x[0]).ToArray(),
                Side.Right => Board.Select(x => x[^1]).ToArray(),
                Side.Top => Board[0],
                Side.Bottom => Board[^1],
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            };

        public Day20Map ModifyMap(ModificationMode modificationMode)
            => modificationMode switch
            {
                ModificationMode.None => this,
                ModificationMode.Turn90 => new Day20Map(Number, Turn90(Board)),
                ModificationMode.Turn180 => new Day20Map(Number, Turn90(Turn90(Board))),
                ModificationMode.Turn270 => new Day20Map(Number, Turn90(Turn90(Turn90(Board)))),
                ModificationMode.FlipX => new Day20Map(Number, FlipX(Board)),
                ModificationMode.FlipY => new Day20Map(Number, FlipY(Board)),
                _ => throw new ArgumentOutOfRangeException(nameof(modificationMode), modificationMode, null)
            };

        private char[][] FlipY(char[][] board)
        {
            var newBoard = CreateBoard(board.Length, board[0].Length);
            for (int y = 0; y < board.Length; y++)
            {
                for (int x = 0; x < board[y].Length; x++)
                {
                    newBoard[board.Length - 1 - y][x] = board[y][x];
                }
            }

            return newBoard;
        }

        private char[][] FlipX(char[][] board)
        {
            var newBoard = CreateBoard(board.Length, board[0].Length);
            for (int y = 0; y < board.Length; y++)
            {
                for (int x = 0; x < board[y].Length; x++)
                {
                    newBoard[y][board[y].Length - 1 - x] = board[y][x];
                }
            }

            return newBoard;
        }

        private char[][] Turn90(char[][] board)
        {
            var newBoard = CreateBoard(board.Length, board[0].Length);
            for (int y = 0; y < board.Length; y++)
            {
                for (int x = 0; x < board[y].Length; x++)
                {
                    newBoard[x][board[y].Length - 1 - y] = board[y][x];
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

        private char[][] CreateBoard(int sizeX, int sizeY)
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
        Turn180,
        Turn270,
        FlipX,
        FlipY
    }
}