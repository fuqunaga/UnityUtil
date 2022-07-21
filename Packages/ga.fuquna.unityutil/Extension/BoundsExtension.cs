using System.Linq;
using UnityEngine;

namespace UnityUtil
{
    public static class BoundsExtension
    {
        public static Vector3 InsideRandom(this Bounds me)
        {
            return new Vector3(
                    Mathf.Lerp(me.min.x, me.max.x, Random.value),
                    Mathf.Lerp(me.min.y, me.max.y, Random.value),
                    Mathf.Lerp(me.min.z, me.max.z, Random.value)
                    );
        }

        public static Bounds Transform(this Bounds me, Transform transform)
        {
            var min = me.min;
            var max = me.max;

            var ret = new Bounds(transform.TransformPoint(me.center), Vector3.zero);

            new[] {
                min,
                new Vector3(min.x, min.y, max.z),
                new Vector3(min.x, max.y, min.z),
                new Vector3(min.x, max.y, max.z),
                new Vector3(max.x, min.y, min.z),
                new Vector3(max.x, min.y, max.z),
                new Vector3(max.x, max.y, min.z),
                max
            }.ToList().ForEach(localPos =>
            {
                ret.Encapsulate(transform.TransformPoint(localPos));
            });

            return ret;
        }
    }
}