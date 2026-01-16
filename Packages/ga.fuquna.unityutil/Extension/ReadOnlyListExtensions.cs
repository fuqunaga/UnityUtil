using System;
using System.Collections.Generic;

namespace UnityUtil
{
    public static class ReadOnlyListExtensions
    {
        public static int IndexOf<T>(this IReadOnlyList<T> list, T element)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(list[i], element))
                {
                    return i;
                }
            }
            return -1;
        }
        
        public static int LastIndexOf<T>(this IReadOnlyList<T> list, T element)
        {
            for (var i = list.Count - 1; i >= 0; i--)
            {
                if (EqualityComparer<T>.Default.Equals(list[i], element))
                {
                    return i;
                }
            }
            return -1;
        }
        
        public static int FindIndex<T>(this IReadOnlyList<T> list, Predicate<T> match)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (match(list[i]))
                {
                    return i;
                }
            }
            return -1;
        }
        
        public static int FindLastIndex<T>(this IReadOnlyList<T> list, Predicate<T> match)
        {
            for (var i = list.Count - 1; i >= 0; i--)
            {
                if (match(list[i]))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}