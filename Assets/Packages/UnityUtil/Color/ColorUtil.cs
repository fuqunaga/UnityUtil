using UnityEngine;
using System.Collections;

static public class ColorUtil  {

	static public Color Add(Color col, float rgb, float a = 0)
	{
		return col + new Color(rgb, rgb, rgb, a);
	}

	static public Color Clamp01(Color col)
	{
		return new Color(
			Mathf.Clamp01(col.r),
			Mathf.Clamp01(col.g),
			Mathf.Clamp01(col.b),
			Mathf.Clamp01(col.a)
		);
	}

	static public Color AddClamp01(Color col, float rgb, float a=0)
	{
		return Clamp01(Add (col, rgb, a));
	}
}
