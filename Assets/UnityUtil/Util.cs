using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace UnityUtil
{
    public static class Util
    {
        static public void Swap<T>(ref T lhs, ref T rhs)
        {
            (lhs, rhs) = (rhs, lhs);
        }

        static public T SelectRandomWithWeight<T>(List<T> list, System.Func<T, float> getWeight)
        {
            var total = list.Aggregate(0f, (s, data) => s + getWeight(data));
            var corsor = Random.value * total;
            var ret = list.Find(data =>
            {
                corsor -= getWeight(data);
                return corsor <= 0f;
            });

            return ret;
        }

        static public T RandomSelect<T>(T[] list)
        {
            return list[Random.Range(0, list.Length)];
        }


        public static System.Type ConvertStringToType(string typeName)
        {

            // Try Type.GetType() first. This will work with types defined
            // by the Mono runtime, etc.
            var type = System.Type.GetType(typeName);

            // If it worked, then we're done here
            if (type != null)
                return type;

            // Get the name of the assembly (Assumption is that we are using
            // fully-qualified type names)
            var assemblyName = typeName.Substring(0, typeName.IndexOf('.'));

            // Attempt to load the indicated Assembly
            var assembly = System.Reflection.Assembly.Load(assemblyName);
            if (assembly == null)
                return null;

            // Ask that assembly to return the proper Type
            return assembly.GetType(typeName);

        }

        public static T SelectWithWait<T>(List<T> list, System.Func<T, float> getWait) where T : class
        {
            var total = list.Aggregate(0f, (s, data) => s + getWait(data));
            var corsor = Random.value * total;
            var ret = list.Find(data =>
            {
                corsor -= getWait(data);
                return corsor <= 0f;
            });

            return ret;
        }


        [System.Serializable]
        public class Stopwatch
        {
            public static Stopwatch StartNew()
            {
                var s = new Stopwatch();
                s.Start();
                return s;
            }

            public float? last;
            protected float _elapsed;

            protected float time { get { return Time.time; } }

            public void Start() { last = time; }
            public void Stop() { _elapsed += Elapsed; last = 0; }
            public bool IsRunning { get { return last.HasValue; } }
            public float Elapsed { get { return IsRunning ? time - last.Value : _elapsed; } }
        }
    }
}