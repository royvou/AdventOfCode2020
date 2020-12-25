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
            var cardLookup = new LinkedListNode<int>[cards.Count + 1]; //new Dictionary<int, LinkedListNode<int>>();
            var node = cards.First;

            do
            {
                cardLookup[node.Value] = node;
                node = node.Next;
            } while (node != null);

            LinkedListNode<int> current = cards.First;

            //Array to store current nodes in
            var links = new LinkedListNode<int>[3];

            for (var currentCupLoop = 0; currentCupLoop < rounds; currentCupLoop++)
            {
                for (int i = 0; i < 3; i++)
                {
                    var card = current.NextOrFirst();
                    cards.Remove(card);
                    links[i] = card;
                }


                LinkedListNode<int> destination = null;
                int subtractValue = 1;
                int toLookup = current.Value - subtractValue;
                do
                {
                    if (toLookup <= 0)
                    {
                        toLookup = totalCards;
                    }

                    var lookedUp = cardLookup[toLookup];
                    if (lookedUp != default && lookedUp.List == cards)
                    {
                        destination = lookedUp;
                    }


                    toLookup--;
                } while (destination == null);

                for (int i = links.Length - 1; i >= 0; i--)
                {
                    var card = links[i];
                    cards.AddAfter(destination, card);
                }

                current = current.NextOrFirst();
            }
        }


        private string CalculateScore2(LinkedList<int> cards)
            => cards.SkipWhile(x => x != 1).Take(3).Aggregate(1L, (x, y) => x * y).ToString();
    }
}