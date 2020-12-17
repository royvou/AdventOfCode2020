using AdventOfCode;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class Day17_Tests
    {
        [Theory]
        [InlineData(".#.\n..#\n###", "112")]
        public void Part1(string input, string output)
        {
            var day = new Day_17(input);
            Assert.Equal(day.Solve_1(), output);
        }
    }
}

