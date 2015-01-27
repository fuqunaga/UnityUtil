using UnityEngine;
using System.Collections;
using System.Linq;

static public class Collision2DExtensions {
	
	static public Vector2 GetMeanNormal(this Collision2D a)
	{
		return a.contacts.Aggregate(Vector2.zero, (sum, point)=> sum+point.normal).normalized;
	}
	
}
