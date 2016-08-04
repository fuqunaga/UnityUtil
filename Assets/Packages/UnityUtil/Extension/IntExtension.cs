﻿using UnityEngine;
using System.Collections;

static public class IntExtension
{
    static public void times(this int a, System.Action action)
    {
        for (var i = 0; i < a; ++i)
        {
            action();
        }
    }

    static public void times(this int a, System.Action<int> action)
    {
        for (var i = 0; i < a; ++i)
        {
            action(i);
        }
    }

}