using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AdventOfCode
{
    public static class LinkedListExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinkedListNode<T> NextOrFirst<T>(this LinkedListNode<T> current) 
            => current.Next ?? current.List.First;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinkedListNode<T> NextOrFirst<T>(this LinkedListNode<T> current, LinkedList<T> list) 
            => current.Next ?? list.First;
    }
}