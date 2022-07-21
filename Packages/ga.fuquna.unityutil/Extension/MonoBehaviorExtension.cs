using UnityEngine;

namespace UnityUtil
{

    static public class MonoBehaviourExtension
    {
        public static GameObject InstantiateChild(this MonoBehaviour mono, Object original)
        {
            return mono.InstantiateChild(original, Vector3.zero, Quaternion.identity);
        }

        public static GameObject InstantiateChild(this MonoBehaviour me, Object original, Vector3 pos, Quaternion rot)
        {
            var go = Object.Instantiate(original, pos, rot) as GameObject;
            if (go) go.transform.SetParent(me.transform);
            return go;
        }
    }
}