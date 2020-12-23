using AdventOfCode;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class Day_23Tests
    {
        [Theory]
        [InlineData("389125467", "92658374", 10)]
        [InlineData("389125467", "67384529", 100)]

        public void Part1(string input, string output, int rounds)
        {
            var day = new Day_23(input);
            Assert.Equal(output, day.Solve_1(rounds));
        }

        [Theory]
        [InlineData("Player 1:\n9\n2\n6\n3\n1\n\nPlayer 2:\n5\n8\n4\n7\n10", "291")]
        public void Part2(string input, string output)
        {
            var day = new Day_23(input);
            Assert.Equal(output, day.Solve_2());
        }
        
    }
}