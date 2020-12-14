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

        /*
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
        }*/
    }
}