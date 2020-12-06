using AdventOfCode;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class Day_05Tests
    {
        public Day_05Tests()
        {
            Day = new Day_05();
        }

        public Day_05 Day { get; set; }

        [Theory]
        [InlineData("BFFFBBFRRR", 567)]
        [InlineData("FFFBBBFRRR", 119)]
        [InlineData("BBFFBBFRLL", 820)]
        public void GetSeatIdTest(string input, int output)
        {
            Assert.Equal(Day.GetSeatId(input), output);
        }
        
        [Theory]
        [InlineData("BFFFBBFRRR", 70)]
        [InlineData("FFFBBBFRRR", 14)]
        [InlineData("BBFFBBFRLL", 102)]
        public void GetRowTest(string input, int output)
        {
            Assert.Equal(Day.GetRow(input), output);
        }
        
        [Theory]
        [InlineData("BFFFBBFRRR", 7)]
        [InlineData("FFFBBBFRRR", 7)]
        [InlineData("BBFFBBFRLL", 4)]
        public void GetColumnTest(string input, int output)
        {
            Assert.Equal(Day.GetColumn(input), output);
        }
    }
}