using AdventOfCode;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class Day_12Tests
    {
      
        [Theory]
        [InlineData("F10\nN3\nF7\nR90\nF11", "25")]
        public void Part1(string input, string output)
        {
            var Day12 = new Day_12(input);
            Assert.Equal(Day12.Solve_1(), output);
        }
        
        [Theory]
        [InlineData("F10\nN3\nF7\nR90\nF11", "286")]
        public void Part2(string input, string output)
        {
            var Day12 = new Day_12(input);
            Assert.Equal(Day12.Solve_2(), output);
        }
    }
}