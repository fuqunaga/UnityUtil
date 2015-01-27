using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class IEnumerableExtension
{
    public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunksize)
    {
        while (source.Any())
        {
            yield return source.Take(chunksize);
            source = source.Skip(chunksize);
        }
    }

    public static bool IsEmpty<T>(this IEnumerable<T> source)
    {
        if (source == null)
            return true; // or throw an exception
        return !source.Any();
    }
}