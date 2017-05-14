using UnityEngine;

namespace UnityUtil
{

    public static class BoundsExtension
    {
        public static Vector3 InsideRandom(this Bounds bounds)
        {
            return new Vector3(
                    Mathf.Lerp(bounds.min.x, bounds.max.x, Random.value),
                    Mathf.Lerp(bounds.min.y, bounds.max.y, Random.value),
                    Mathf.Lerp(bounds.min.z, bounds.max.z, Random.value)
                    );
        }

    }
}