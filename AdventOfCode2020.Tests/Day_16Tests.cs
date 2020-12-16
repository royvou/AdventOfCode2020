using AdventOfCode;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class Day_16Tests
    {
        [Theory]
        [InlineData("class: 1-3 or 5-7\nrow: 6-11 or 33-44\nseat: 13-40 or 45-50\n\nyour ticket:\n7,1,14\n\nnearby tickets:\n7,3,47\n40,4,50\n55,2,20\n38,6,12", "71")]
        public void Part1(string input, string output)
        {
            var day = new Day_16(input);
            Assert.Equal(day.Solve_1(), output);
        }

        /*[Theory]
        [InlineData("3,1,2", "362")]
        public void Part2(string input, string output)
        {
            var day = new Day_16(input);
            Assert.Equal(day.Solve_2(), output);
        }*/
    }
}