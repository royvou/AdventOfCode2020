using AdventOfCode;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class Day_15Tests
    {
        [Theory]
        [InlineData("0,3,6", "436")]
        [InlineData("1,3,2", "1")]
        [InlineData("2,1,3", "10")]
        [InlineData("1,2,3", "27")]
        [InlineData("2,3,1", "78")]
        [InlineData("3,2,1", "438")]
        [InlineData("3,1,2", "1836")]
        public void Part1(string input, string output)
        {
            var day = new Day_15(input);
            Assert.Equal(day.Solve_1(), output);
        }

        [Theory]
        [InlineData("0,3,6", "175594")]
        [InlineData("1,3,2", "2578")]
        [InlineData("2,1,3", "3544142")]
        [InlineData("1,2,3", "261214")]
        [InlineData("2,3,1", "6895259")]
        [InlineData("3,2,1", "18")]
        [InlineData("3,1,2", "362")]
        public void Part2(string input, string output)
        {
            var day = new Day_15(input);
            Assert.Equal(day.Solve_2(), output);
        }
    }
}