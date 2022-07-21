using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace UnityUtil
{

    public static class EnumerableExtension
    {
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunkSize)
        {
            using var pool = ListPool<T>.Get(out var list);
            list.AddRange(source);
            
            for(var i=0; i<list.Count; i+=chunkSize)
            {
                yield return list.GetRange(i, chunkSize);
            }
        }

        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null)
                return true; // or throw an exception
            return !source.Any();
        }

        public static T SelectRandom<T>(this IEnumerable<T> source)
        {
            using var pool = ListPool<T>.Get(out var list);
            list.AddRange(source);

            return list.ElementAt(Random.Range(0, list.Count()));
        }
        
        public static T SelectRandomWithWeight<T>(List<T> list, Func<T, float> getWeight)
        {
            var total = list.Aggregate(0f, (s, data) => s + getWeight(data));
            var cursor = Random.value * total;
            var ret = list.Find(data =>
            {
                cursor -= getWeight(data);
                return cursor <= 0f;
            });

            return ret;
        }

        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer = null)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            comparer ??= Comparer<TKey>.Default;

            using var sourceIterator = source.GetEnumerator();
            
            if (!sourceIterator.MoveNext())
            {
                throw new InvalidOperationException("Sequence contains no elements");
            }
            var min = sourceIterator.Current;
            var minKey = selector(min);
            while (sourceIterator.MoveNext())
            {
                var candidate = sourceIterator.Current;
                var candidateProjected = selector(candidate);
                if (comparer.Compare(candidateProjected, minKey) < 0)
                {
                    min = candidate;
                    minKey = candidateProjected;
                }
            }
            return min;
        }
    }
}