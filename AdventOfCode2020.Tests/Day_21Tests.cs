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
            Assert.Equal(output,day.Solve_1());
        }

        [Theory]
        [InlineData("mxmxvkd kfcds sqjhc nhms (contains dairy, fish)\ntrh fvjkl sbzzf mxmxvkd (contains dairy)\nsqjhc fvjkl (contains soy)\nsqjhc mxmxvkd sbzzf (contains fish)", "mxmxvkd,sqjhc,fvjkl")]
        public void Part2(string input, string output)
        {
            var day = new Day_21(input);
            Assert.Equal(output,day.Solve_2());
        }
    }
}