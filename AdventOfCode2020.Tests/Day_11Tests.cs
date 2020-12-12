using System;
using System.Linq;
using AdventOfCode;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class Day_11Tests
    {
        public Day_11Tests()
        {
            Day = new Day_11();
        }

        public Day_11 Day { get; set; }

        [Fact]
        public void Part1_Round1()
        {
            var input = "L.LL.LL.LL\nLLLLLLL.LL\nL.L.L..L..\nLLLL.LL.LL\nL.LL.LL.LL\nL.LLLLL.LL\n..L.L.....\nLLLLLLLLLL\nL.LLLLLL.L\nL.LLLLL.LL\n".Replace("\n",  Environment.NewLine);

            var map = input.Trim().SplitNewLine().Select(x => x.ToCharArray()).ToArray();
            var hasCHanges = Day.CalculateNextMap(map, out var newMap);

            var expectedOutput = "#.##.##.##\n#######.##\n#.#.#..#..\n####.##.##\n#.##.##.##\n#.#####.##\n..#.#.....\n##########\n#.######.#\n#.#####.##\n".Replace("\n", String.Empty);

            var resultAsString = string.Join(string.Empty, newMap.Select(x => new string(x)));
            Assert.Equal(expectedOutput, resultAsString);
        }
        
        [Fact]
        public void Part1_Round2()
        {
            var input = "#.##.##.##\n#######.##\n#.#.#..#..\n####.##.##\n#.##.##.##\n#.#####.##\n..#.#.....\n##########\n#.######.#\n#.#####.##\n".Replace("\n",  Environment.NewLine);

            var map = input.Trim().SplitNewLine().Select(x => x.ToCharArray()).ToArray();
            var hasCHanges = Day.CalculateNextMap(map, out var newMap);

            var expectedOutput = "#.LL.L#.##\n#LLLLLL.L#\nL.L.L..L..\n#LLL.LL.L#\n#.LL.LL.LL\n#.LLLL#.##\n..L.L.....\n#LLLLLLLL#\n#.LLLLLL.L\n#.#LLLL.##\n".Replace("\n", String.Empty);

            var resultAsString = string.Join(string.Empty, newMap.Select(x => new string(x)));
            Assert.Equal(expectedOutput, resultAsString);
        }
        
        [Fact]
        public void Part2_Round1()
        {
            var input = "L.LL.LL.LL\nLLLLLLL.LL\nL.L.L..L..\nLLLL.LL.LL\nL.LL.LL.LL\nL.LLLLL.LL\n..L.L.....\nLLLLLLLLLL\nL.LLLLLL.L\nL.LLLLL.LL\n".Replace("\n",  Environment.NewLine);

            var map = input.Trim().SplitNewLine().Select(x => x.ToCharArray()).ToArray();
            var hasCHanges = Day.CalculateNextMap2(map, out var newMap);

            var expectedOutput = "#.##.##.##\n#######.##\n#.#.#..#..\n####.##.##\n#.##.##.##\n#.#####.##\n..#.#.....\n##########\n#.######.#\n#.#####.##\n".Replace("\n", String.Empty);

            var resultAsString = string.Join(string.Empty, newMap.Select(x => new string(x)));
            Assert.Equal(expectedOutput, resultAsString);
        }
        
        [Fact]
        public void Part2_Round2()
        {
            var input = "#.##.##.##\n#######.##\n#.#.#..#..\n####.##.##\n#.##.##.##\n#.#####.##\n..#.#.....\n##########\n#.######.#\n#.#####.##\n".Replace("\n",  Environment.NewLine);

            var map = input.Trim().SplitNewLine().Select(x => x.ToCharArray()).ToArray();
            var hasCHanges = Day.CalculateNextMap2(map, out var newMap);
            
            var expectedOutput = "#.LL.LL.L#\n#LLLLLL.LL\nL.L.L..L..\nLLLL.LL.LL\nL.LL.LL.LL\nL.LLLLL.LL\n..L.L.....\nLLLLLLLLL#\n#.LLLLLL.L\n#.LLLLL.L#\n".Replace("\n", String.Empty);

            var resultAsString = string.Join(string.Empty, newMap.Select(x => new string(x)));
            Assert.Equal(expectedOutput, resultAsString);
        }
        
         
        [Fact]
        public void Part2_Round3()
        {
            var input =  "#.LL.LL.L#\n#LLLLLL.LL\nL.L.L..L..\nLLLL.LL.LL\nL.LL.LL.LL\nL.LLLLL.LL\n..L.L.....\nLLLLLLLLL#\n#.LLLLLL.L\n#.LLLLL.L#\n".Replace("\n",  Environment.NewLine);

            var map = input.Trim().SplitNewLine().Select(x => x.ToCharArray()).ToArray();
            var hasCHanges = Day.CalculateNextMap2(map, out var newMap);
            
            var expectedOutput = "#.L#.##.L#\n#L#####.LL\nL.#.#..#..\n##L#.##.##\n#.##.#L.##\n#.#####.#L\n..#.#.....\nLLL####LL#\n#.L#####.L\n#.L####.L#\n".Replace("\n", String.Empty);

            var resultAsString = string.Join(string.Empty, newMap.Select(x => new string(x)));
            Assert.Equal(expectedOutput, resultAsString);
        }
        
        [Fact]
        public void Part2_Round4()
        {
            var input =  "#.L#.##.L#\n#L#####.LL\nL.#.#..#..\n##L#.##.##\n#.##.#L.##\n#.#####.#L\n..#.#.....\nLLL####LL#\n#.L#####.L\n#.L####.L#\n".Replace("\n",  Environment.NewLine);

            var map = input.Trim().SplitNewLine().Select(x => x.ToCharArray()).ToArray();
            var hasCHanges = Day.CalculateNextMap2(map, out var newMap);
            
            var expectedOutput = "#.L#.L#.L#\n#LLLLLL.LL\nL.L.L..#..\n##LL.LL.L#\nL.LL.LL.L#\n#.LLLLL.LL\n..L.L.....\nLLLLLLLLL#\n#.LLLLL#.L\n#.L#LL#.L#\n".Replace("\n", String.Empty);

            var resultAsString = string.Join(string.Empty, newMap.Select(x => new string(x)));
            Assert.Equal(expectedOutput, resultAsString);
        }
    }
}