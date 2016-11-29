using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

static public class VectorExtension
{
    static public Vector3 Abs(this Vector3 v)
    {
        return new Vector3(
            Mathf.Abs(v.x),
            Mathf.Abs(v.y),
            Mathf.Abs(v.z)
           );
    }
}
