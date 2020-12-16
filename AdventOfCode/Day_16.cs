using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_16 : BaseDay
    {
        public Day_16()
        {
        }

        public Day_16(string input) : base(input)
        {
        }

        public override string Solve_1()
        {
            List<Day16Group> groups = new();
            Day16Ticket myTicket = default;
            List<Day16Ticket> tickets = new();

            var parseArea = ParseArea.Groups;

            foreach (var line in _input.SplitNewLine())
            {
                //Group
                var match = Regex.Match(line, @"([\w ]*): (\d*)-(\d*) or (\d*)-(\d*)");
                if (match.Success)
                {
                    groups.Add(new Day16Group(match.Groups[1].Value, new[]
                    {
                        new Day16GValidRange(match.Groups[2].Value.AsInt(), match.Groups[3].Value.AsInt()),
                        new Day16GValidRange(match.Groups[4].Value.AsInt(), match.Groups[5].Value.AsInt())
                    }));
                    continue;
                }

                if (line.StartsWith("your ticket:"))
                {
                    parseArea = ParseArea.Ticket;
                    continue;
                }

                if (line.StartsWith("nearby tickets:"))
                {
                    parseArea = ParseArea.Tickets;
                    continue;
                }

                var currentTicket = new Day16Ticket(line.Split(',').AsInt().ToArray());
                switch (parseArea)
                {
                    case ParseArea.Ticket:
                        myTicket = currentTicket;
                        break;

                    case ParseArea.Tickets:
                        tickets.Add(currentTicket);
                        break;
                }
            }

            var validRanges = groups.SelectMany(x => x.ValidRange).ToList();
            return tickets.SelectMany(x => GetInvalidNumbers(x, validRanges)).Sum().ToString();
        }

        private IEnumerable<int> GetInvalidNumbers(Day16Ticket day16Ticket, List<Day16GValidRange> validRanges)
            => day16Ticket.Numbers.Where(x => !validRanges.Any(y => MathUtilities.IsBetween(x, y.MinValue, y.MaxValue)));

        public override string Solve_2()
        {
            List<Day16Group> groups = new();
            Day16Ticket myTicket = default;
            List<Day16Ticket> tickets = new();

            //Parse
            var parseArea = ParseArea.Groups;
            foreach (var line in _input.SplitNewLine())
            {
                //Group
                var match = Regex.Match(line, @"([\w ]*): (\d*)-(\d*) or (\d*)-(\d*)");
                if (match.Success)
                {
                    groups.Add(new Day16Group(match.Groups[1].Value, new[]
                    {
                        new Day16GValidRange(match.Groups[2].Value.AsInt(), match.Groups[3].Value.AsInt()),
                        new Day16GValidRange(match.Groups[4].Value.AsInt(), match.Groups[5].Value.AsInt())
                    }));
                    continue;
                }

                if (line.StartsWith("your ticket:"))
                {
                    parseArea = ParseArea.Ticket;
                    continue;
                }

                if (line.StartsWith("nearby tickets:"))
                {
                    parseArea = ParseArea.Tickets;
                    continue;
                }

                var currentTicket = new Day16Ticket(line.Split(',').AsInt().ToArray());
                switch (parseArea)
                {
                    case ParseArea.Ticket:
                        myTicket = currentTicket;
                        break;

                    case ParseArea.Tickets:
                        tickets.Add(currentTicket);
                        break;
                }
            }

            // Validate (Part 1)
            var validRanges = groups.SelectMany(x => x.ValidRange).ToList();
            var validTickets = tickets.Where(x => !GetInvalidNumbers(x, validRanges).Any()).ToList();

            var possiblyFieldMappings = new Dictionary<string, List<int>>();
            foreach (var group in groups)
            {
                for (int index = 0; index < validTickets[0].Numbers.Length; index++)
                {
                    var success =
                        validTickets.All(ticket => group.ValidRange.Any(validrange => MathUtilities.IsBetween(ticket.Numbers[index], validrange.MinValue, validrange.MaxValue)));

                    if (!success) continue;

                    if (possiblyFieldMappings.ContainsKey(@group.GroupName))
                    {
                        possiblyFieldMappings[@group.GroupName].Add(index);
                    }
                    else
                    {
                        possiblyFieldMappings[@group.GroupName] = new List<int>();
                    }
                }
            }
            // Set Valid Fields
            var fieldMappings = new Dictionary<string,int>();
            foreach (var possiblyFieldMapping in possiblyFieldMappings)
            {
                if (possiblyFieldMapping.Value.Count == 1)
                {
                    fieldMappings[possiblyFieldMapping.Key] = possiblyFieldMapping.Value[0];
                }   
            }

            //Get the 'Sure" values first and remove  them from the other values
            var mapping = new Dictionary<string, int>();
            foreach(var map in possiblyFieldMappings.OrderBy(x => x.Value.Count))
            {
                mapping.Add(map.Key, map.Value.FirstOrDefault(v => !mapping.ContainsValue(v)));
            }

            var indexes = mapping.Where(x => x.Key.StartsWith("departure"));

            return indexes.Select(x => myTicket.Numbers[x.Value]).Aggregate(1L, (x, y) => x * y).ToString();
        }
    }

    public record Day16Group(string GroupName, Day16GValidRange[] ValidRange);

    public record Day16Ticket(int[] Numbers);

    public record Day16GValidRange(int MinValue, int MaxValue);

    public enum ParseArea
    {
        Groups,
        Ticket,
        Tickets
    }
}