namespace AdventOfCode
{
    public static class Utilities
    {
        public static bool Between(int number, int min, int max)
            => number >= min && number <= max;

        public static bool CharAt(string input, char @char, int position)
        {
            return input[position - 1] == @char;
        }
    }
}