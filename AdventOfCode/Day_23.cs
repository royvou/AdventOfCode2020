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
            //var cards = _input.Select(x => x.AsInt()).ToList();
            var cards = new LinkedList<int>(); //).ToList();
            foreach (var item in _input.Select(x => x.AsInt()))
            {
                cards.AddLast(item);
            }

            PlayGame(cards, 100);
            return CalculateScore(cards);
        }

        public string Solve_1(int rounds)
        {
            //var cards = _input.Select(x => x.AsInt()).ToList();
            var cards = new LinkedList<int>(); //).ToList();
            foreach (var item in _input.Select(x => x.AsInt()))
            {
                cards.AddLast(item);
            }

            PlayGame(cards, rounds);
            return CalculateScore(cards);
        }

        private string CalculateScore(LinkedList<int> cardsLL)
        {
            var cards = cardsLL.ToList();
            var indexOfOne = cards.IndexOf(1);

            var sb = new StringBuilder();
            for (int i = 0; i < cards.Count - 1; i++)
            {
                sb.Append(cards[(indexOfOne + i + 1) % cards.Count]);
            }

            return sb.ToString();
        }

        public override string Solve_2()
        {
            var cards = new LinkedList<int>(); //).ToList();
            foreach (var item in _input.Select(x => x.AsInt()))
            {
                cards.AddLast(item);
            }

            foreach (var item in Enumerable.Range(cards.Count + 1, 1_000_000 - cards.Count))
            {
                cards.AddLast(item);
            }


            PlayGame(cards, 10_000_000);
            return CalculateScore2(cards);
        }

        private void PlayGame(LinkedList<int> cards, int rounds)
        {
            int totalCards = cards.Count;
            var cardLookup = new Dictionary<int, LinkedListNode<int>>();
            var node = cards.First;

            do
            {
                cardLookup[node.Value] = node;
                node = node.Next;
            } while (node != null);

            LinkedListNode<int> currentLabel = cards.First;
            for (var currentCupLoop = 0; currentCupLoop < rounds; currentCupLoop++)
            {
                var lltr = new LinkedList<int>();

                for (int i = 0; i < 3; i++)
                {
                    var card = currentLabel.NextOrFirst();
                    cards.Remove(card);
                    lltr.AddLast(card);
                }


                LinkedListNode<int> destinationlabel = null;
                int subtractValue = 1;
                int toLookup = currentLabel.Value - subtractValue;
                do
                {
                    if (toLookup <= 0)
                    {
                        toLookup = totalCards;
                    }

                    if (cardLookup.TryGetValue(toLookup, out var lookedUp) && lookedUp.List == cards)
                    {
                        destinationlabel = lookedUp;
                    }


                    toLookup--;
                } while (destinationlabel == null);

                for (int i = 0; i < 3; i++)
                {
                    var card = lltr.Last; //Since we add them front-back we need to go back-front for adding
                    lltr.Remove(card);
                    cards.AddAfter(destinationlabel, card);
                }

                currentLabel = currentLabel.NextOrFirst();
            }
        }


        private string CalculateScore2(LinkedList<int> cards)
            => cards.SkipWhile(x => x != 1).Take(3).Aggregate(1L, (x, y) => x * y).ToString();
    }
}