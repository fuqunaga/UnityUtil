using UnityEngine;
using System.Collections;

[System.Serializable]
public struct LoopRate {

    public float time;

    public float value { get { return Time.time % time / time;  } }

    public bool enable { get { return time > 0f; } }

    public static implicit operator float(LoopRate l)
    {
        return l.value;
    }

}
