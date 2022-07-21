using System.Linq;
using UnityEngine;

namespace UnityUtil
{

    public static class Collision2DExtension
    {

        public static Vector2 GetMeanNormal(this Collision2D a)
        {
            return a.contacts.Aggregate(Vector2.zero, (sum, point) => sum + point.normal).normalized;
        }

    }
}