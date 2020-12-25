using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_25 : BaseDay
    {
        public Day_25()
        {
        }

        public Day_25(string input) : base(input)
        {
        }

        public override string Solve_1()
        {
            var publicKeys = _input.ParseAsLongArray().ToArray();
            var loopSizes = publicKeys.Select(x => CalculateLoopSize(x)).ToList();
            return CalculateEncryptionKey(publicKeys[0], loopSizes[1]).ToString();
        }

        private long CalculateEncryptionKey(long publicKey, long loopSiz)
        {
            long encryptionKey = 1;

            for (int i = 0; i < loopSiz; i++)
            {
                encryptionKey = Calculate(publicKey, encryptionKey);
            }

            return encryptionKey;
        }

        private static long Calculate(long publicKey, long encryptionKey)
            => encryptionKey * publicKey % 20201227;

        private long CalculateLoopSize(long publicKey)
        {
            long subjectNumber = 1, loopSize = 0;

            while (publicKey != subjectNumber)
            {
                subjectNumber = Calculate(subjectNumber, 7); //subjectNumber * 7 % 20201227;
                loopSize += 1;
            }

            return loopSize;
        }

        public override string Solve_2() => bool.TrueString;
    }

    //public record Day25Device(int LoopSize, int EncryptionKey, int PublicKey);
}