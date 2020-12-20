using AdventOfCode;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class Day_19Tests
    {
        [Theory]
        [InlineData("0: 4 1 5\n1: 2 3 | 3 2\n2: 4 4 | 5 5\n3: 4 5 | 5 4\n4: \"a\"\n5: \"b\"\n\nababbb\nbababa\nabbbab\naaabbb\naaaabbb", "2")]
        public void Part1(string input, string output)
        {
            var day = new Day_19(input);
            Assert.Equal(day.Solve_1(), output);
        }

        [Theory]
        [InlineData("", "")]
        public void Part2(string input, string output)
        {
            var day = new Day_19(input);
            Assert.Equal(day.Solve_2(), output);
        }
    }
}