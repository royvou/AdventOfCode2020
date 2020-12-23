using AdventOfCode;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class Day_23Tests
    {
        [Theory]
        [InlineData("389125467", "54673289", 1)]
        [InlineData("389125467", "32546789", 2)]
        [InlineData("389125467", "34672589", 3)]
        [InlineData("389125467", "32584679", 4)]
        [InlineData("389125467", "36792584", 5)]
        [InlineData("389125467", "93672584", 6)]
        [InlineData("389125467", "92583674", 7)]
        [InlineData("389125467", "58392674", 8)]
        [InlineData("389125467", "83926574", 9)]
        [InlineData("389125467", "92658374", 10)]
        [InlineData("389125467", "67384529", 100)]
        public void Part1(string input, string output, int rounds)
        {
            var day = new Day_23(input);
            Assert.Equal(output, day.Solve_1(rounds));
        }

        [Theory]
        [InlineData("389125467", "149245887792")]
        public void Part2(string input, string output)
        {
            var day = new Day_23(input);
            Assert.Equal(output, day.Solve_2());
        }
    }
}