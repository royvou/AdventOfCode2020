using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            var result = PlayGame(players, out Day22Player winner);
            return CalculateScore(winner).ToString();
        }

        private static int CalculateScore(Day22Player winner)
        {
            return winner.Cards.Reverse().Select((item, index) => (index + 1) * item.Value).Sum();
        }

        private bool PlayGame(IList<Day22Player> players, out Day22Player gameWinner)
        {
            while (players.Count(x => x.Cards.Count > 0) > 1)
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

            gameWinner = players.OrderByDescending(x => x.Cards.Count).FirstOrDefault();
            return true;
        }

        private List<Day22Player> ParsePlayers(string input) =>
            input.SplitDoubleNewLine().Select(lines =>
            {
                var input = lines.SplitNewLine();
                var name = input[0];
                var cards = input[1..].AsInt().Select(x => new Day22Card(x));

                return new Day22Player(name, new Queue<Day22Card>(cards));
            }).ToList();

        public override string Solve_2()
        {
            var players = ParsePlayers(_input);
            PlayGameRecursive(players, out var winner);
            return CalculateScore(winner).ToString();
        }

        private bool PlayGameRecursive(List<Day22Player> players, out Day22Player gameWinner)
        {
            HashSet<string> playedGameStates = new();
            while (players.All(x => x.Cards.Count != 0))
            {
                // if there was a previous round in this game that had exactly the same cards in the same order in the same players' decks, the game instantly ends in a win for player 1.
                IDictionary<Day22Player, Day22Card> cardPlayers = new Dictionary<Day22Player, Day22Card>();
                Day22Player winner;
                List<Day22Card> cards = new();

                HashSet<string> currentStates = new HashSet<string>(players.Select(player => GenerateState(player)));

                if (playedGameStates.Overlaps(currentStates))
                {
                    winner = players[0];
                    cards.Add(players[0].Cards.Dequeue());
                    foreach (var player in players.Skip(1))
                    {
                        cards.Add(player.Cards.Dequeue());
                    }

                    gameWinner = winner;
                    return true;
                }
                else
                {
                    foreach (var player in players)
                    {
                        var currentCard = player.Cards.Dequeue();
                        cardPlayers.Add(player, currentCard);
                    }

                    if (cardPlayers.All(player => player.Value.Value <= player.Key.Cards.Count))
                    {
                        //RECURSIVE!
                        var recursePlayers = players.Select(x => x.Duplicate(cardPlayers[x].Value)).ToList();
                        PlayGameRecursive(recursePlayers, out var recurseWinner);

                        winner = players.FirstOrDefault(x => x.Name == recurseWinner.Name);

                        cards.Add(cardPlayers[winner]);
                        foreach (var loser in cardPlayers.Where(x => x.Key.Name != winner.Name))
                        {
                            cards.Add(cardPlayers[loser.Key]);
                        }
                    }
                    else
                    {
                        // Otherwise, at least one player must not have enough cards left in their deck to recurse; the winner of the round is the player with the higher-value card.
                        winner = cardPlayers.OrderByDescending(x => x.Value.Value).FirstOrDefault().Key;
                        cards = cardPlayers.Values.OrderByDescending(x => x.Value).ToList();
                    }
                }


                foreach (var card in cards)
                {
                    winner.Cards.Enqueue(card);
                }

                playedGameStates.UnionWith(currentStates);
            }

            gameWinner = players.OrderByDescending(x => x.Cards.Count).FirstOrDefault();
            return true;
        }


        private string GenerateState(Day22Player player)
        {
            var sb = new StringBuilder();
            foreach (var card in player.Cards)
            {
                sb.Append(card.Value.ToString()).Append("|");
            }

            return sb.ToString();
        }
    }

    public record Day22Player(string Name, Queue<Day22Card> Cards)
    {
        public Day22Player Duplicate(int numberOfCards) => this with {Cards = new Queue<Day22Card>(Cards.Take(numberOfCards))};
    };

    public record Day22Card(int Value);
}