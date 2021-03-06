﻿using System;
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


            return lines.Where(x =>
            {
                var (success, startIndex) = IsValidLine(rules, 0, x, 0);

                return success && startIndex == x.Length;
            }).Count().ToString();
        }

        private ( bool Success, int StartIndex) IsValidLine(Dictionary<int, Day19Rule> rules, int ruleId, string inputString, int startIndex)
        {
            var currentRule = rules[ruleId];

            if (currentRule.Char.HasValue)
            {
                if (startIndex >= inputString.Length)
                {
                    return (false, startIndex);
                }

                var matches = inputString[startIndex] == currentRule.Char.Value;
                return (matches, matches ? startIndex + 1 : -1);
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
                        localIndex = matches.StartIndex;
                    }
                    else
                    {
                        match = false;
                        break;
                    }

                    //If we have matched the full string after this  check, check if its one of the recurisve items then succeed else let itfail :)
                    if (localIndex == inputString.Length && (rule == 8 || rule == 11 || rule == 31))
                    {
                        return (true, localIndex);
                    }
                }


                if (match)
                {
                    return (true, localIndex);
                }
            }

            return (false, -1);
        }


        public override string Solve_2()
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

            //Overrule part 2 rules
            rules[8] = new Day19Rule(null, new[]
            {
                new[] {42,},
                new[] {42, 8,},
            });
            rules[11] = new Day19Rule(null, new[]
            {
                new[] {42, 31,},
                new[] {42, 11, 31,},
            });

            return lines.Where(x =>
            {
                var (success, startIndex) = IsValidLine(rules, 0, x, 0);
                return success && startIndex == x.Length;
            }).Count().ToString();
        }
    }

    public record Day19Rule(char? Char, int[][] References);
}