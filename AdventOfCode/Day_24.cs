using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_24 : BaseDay
    {
        public Day_24()
        {
        }

        public Day_24(string input) : base(input)
        {
        }


        public override string Solve_1()
        {
            var map = new Dictionary<Point, bool>();
            var actions = ParseInput(_input);
            ExecuteActions(actions, map);

            return map.Count(x => x.Value).ToString();
        }

        private void ExecuteActions(IEnumerable<Day24Direction[]> actions, Dictionary<Point, bool> map)
        {
            foreach (var action in actions)
            {
                Point currentPoint = new Point(0, 0);
                foreach (var step in action)
                {
                    currentPoint = MovePoint(currentPoint, step);
                }

                map[currentPoint] = map.TryGetValue(currentPoint, out var newPointvalue) ? !newPointvalue : true;
            }
        }

        // https://www.redblobgames.com/grids/hexagons/
        private Point MovePoint(Point currentPoint, Day24Direction step) =>
            step switch
            {
                Day24Direction.E => new Point(currentPoint.X + 1, currentPoint.Y),
                Day24Direction.SE => new Point(currentPoint.X + 0, currentPoint.Y + 1),
                Day24Direction.SW => new Point(currentPoint.X - 1, currentPoint.Y + 1),
                Day24Direction.W => new Point(currentPoint.X - 1, currentPoint.Y),
                Day24Direction.NW => new Point(currentPoint.X, currentPoint.Y - 1),
                Day24Direction.NE => new Point(currentPoint.X + 1, currentPoint.Y - 1),
                _ => throw new ArgumentOutOfRangeException(nameof(step), step, null)
            };
      
        private static readonly Day24Direction[] Directions = {Day24Direction.E, Day24Direction.W, Day24Direction.NE, Day24Direction.NW, Day24Direction.SE, Day24Direction.SW};

        private IEnumerable<Point> GetNeighBours(Point point)
            => Directions.Select(x => MovePoint(point, x));

        private int CountNeighbours(Dictionary<Point, bool> map, Point point)
            => GetNeighBours(point).Count(x => map.TryGetValue(x, out var black) && black);

        private IEnumerable<Day24Direction[]> ParseInput(string input)
            => input.SplitNewLine().Select(x => { return Regex.Matches(x, @"e|se|sw|w|nw|ne").Select(y => y.Value).Select(y => Enum.Parse<Day24Direction>(y, true)).ToArray(); });

        public override string Solve_2()
            => Solve_2(100);


        public string Solve_2(int rounds)
        {
            var map = new Dictionary<Point, bool>();
            var actions = ParseInput(_input);
            ExecuteActions(actions, map);

            var resultMap = Enumerable.Range(0, rounds).Aggregate(
                map,
                (currentBlackTileMap, _) => ExecuteRound(currentBlackTileMap));

            return resultMap.Count(x => x.Value).ToString();
        }

        private Dictionary<Point, bool> ExecuteRound(Dictionary<Point, bool> currentBlackTileMap)
        {
            var result = new Dictionary<Point, bool>();
            var toCheck = currentBlackTileMap.Keys.SelectMany(GetNeighBours).Concat(currentBlackTileMap.Keys).ToHashSet();
            foreach (var point in toCheck)
            {
                var neighbours = CountNeighbours(currentBlackTileMap, point);

                if (currentBlackTileMap.TryGetValue(point, out var currentPointvalue) && currentPointvalue) //Black Tile
                {
                    if (neighbours == 1 || neighbours == 2) // Any black tile with zero or more than 2 black tiles immediately adjacent to it is flipped to white.
                    {
                        result[point] = true;
                    }
                }
                else //White Tile
                {
                    //New
                    if (neighbours == 2)
                    {
                        result[point] = true;
                    }
                }
            }

            return result;
        }
    }

    public enum Day24Direction
    {
        E,
        SE,
        SW,
        W,
        NW,
        NE
    }
    
    public readonly struct Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public readonly int X;

        public readonly int Y;

        public override int GetHashCode() => HashCode.Combine(X, Y);

        public bool Equals(Point other) => X == other.X && Y == other.Y;

        public override bool Equals(object obj) 
            => obj is Point other && Equals(other);
    }
}