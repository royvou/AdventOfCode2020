using System.Collections.Generic;

namespace AdventOfCode
{
    public static class LinkedListExtensions
    {
        public static LinkedListNode<T> NextOrFirst<T>(this LinkedListNode<T> current)
        {
            return current.Next ?? current.List.First;
        }
    }
}