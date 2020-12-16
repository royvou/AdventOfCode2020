namespace AdventOfCode
{
    public static class MathUtilities
    {
        public static long PowerOf2(int powerOf) => 1L << powerOf;

        public static bool IsBetween(in int number, in int validRangeMinValue, in int validRangeMaxValue)
            => number >= validRangeMinValue && validRangeMaxValue >= number;
    }
}