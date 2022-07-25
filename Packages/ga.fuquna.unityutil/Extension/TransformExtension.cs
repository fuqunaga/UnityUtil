using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityUtil
{

    public static class TransformExtension
    {
        public static IEnumerable<T> GetComponentsAll<T>(this Transform trans) where T : Component 
            => trans.GetComponents<T>()
                .Concat(
                    trans.GetChildren().SelectMany(GetComponentsAll<T>)
                );
        
        public static IEnumerable<Transform> GetChildren(this Transform trans)
        {
            var num = trans.childCount;
            for (var i = 0; i < num; ++i)
            {
                yield return trans.GetChild(i);
            }
        }

        public static IEnumerable<Transform> GetDescendants(this Transform transform)
        {
            foreach (Transform trans in transform)
            {
                yield return trans;
                foreach (var t in GetDescendants(trans))
                {
                    yield return t;
                }
            }
        }

        public static void Reset(this Transform trans)
        {
            trans.position = Vector3.zero;
            trans.rotation = Quaternion.identity;
            trans.localScale = Vector3.one;
        }
    }
}