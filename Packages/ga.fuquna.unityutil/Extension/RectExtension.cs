using UnityEngine;

namespace UnityUtil
{
    public static class RectExtension
    {
        public static void Encapsulate(ref this Rect rect, Vector2 v)
        {
            if (v.x < rect.xMin) rect.xMin = v.x;
            if (v.x > rect.xMax) rect.xMax = v.x;
            if (v.y < rect.yMin) rect.yMin = v.y;
            if (v.y > rect.yMax) rect.yMax = v.y;
        }
        
        public static void Encapsulate(ref this Rect rect, Rect other)
        {
            var min = rect.min;
            var max = rect.max;

            var minOther = other.min;
            var maxOther = other.max;

            min.x = Mathf.Min(min.x, minOther.x);
            min.y = Mathf.Min(min.y, minOther.y);

            max.x = Mathf.Max(max.x, maxOther.x);
            max.y = Mathf.Max(max.y, maxOther.y);

            rect.min = min;
            rect.max = max;
        }

        public static void ExtendX(ref this Rect rect, float minX, float maxX)
        {
            rect.xMin = Mathf.Min(rect.xMin, minX);
            rect.xMax = Mathf.Max(rect.xMax, maxX);
        }

        public static void ExtendY(ref this Rect rect, float minY, float maxY)
        {
            rect.yMin = Mathf.Min(rect.yMin, minY);
            rect.yMax = Mathf.Max(rect.yMax, maxY);
        }
    }
}