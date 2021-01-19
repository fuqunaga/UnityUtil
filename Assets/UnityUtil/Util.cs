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
    }
}
