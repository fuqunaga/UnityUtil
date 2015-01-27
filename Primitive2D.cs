using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public static class Primitive2D {

	public static Circle Circle(float radius, int pointNum)
	{
		return new Circle (radius, pointNum);
	}

	public static void DrawCircle(Vector2 center, float radius, int pointNum, Color col=default(Color))
	{
		var circle = Circle(radius, pointNum);
		var points = circle.GetPoints().Select(p => p + center).ToArray();
		for(var i=0; i<points.Length-1; ++i)
		{
			Debug.DrawLine(points[i], points[i+1], col);
		}
	}
}

public class Circle
{
	float _radius;
	int _pointNum;
	List<Vector2> _points;
	
	public Circle(float radius, int pointNum)
	{
		_radius = radius;
		_pointNum = pointNum;
	}

	public Vector2[] GetPoints(bool loop = true)
	{
		if (null == _points) 
		{
			_points = new List<Vector2>();

			var radDelta = Mathf.PI * 2f / _pointNum;
			for(var i=0; i<_pointNum; ++i)
			{
				var rad = radDelta * i;
				_points.Add (new Vector2(
							Mathf.Cos(rad) * _radius, 
							Mathf.Sin(rad) * _radius
							));
			}

			if (loop ) _points.Add(_points[0]);
		}
		return _points.ToArray();
	}
}

