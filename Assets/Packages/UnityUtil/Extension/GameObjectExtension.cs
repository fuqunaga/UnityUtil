using UnityEngine;

public static class GameObjectExtension
{
    static public T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        return go.GetComponent<T>() ?? go.AddComponent<T>();
    }
}
