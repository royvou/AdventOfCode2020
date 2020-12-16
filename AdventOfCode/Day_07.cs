using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode
{
    public class Day_07 : BaseDay
    {
        public override string Solve_1()
        {
            var bags = _input.SplitNewLine().ToImmutableDictionary(
                input => Regex.Match(input, @"^(.*?) bag(s|) contain .*").Groups[1].Value,
                input => Regex.Matches(input, @"(\d (.*?) bag(s|))").Select(x => x.Groups[2].Value).ToArray());

            return FindBagsCount(bags, "shiny gold").ToString();
        }

        private int FindBagsCount(ImmutableDictionary<string, string[]> bags, string bagToFind)
        {
            return bags.Select(x => FindBagsCount(bags, x.Key, bagToFind)).Where(x => x).Count(x => x);
        }

        private bool FindBagsCount(in ImmutableDictionary<string, string[]> bags, in string bag, in string bagToFind) //, HashSet<string> result)
        {
            var currentBagContents = bags[bag];
            if (currentBagContents.Contains(bagToFind))
            {
                return true;
            }

            foreach (var innerBagContents in currentBagContents)
            {
                if (FindBagsCount(bags, innerBagContents, bagToFind))
                {
                    return true;
                }
            }

            return false;
        }

        public override string Solve_2()
        {
            var bags = _input.SplitNewLine().ToImmutableDictionary(
                input => Regex.Match(input, @"^(.*?) bag(s|) contain .*").Groups[1].Value,
                input => Regex.Matches(input, @"((\d) (.*?) bag(s|))").Select(x => (Count: int.Parse(x.Groups[2].Value), Name: x.Groups[3].Value)).ToArray());

            return (FindBagsCount2(bags, "shiny gold")-1).ToString();
        }

        private int FindBagsCount2(ImmutableDictionary<string, (int Count, string Name)[]> bags, string shinyGold)
        {
            var bag = bags[shinyGold];
            return 1 + bag.Select(x => x.Count * FindBagsCount2(bags, x.Name)).Sum();
        }
    }
}