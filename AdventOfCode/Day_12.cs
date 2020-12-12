using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode
{
    public class Day_12 : BaseDay
    {
        private string _input;

        public Day_12()
        {
            _input = File.ReadAllText(InputFilePath);
        }

        public Day_12(string input)
        {
            _input = input;
        }

        public override string Solve_1()
        {
            var directions = _input.SplitNewLine().Select(x => new Day12_Action(Enum.Parse<Day12_Direction>(x.Substring(0, 1), true), int.Parse(x.Substring(1))));

            var result = directions.Aggregate((north: 0, east: 0, dir: 0), ((accumulator, next) =>
            {
                return next switch
                {
                    Day12_Action action when action.Direction == Day12_Direction.N || (action.Direction == Day12_Direction.F && accumulator.dir == 90)
                        => (accumulator.north + next.Amount, accumulator.east, accumulator.dir),
                    Day12_Action action when action.Direction == Day12_Direction.S || (action.Direction == Day12_Direction.F && accumulator.dir == 270)
                        => (accumulator.north - next.Amount, accumulator.east, accumulator.dir),
                    Day12_Action action when action.Direction == Day12_Direction.E || (action.Direction == Day12_Direction.F && accumulator.dir == 0)
                        => (accumulator.north, accumulator.east + next.Amount, accumulator.dir),
                    Day12_Action action when action.Direction == Day12_Direction.W || (action.Direction == Day12_Direction.F && accumulator.dir == 180)
                        => (accumulator.north, accumulator.east - next.Amount, accumulator.dir),
                    Day12_Action action when action.Direction == Day12_Direction.L
                        => (accumulator.north, accumulator.east, (360 + accumulator.dir + next.Amount) % 360),
                    Day12_Action action when action.Direction == Day12_Direction.R
                        => (accumulator.north, accumulator.east, (360 + accumulator.dir - next.Amount) % 360),
                    _ => (accumulator.north, accumulator.east, accumulator.dir)
                };
            }));
            return (Math.Abs(result.north) + Math.Abs(result.east)).ToString();
        }

        public override string Solve_2()
        {
            throw new NotImplementedException();
        }
    }

    public record Day12_Action(Day12_Direction Direction, int Amount);

    public enum Day12_Direction
    {
        N,
        S,
        E,
        W,
        L,
        R,
        F
    }

    /* Alternative Switch case
      /*return next.Direction switch
                {
                    Day12_Direction.N => (accumulator.north + next.Amount, accumulator.east, accumulator.dir),
                    Day12_Direction.S => (accumulator.north - next.Amount, accumulator.east, accumulator.dir),
                    Day12_Direction.E => (accumulator.north, accumulator.east + next.Amount, accumulator.dir),
                    Day12_Direction.W => (accumulator.north, accumulator.east - next.Amount, accumulator.dir),
                    Day12_Direction.L => (accumulator.north, accumulator.east, (360 + accumulator.dir + next.Amount) % 360),
                    Day12_Direction.R => (accumulator.north, accumulator.east, (360 + accumulator.dir - next.Amount) % 360),
                    Day12_Direction.F => accumulator.dir switch
                    {
                        0 => (accumulator.north, accumulator.east + next.Amount, accumulator.dir), //East
                        90 => (accumulator.north + next.Amount, accumulator.east, accumulator.dir), //North
                        180 => (accumulator.north, accumulator.east - next.Amount, accumulator.dir), //West
                        270 => (accumulator.north - next.Amount, accumulator.east, accumulator.dir), //South
                        _ => throw new ArgumentOutOfRangeException(nameof(accumulator.dir)),
                    },
                    _ => throw new ArgumentOutOfRangeException(nameof(next.Direction)),
                };
        */
}