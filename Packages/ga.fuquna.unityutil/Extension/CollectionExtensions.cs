using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityUtil
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// 指定した述語に一致する要素をすべて削除します。
        /// </summary>
        public static int RemoveWhere<T>(this ICollection<T> collection, Func<T, bool> match)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (match == null) throw new ArgumentNullException(nameof(match));

            using var _ = ListPool.GetAndFill(out var toRemove, collection.Where(match));

            foreach (var item in toRemove)
            {
                collection.Remove(item);
            }

            return toRemove.Count;
        }
    }
}