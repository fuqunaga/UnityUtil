using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace UnityUtil
{

    public static class EnumerableExtension
    {
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunkSize)
        {
            while (source.Any())
            {
                yield return source.Take(chunkSize);
                source = source.Skip(chunkSize);
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
        
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Func<T, float> getPriority = null)
        {
            getPriority = getPriority ?? GetPriorityDefault;
            return source.OrderBy(getPriority);
            
            float GetPriorityDefault(T x) => Random.value;
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
        
        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer = null)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            comparer ??= Comparer<TKey>.Default;

            using var sourceIterator = source.GetEnumerator();
            
            if (!sourceIterator.MoveNext())
            {
                throw new InvalidOperationException("Sequence contains no elements");
            }
            var max = sourceIterator.Current;
            var maxKey = selector(max);
            while (sourceIterator.MoveNext())
            {
                var candidate = sourceIterator.Current;
                var candidateProjected = selector(candidate);
                if (comparer.Compare(candidateProjected, maxKey) > 0)
                {
                    max = candidate;
                    maxKey = candidateProjected;
                }
            }
            return max;
        }
    }
}