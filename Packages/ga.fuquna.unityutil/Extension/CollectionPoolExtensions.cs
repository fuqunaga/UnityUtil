using System.Collections.Generic;
using UnityEngine.Pool;

namespace UnityUtil
{
    public static class ListPool
    {
        public static PooledObject<List<T>> GetAndFill<T>(out List<T> value, IEnumerable<T> initialContents)
        {
            var pooledObject = ListPool<T>.Get(out value);
            if (initialContents != null)
            {
                value.AddRange(initialContents);
            }

            return pooledObject;
        }
    }

    public static class HashSetPool
    {
        public static PooledObject<HashSet<T>> GetAndFill<T>(out HashSet<T> value, IEnumerable<T> initialContents)
        {
            var pooledObject = HashSetPool<T>.Get(out value);
            if (initialContents != null)
            {
                foreach (var item in initialContents)
                {
                    value.Add(item);
                }
            }
            return pooledObject;
        }
    }
    
    public static class DictionaryPool
    {
        public static PooledObject<Dictionary<TKey, TValue>> GetAndFill<TKey, TValue>(out Dictionary<TKey, TValue> value, IEnumerable<KeyValuePair<TKey, TValue>> initialContents)
        {
            var pooledObject = DictionaryPool<TKey, TValue>.Get(out value);
            if (initialContents != null)
            {
                foreach (var (key, v) in initialContents)
                {
                    value.Add(key, v);
                }
            }
            return pooledObject;
        }
    }
}