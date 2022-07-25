using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityUtil
{
    public static class GameObjectExtension
    {
        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            if (!go.TryGetComponent<T>(out var co))
            {
                co = go.AddComponent<T>();
            }

            return co;
        }

        public static IEnumerable<GameObject> GetChildren(this GameObject gameObject) 
            => gameObject.transform.GetChildren().Select(trans => trans.gameObject);

        public static IEnumerable<GameObject> GetDescendants(this GameObject gameObject)
            => gameObject.transform.GetDescendants().Select(trans => trans.gameObject);
    }
}