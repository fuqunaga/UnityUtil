using UnityEngine;
using System.Collections;
using System.Linq;

static public class AnimationCurveExtensions
{

    static public float LastTime(this AnimationCurve curve)
    {
        return curve.keys.Last().time;
    }

}