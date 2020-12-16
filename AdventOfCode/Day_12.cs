using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode
{
    public class Day_12 : BaseDay
    {
        public Day_12() : base()
        {
        }

        public Day_12(string input) : base(input)
        {
        }

        public override string Solve_1()
        {
            var directions = GetDirectionsFromInput();

            var result = CalculatePosition(directions, new Day12_Position(0, 0, 0));
            return (Math.Abs(result.north) + Math.Abs(result.east)).ToString();
        }

        private IEnumerable<Day12_Action> GetDirectionsFromInput() =>
            _input.SplitNewLine().Select(x => new Day12_Action(Enum.Parse<Day12_Direction>(x.Substring(0, 1), true), int.Parse(x.Substring(1))));

        private static Day12_Position CalculatePosition(IEnumerable<Day12_Action> directions, Day12_Position initialPosition)
        {
            var result = directions.Aggregate(initialPosition, ((accumulator, next) =>
            {
                return next switch
                {
                    { } action when action.Direction == Day12_Direction.N || (action.Direction == Day12_Direction.F && accumulator.direcation == 90)
                        => accumulator with { north = accumulator.north + next.Amount},
                    { } action when action.Direction == Day12_Direction.S || (action.Direction == Day12_Direction.F && accumulator.direcation == 270)
                        => accumulator with { north = accumulator.north - next.Amount},
                    { } action when action.Direction == Day12_Direction.E || (action.Direction == Day12_Direction.F && accumulator.direcation == 0)
                        => accumulator with { east = accumulator.east + next.Amount},
                    { } action when action.Direction == Day12_Direction.W || (action.Direction == Day12_Direction.F && accumulator.direcation == 180)
                        => accumulator with { east = accumulator.east - next.Amount},
                    {Direction: Day12_Direction.L} action => accumulator with { direcation = (360 + accumulator.direcation + next.Amount) % 360},
                    {Direction: Day12_Direction.R} action => accumulator with { direcation = (360 + accumulator.direcation - next.Amount) % 360},
                    var _ => accumulator
                };
            }));
            return result;
        }

        public override string Solve_2()
        {
            var directions = GetDirectionsFromInput();

            var result = CalculatePosition2(directions, new Day12_Position(0, 0, 0, 1, 10));
            return (Math.Abs(result.north) + Math.Abs(result.east)).ToString();
        }

        private static Day12_Position CalculatePosition2(IEnumerable<Day12_Action> directions, Day12_Position initialPosition)
        {
            var result = directions.Aggregate(initialPosition, ((accumulator, next) =>
            {
                return next switch
                {
                    {Direction: Day12_Direction.N} action => accumulator with { northWaypoint = accumulator.northWaypoint + next.Amount},
                    {Direction: Day12_Direction.S} action => accumulator with { northWaypoint = accumulator.northWaypoint - next.Amount},
                    {Direction: Day12_Direction.E} action => accumulator with { eastWaypoint = accumulator.eastWaypoint + next.Amount},
                    {Direction: Day12_Direction.W} action => accumulator with { eastWaypoint = accumulator.eastWaypoint - next.Amount},
                    {Direction: Day12_Direction.F} action
                        => accumulator with {
                            north = accumulator.north + ( accumulator.northWaypoint * action.Amount),
                            east = accumulator.east + (accumulator.eastWaypoint * action.Amount),
                            },
                    {Direction: Day12_Direction.L} action => RotateWaypoint(accumulator, (360 + accumulator.direcation + next.Amount) % 360),
                    {Direction: Day12_Direction.R} action => RotateWaypoint(accumulator, (360 + accumulator.direcation - next.Amount) % 360),
                    _ => accumulator
                };
            }));
            return result;
        }

        private static Day12_Position RotateWaypoint(Day12_Position accumulator, int accumulatorDirecation)
        {
            return accumulatorDirecation switch
            {
                //0 => null, We don't change direction?!
                90 => accumulator with { northWaypoint = accumulator.eastWaypoint, eastWaypoint = -accumulator.northWaypoint},
                180 => accumulator with { northWaypoint = -accumulator.northWaypoint, eastWaypoint = -accumulator.eastWaypoint},
                270 => accumulator with { northWaypoint = -accumulator.eastWaypoint, eastWaypoint = accumulator.northWaypoint},
                _ => throw new ArgumentOutOfRangeException(nameof(accumulatorDirecation), accumulatorDirecation, null)
            };
        }
    }

    public record Day12_Action(Day12_Direction Direction, int Amount);

    public record Day12_Position(int north, int east, int direcation, int northWaypoint = 0, int eastWaypoint = 0);

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