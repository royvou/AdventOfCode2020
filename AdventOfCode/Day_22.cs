using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode
{
    public class Day_22 : BaseDay
    {
        public Day_22()
        {
        }

        public Day_22(string input) : base(input)
        {
        }


        public override string Solve_1()
        {
            var players = ParsePlayers(_input);
            var result = PlayGame(players);
            var winner = players.FirstOrDefault(x => x.Cards.Count > 0);
            return winner.Cards.Reverse().Select((item, index) => (index + 1) * item.Value).Sum().ToString();
        }

        private bool PlayGame(IList<Day22Player> players)
        {
            //while (players.Count(x => x.Cards.Count > 1) > 1)
            while(players.Count(x => x.Cards.Count > 0) > 1)
            {
                IDictionary<Day22Player, Day22Card> cardPlayers = new Dictionary<Day22Player, Day22Card>();
                foreach (var player in players)
                {
                    var currentCard = player.Cards.Dequeue();

                    cardPlayers.Add(player, currentCard);
                }

                var highestPlayerCard = cardPlayers.OrderByDescending(x => x.Value.Value).FirstOrDefault();

                foreach (var card in cardPlayers.Values.OrderByDescending(x => x.Value))
                {
                    highestPlayerCard.Key.Cards.Enqueue(card);
                }
            }

            return true;
        }

        private IList<Day22Player> ParsePlayers(string input)
        {
            return input.SplitDoubleNewLine().Select(lines =>
            {
                var input = lines.SplitNewLine();
                var name = input[0];
                var cards = input[1..].AsInt().Select(x => new Day22Card(x));

                return new Day22Player(name, new Queue<Day22Card>(cards));
            }).ToList();
        }

        public override string Solve_2()
        {
            throw new NotImplementedException();
        }
    }

    public record Day22Player(string Name, Queue<Day22Card> Cards);

    public record Day22Card(int Value);
}