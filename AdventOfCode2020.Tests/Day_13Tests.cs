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
    }
}