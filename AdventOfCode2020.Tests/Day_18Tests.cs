using AdventOfCode;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class Day_18Tests
    {
        [Theory]
        [InlineData("1 + 2 * 3 + 4 * 5 + 6", "71")]
        [InlineData("1 + (2 * 3) + (4 * (5 + 6))", "51")]
        [InlineData("2 * 3 + (4 * 5)", "26")]
        [InlineData("5 + (8 * 3 + 9 + 3 * 4 * 3)", "437")]
        [InlineData("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", "12240")]
        [InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", "13632")]
        public void Part1(string input, string output)
        {
            var day = new Day_18(input);
            Assert.Equal(day.Solve_1(), output);
        }
    }
}