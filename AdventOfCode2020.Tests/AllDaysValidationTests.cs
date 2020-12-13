using System;
using AdventOfCode;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class AllDaysValidationTests
    {
        [Theory]
        [InlineData(typeof(Day_01), "1006176", "199132160")]
        [InlineData(typeof(Day_02), "500", "313")]
        [InlineData(typeof(Day_03), "270", "2122848000")]
        [InlineData(typeof(Day_04), "202", "137")]
        [InlineData(typeof(Day_05), "866", "583")]
        [InlineData(typeof(Day_06), "6778", "3406")]
        [InlineData(typeof(Day_07), "126", "220149")]
        [InlineData(typeof(Day_08), "1744", "1174")]
        [InlineData(typeof(Day_09), "14144619", "1766397")]
        [InlineData(typeof(Day_10), "3000", "193434623148032")]
        [InlineData(typeof(Day_11), "2243", "2027")]
        [InlineData(typeof(Day_12), "759", "45763")]
        [InlineData(typeof(Day_13), "2845", "487905974205117")]

        public void TestAllDays(Type type, string result1, string result2)
        {
            var day = (BaseDay) Activator.CreateInstance(type);

            Assert.Equal(result1, day.Solve_1());
            Assert.Equal(result2, day.Solve_2());
        }
    }
}