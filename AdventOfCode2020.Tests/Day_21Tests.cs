using AdventOfCode;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class Day_21Tests
    {
          [Theory]
        [InlineData("mxmxvkd kfcds sqjhc nhms (contains dairy, fish)\ntrh fvjkl sbzzf mxmxvkd (contains dairy)\nsqjhc fvjkl (contains soy)\nsqjhc mxmxvkd sbzzf (contains fish)", "5")]
        public void Part1(string input, string output)
        {
            var day = new Day_21(input);
            Assert.Equal(day.Solve_1(), output);
        }

        [Theory]
        [InlineData("mxmxvkd kfcds sqjhc nhms (contains dairy, fish)\ntrh fvjkl sbzzf mxmxvkd (contains dairy)\nsqjhc fvjkl (contains soy)\nsqjhc mxmxvkd sbzzf (contains fish)", "5")]
        public void Part2(string input, string output)
        {
            var day = new Day_21(input);
            Assert.Equal(day.Solve_2(), output);
        }
    }
}