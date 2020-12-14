using AdventOfCode;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class Day_14Tests
    {
        [Theory]
        [InlineData("mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X\nmem[8] = 11\nmem[7] = 101\nmem[8] = 0", "165")]
        public void Part1(string input, string output)
        {
            var day = new Day_14(input);
            Assert.Equal(day.Solve_1(), output);
        }

        [Theory]
        [InlineData("mask = 000000000000000000000000000000X1001X\nmem[42] = 100\nmask = 00000000000000000000000000000000X0XX\nmem[26] = 1", "208")]
        public void Part2(string input, string output)
        {
            var day = new Day_14(input);
            Assert.Equal(day.Solve_2(), output);
        }
    }
}