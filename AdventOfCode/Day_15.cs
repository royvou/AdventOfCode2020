using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace AdventOfCode
{
    public class Day_15 : BaseDay
    {
        public Day_15()
        {
        }

        public Day_15(string input) : base(input)
        {
        }

        public override string Solve_1()
            => NthNumber(2020);

        private string NthNumber(int number)
            => GetEnumeratorFor(_input.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).AsInt(), number + 1).Skip(number - 1).FirstOrDefault().ToString();

        private IEnumerable<int> GetEnumeratorFor(IEnumerable<int> input, int maxTurns = int.MaxValue)
        {
            var history = new Dictionary<int, (int SecondLastIndex, int LatestIndex)>();

            int turn = 1;
            int lastNumber = 0;

            foreach (var number in input)
            {
                history[number] = (-1,turn);
                lastNumber = number;
                yield return lastNumber;

                turn += 1;
            }

            while (turn < maxTurns)
            {
                var historyOfLastSpoken = history[lastNumber];
                int toSpeak;
                if (historyOfLastSpoken.SecondLastIndex == -1)
                {
                    toSpeak = 0;
                }
                else
                {
                    toSpeak = historyOfLastSpoken.LatestIndex - historyOfLastSpoken.SecondLastIndex;
                    
                }

                lastNumber = toSpeak;
                if (history.TryGetValue(toSpeak, out var currentQueue))
                {
                    history[toSpeak] = (currentQueue.LatestIndex, turn);
                }
                else
                {
                    history[toSpeak]= (-1,turn);
                }

                yield return lastNumber;
                turn += 1;
            }
        }


        public override string Solve_2()
            => NthNumber(30000000);
    }
}