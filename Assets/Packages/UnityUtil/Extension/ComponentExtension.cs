using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityUtil
{
    static public class ComponentExtension
    {
        static public List<T> GetComponentsAll<T>(this Component c) where T : Component
        {
            return c.transform.GetComponentsAll<T>();

        }
    }
}