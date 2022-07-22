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
    }
}