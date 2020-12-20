using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_19 : BaseDay
    {
        public Day_19()
        {
        }

        public Day_19(string input) : base(input)
        {
        }

        public override string Solve_1()
        {
            var rules = new Dictionary<int, Day19Rule>();
            var rawLines = _input.SplitNewLine();
            var lines = new List<string>();
            foreach (var line in rawLines)
            {
                var regexMatch = Regex.Match(line, @"(\d*): (.*)");
                if (regexMatch.Success)
                {
                    var ruleNumber = int.Parse(regexMatch.Groups[1].Value);
                    Day19Rule rule = null;
                    var values = regexMatch.Groups[2].Value;
                    //Is Char
                    var regexMatch2 = Regex.Match(values, @"""(.*)""");
                    if (regexMatch2.Success)
                    {
                        rule = new Day19Rule(regexMatch2.Groups[1].Value[0], null);
                    }
                    else
                    {
                        //Reference
                        var references = values.Split('|', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(x => x.SplitSpace().Select(y => int.Parse(y)).ToArray()).ToArray();
                        rule = new Day19Rule(null, references);
                    }

                    rules.Add(ruleNumber, rule);
                }
                else
                {
                    lines.Add(line);
                }
            }


            return lines.Count(x => IsValidLine(rules, 0, x, 0).Success).ToString();
        }

        private ( bool Success, int AdvanceBy) IsValidLine(Dictionary<int, Day19Rule> rules, int ruleId, string inputString, int startIndex)
        {
            var currentRule = rules[ruleId];

            if (currentRule.Char.HasValue)
            {
                var matches = inputString[startIndex] == currentRule.Char.Value;
                return (matches, matches ? 1 : 0);
            }

            foreach (var reference in currentRule.References)
            {
                var match = true;
                var localIndex = startIndex;
                foreach (var rule in reference)
                {
                    var matches = IsValidLine(rules, rule, inputString, localIndex);
                    if (matches.Success)
                    {
                        localIndex += matches.AdvanceBy;
                    }
                    else
                    {
                        match = false;
                        break;
                    }
                }

                if (match)
                    //If we are "starting from 0" we need to check if we dont have any characters in input left to match   
                    return (startIndex == 0 ? inputString.Length == localIndex : true, localIndex - startIndex);
            }

            return (false, 0);
        }


        public override string Solve_2() => throw new NotImplementedException();
    }

    public record Day19Rule(char? Char, int[][] References);
}