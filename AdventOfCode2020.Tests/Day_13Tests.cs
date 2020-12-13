using AdventOfCode;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class Day13_Tests
    {
        [Theory]
        [InlineData("939\n7,13,x,x,59,x,31,19", "295")]
        public void Part1(string input, string output)
        {
            var day = new Day_13(input);
            Assert.Equal(day.Solve_1(), output);
        }

        [Theory]
        [InlineData("7,13,x,x,59,x,31,19", "1068781")]
        [InlineData("17,x,13,19", "3417")]
        [InlineData("67,7,59,61", "754018")]
        [InlineData("67,x,7,59,61", "779210")]
        [InlineData("67,7,x,59,61", "1261476")]
        [InlineData("1789,37,47,1889", "1202161486")]
        public void Part2(string input, string output)
        {
            var day = new Day_13(input);
            Assert.Equal(day.Solve_2(), output);
        }
    }
}
/*

    The earliest timestamp that matches the list 17,x,13,19 is 3417.
    67,7,59,61 first occurs at timestamp 754018.
    67,x,7,59,61 first occurs at timestamp 779210.
    67,7,x,59,61 first occurs at timestamp 1261476.
    1789,37,47,1889 first occurs at timestamp 1202161486.
*/