using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityUtil
{
    public static class EnumExtension
    {
        public static List<T> GetValues<T>() => Enum.GetValues(typeof(T)).Cast<T>().ToList();
    }
}