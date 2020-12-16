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

        public override string Solve_2() => throw new System.NotImplementedException();
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