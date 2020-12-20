using AdventOfCode;
using Xunit;

namespace AdventOfCode2020.Tests
{
    public class Day_20Tests
    {
        [Theory]
        [InlineData("Tile 2311:\n..##.#..#.\n##..#.....\n#...##..#.\n####.#...#\n##.##.###.\n##...#.###\n.#.#.#..##\n..#....#..\n###...#.#.\n..###..###\n\nTile 1951:\n#.##...##.\n#.####...#\n.....#..##\n#...######\n.##.#....#\n.###.#####\n###.##.##.\n.###....#.\n..#.#..#.#\n#...##.#..\n\nTile 1171:\n####...##.\n#..##.#..#\n##.#..#.#.\n.###.####.\n..###.####\n.##....##.\n.#...####.\n#.##.####.\n####..#...\n.....##...\n\nTile 1427:\n###.##.#..\n.#..#.##..\n.#.##.#..#\n#.#.#.##.#\n....#...##\n...##..##.\n...#.#####\n.#.####.#.\n..#..###.#\n..##.#..#.\n\nTile 1489:\n##.#.#....\n..##...#..\n.##..##...\n..#...#...\n#####...#.\n#..#.#.#.#\n...#.#.#..\n##.#...##.\n..##.##.##\n###.##.#..\n\nTile 2473:\n#....####.\n#..#.##...\n#.##..#...\n######.#.#\n.#...#.#.#\n.#########\n.###.#..#.\n########.#\n##...##.#.\n..###.#.#.\n\nTile 2971:\n..#.#....#\n#...###...\n#.#.###...\n##.##..#..\n.#####..##\n.#..####.#\n#..#.#..#.\n..####.###\n..#.#.###.\n...#.#.#.#\n\nTile 2729:\n...#.#.#.#\n####.#....\n..#.#.....\n....#..#.#\n.##..##.#.\n.#.####...\n####.#.#..\n##.####...\n##..#.##..\n#.##...##.\n\nTile 3079:\n#.#.#####.\n.#..######\n..#.......\n######....\n####.#..#.\n.#...#.##.\n#.#####.##\n..#.###...\n..#.......\n..#.###...", "20899048083289")]
        public void Part1(string input, string output)
        {
            var day = new Day_20(input);
            Assert.Equal(day.Solve_1(), output);
        }

        /*[Theory]
        [InlineData("42: 9 14 | 10 1\n9: 14 27 | 1 26\n10: 23 14 | 28 1\n1: \"a\"\n11: 42 31\n5: 1 14 | 15 1\n19: 14 1 | 14 14\n12: 24 14 | 19 1\n16: 15 1 | 14 14\n31: 14 17 | 1 13\n6: 14 14 | 1 14\n2: 1 24 | 14 4\n0: 8 11\n13: 14 3 | 1 12\n15: 1 | 14\n17: 14 2 | 1 7\n23: 25 1 | 22 14\n28: 16 1\n4: 1 1\n20: 14 14 | 1 15\n3: 5 14 | 16 1\n27: 1 6 | 14 18\n14: \"b\"\n21: 14 1 | 1 14\n25: 1 1 | 1 14\n22: 14 14\n8: 42\n26: 14 22 | 1 20\n18: 15 15\n7: 14 5 | 1 21\n24: 14 1\n\nabbbbbabbbaaaababbaabbbbabababbbabbbbbbabaaaa\nbbabbbbaabaabba\nbabbbbaabbbbbabbbbbbaabaaabaaa\naaabbbbbbaaaabaababaabababbabaaabbababababaaa\nbbbbbbbaaaabbbbaaabbabaaa\nbbbababbbbaaaaaaaabbababaaababaabab\nababaaaaaabaaab\nababaaaaabbbaba\nbaabbaaaabbaaaababbaababb\nabbbbabbbbaaaababbbbbbaaaababb\naaaaabbaabaaaaababaa\naaaabbaaaabbaaa\naaaabbaabbaaaaaaabbbabbbaaabbaabaaa\nbabaaabbbaaabaababbaabababaaab\naabbbbbaabbbaaaaaabbbbbababaaaaabbaaabba", "11")]
        public void Part2(string input, string output)
        {
            var day = new Day_19(input);
            Assert.Equal(day.Solve_2(), output);
        }*/
    }
}