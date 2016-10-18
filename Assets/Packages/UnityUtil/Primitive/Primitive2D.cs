using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

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
        public Vector2 scale { get { return _scale; } }
        Vector2 _scale;

        public Vector2 center { get { return _center; } }
        Vector2 _center;

        List<Vector2> _points;

        public Primitive2DBase(Vector2 scale, Vector2 center)
        {
            _scale = scale;
            _center = center;
        }

        protected abstract List<Vector2> CalcPoints();

        public Vector2[] GetPoints(bool loop = true)
        {
            if (null == _points)
            {
                _points = CalcPoints();
                if (loop) _points.Add(_points[0]);
            }
            return _points.ToArray();
        }
    }

    public class Circle : Primitive2DBase
    {
        public int pointNum { get { return _pointNum; } }
        int _pointNum;

        public Circle(float radius, int pointNum, Vector2 center = default(Vector2))
            : this(Vector2.one * radius, pointNum, center)
        {
        }

        public Circle(Vector2 scale, int pointNum, Vector2 center = default(Vector2))
            : base(scale, center)
        {
            _pointNum = pointNum;
        }

        protected override List<Vector2> CalcPoints()
        {
            var ret  = new List<Vector2>();

            var radDelta = Mathf.PI * 2f / _pointNum;
            for (var i = 0; i < _pointNum; ++i)
            {
                var rad = radDelta * i;
                ret.Add(new Vector2(
                            Mathf.Cos(rad) * scale.x,
                            Mathf.Sin(rad) * scale.y
                            )
                            + center);
            }

            return ret;
        }
    }

}