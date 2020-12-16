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

        private string NthNumber(in int number)
            => GetEnumeratorFor(_input.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).AsInt(), number).ToString();

        private int GetEnumeratorFor(in IEnumerable<int> input, in int maxTurns = int.MaxValue)
        {
            var history = new Dictionary<int, (int SecondLastIndex, int LatestIndex)>();

            int turn = 1;
            int lastNumber = 0;

            foreach (var number in input)
            {
                history[number] = (-1,turn);
                lastNumber = number;
                turn += 1;
            }

            while (turn <= maxTurns)
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

                turn += 1;
            }

            return lastNumber;
        }


        public override string Solve_2()
            => NthNumber(30000000);
    }
}