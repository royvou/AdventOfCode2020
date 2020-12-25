using AdventOfCode;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class Day_25_Tests
    {
        [Theory]
        [InlineData("5764801\n17807724", "14897079")]
        public void Part1(string input, string output)
        {
            var day = new Day_25(input);
            Assert.Equal(output, day.Solve_1());
        }

        [Theory]
        [InlineData("5764801\n17807724", "True")]
        public void Part2(string input, string output)
        {
            var day = new Day_25(input);
            Assert.Equal(output, day.Solve_2());
        }
    }
}