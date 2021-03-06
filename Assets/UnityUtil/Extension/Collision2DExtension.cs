﻿using UnityEngine;
using System.Collections;
using System.Linq;

namespace UnityUtil
{

    static public class Collision2DExtension
    {

        static public Vector2 GetMeanNormal(this Collision2D a)
        {
            return a.contacts.Aggregate(Vector2.zero, (sum, point) => sum + point.normal).normalized;
        }

    }
}