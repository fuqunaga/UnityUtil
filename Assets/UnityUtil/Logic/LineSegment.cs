using System;
using UnityEngine;


namespace UnityUtil
{
    [Serializable]
    public struct LineSegment
    {
        public Vector2 start;
        public Vector2 end;

        public override string ToString()
        {
            return $"({start},{end})";
        }

        public float ClosestPointRate(Vector2 pos)
        {
            var ab = end - start;
            var r2 = Vector2.Dot(ab, ab);
            var tt = -Vector2.Dot(ab, start - pos);
            return Mathf.Clamp01(tt / r2);
        }

        // p = (at+x1, bt+y1)
        // https://zenn.dev/boiledorange73/articles/0037-js-distance-pt-seg
        public Vector2 ClosestPoint(Vector2 pos)
        {
            var ab = end - start;
            return ab * ClosestPointRate(pos) + start;
        }


        public float Distance(Vector2 pos) => Vector2.Distance(ClosestPoint(pos), pos);


        public Vector2 CalcNormal(Vector2 pos)
        {
            var toEndDir = (end - start).normalized;
            var toPos = pos - start;
            return (toPos - Vector2.Dot(toPos, toEndDir) * toEndDir).normalized;
        }

        public bool IsLeft(Vector2 pos)
        {
            var toEnd = end - start;
            var toPos = pos - start;

            return Vector3.Cross(toEnd, toPos).z > 0f;
        }

        //交点を求める
        //https://www.hiramine.com/programming/graphics/2d_segmentintersection.html
        //https://gist.github.com/yoshiki/7702066
        public static Vector2? CalcIntersection(LineSegment lhs, LineSegment rhs)
        {
            var a = lhs.start;
            var b = lhs.end;
            var c = rhs.start;
            var d = rhs.end;

            var div = (b.x - a.x) * (d.y - c.y) - (b.y - a.y) * (d.x - c.x);
            if (div == 0f) return null;

            var ac = c - a;
            var u = ((d.y - c.y) * ac.x - (d.x - c.x) * ac.y) / div;
            var s = ((b.y - a.y) * ac.x - (b.x - a.x) * ac.y) / div;

            if (u < 0f || 1f < u) return null;
            if (s < 0f || 1f < s) return null;

            return a + u * (b - a);
        }
    }
}