using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace UnityUtil
{
    namespace Primitive2D
    {
        public static class Primitive2DDebug
        {
            public static void Draw(Primitive2DBase pb, Color col = default(Color))
            {
                var points = pb.GetPoints();
                for (var i = 0; i < points.Length - 1; ++i)
                {
                    Debug.DrawLine(points[i], points[i + 1], col);
                }
            }
        }

        public abstract class Primitive2DBase
        {
            public Vector2 Scale { get; }

            public Vector2 Center { get; }

            List<Vector2> _points;

            protected Primitive2DBase(Vector2 scale, Vector2 center)
            {
                Scale = scale;
                Center = center;
            }

            protected abstract IEnumerable<Vector2> CalcPoints();

            public Vector2[] GetPoints(bool loop = true)
            {
                if (null == _points)
                {
                    _points = CalcPoints().ToList();
                    if (loop) _points.Add(_points[0]);
                }
                return _points.ToArray();
            }
        }

        public class Circle : Primitive2DBase
        {
            public int PointNum { get; }

            public Circle(float radius, int pointNum, Vector2 center = default)
                : this(Vector2.one * radius, pointNum, center)
            {
            }

            public Circle(Vector2 scale, int pointNum, Vector2 center = default)
                : base(scale, center)
            {
                PointNum = pointNum;
            }

            protected override IEnumerable<Vector2> CalcPoints()
            {
                var radDelta = Mathf.PI * 2f / PointNum;
                for (var i = 0; i < PointNum; ++i)
                {
                    var rad = radDelta * i;
                    yield return new Vector2(
                                     Mathf.Cos(rad) * Scale.x,
                                     Mathf.Sin(rad) * Scale.y
                                 )
                                 + Center;
                }
            }
        }
    }
}