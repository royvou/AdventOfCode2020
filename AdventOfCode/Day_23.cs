using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day_23 : BaseDay
    {
        public Day_23()
        {
        }

        public Day_23(string input) : base(input)
        {
        }

        public override string Solve_1()
        {
            var cards = _input.Select(x => x.AsInt()).ToList();

            PlayGame(cards, 100);
            return CalculateScore(cards);
        }

        public string Solve_1(int rounds)
        {
            var cards = _input.Select(x => x.AsInt()).ToList();

            PlayGame(cards, rounds);
            return CalculateScore(cards);
        }

        private void PlayGame(List<int> cards, int rounds)
        {
            for (var currentCupLoop = 0; currentCupLoop < rounds; currentCupLoop++)
            {
                var currentCupIndex = 0;

                var currentLabel = cards[currentCupIndex];
                var cardsToRemove = cards.Skip(currentCupIndex + 1).Take(3).ToList();

                cards.RemoveRange(currentCupIndex + 1, 3);

                int destinationlabel = -1;
                int subtractValue = 1;
                do
                {
                    var toLookup = currentLabel - subtractValue;
                    if (toLookup == 0)
                    {
                        destinationlabel = cards.IndexOf(cards.OrderByDescending(x => x).First());
                    }
                    else
                    {
                        destinationlabel = cards.Contains(toLookup) ? cards.IndexOf(toLookup) : -1;
                    }

                    subtractValue++;
                } while (destinationlabel == -1);

                cards.InsertRange(destinationlabel + 1, cardsToRemove);

                //Reorder
                cards.Add(cards[0]);
                cards.RemoveAt(0);
            }
        }

        private string CalculateScore(List<int> cards)
        {
            var indexOfOne = cards.IndexOf(1);

            var sb = new StringBuilder();
            for (int i = 0; i < cards.Count - 1; i++)
            {
                sb.Append(cards[(indexOfOne + i + 1) % cards.Count]);
            }

            return sb.ToString();
        }

        public override string Solve_2() => throw new System.NotImplementedException();
    }
}