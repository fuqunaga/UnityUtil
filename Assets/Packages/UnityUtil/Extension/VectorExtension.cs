﻿using UnityEngine;

namespace UnityUtil
{

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
}