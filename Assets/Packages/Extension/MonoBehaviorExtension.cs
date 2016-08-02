using UnityEngine;
using System.Collections;

static public class MonoBehaviourExtension
{
    public static GameObject InstantiateChild(this MonoBehaviour mono, Object original, Vector3 pos, Quaternion rot)
    {
        var go = Object.Instantiate(original, pos, rot) as GameObject;
        if (go) go.transform.parent = mono.transform;
        return go;
    }
}
