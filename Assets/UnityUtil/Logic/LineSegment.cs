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


        // 点との距離
        // https://qiita.com/boiledorange73/items/bcd4e150e7caa0210ee6
        public float Distance(Vector2 pos)
        {
            var x0 = pos.x;
            var y0 = pos.y;
            var x1 = start.x;
            var y1 = start.y;
            var x2 = end.x;
            var y2 = end.y;

            var a = x2 - x1;
            var b = y2 - y1;
            var r2 = a * a + b * b;
            var tt = -(a * (x1 - x0) + b * (y1 - y0));
            if (tt < 0)
            {
                return (x1 - x0) * (x1 - x0) + (y1 - y0) * (y1 - y0);
            }
            if (tt > r2)
            {
                return (x2 - x0) * (x2 - x0) + (y2 - y0) * (y2 - y0);
            }
            var f1 = a * (y1 - y0) - b * (x1 - x0);
            return (f1 * f1) / r2;
        }

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