using UnityEngine;

namespace UnityUtil
{
    [System.Serializable]
    public class RandParam
    {

        public float offset = 0f;
        public float randRange = 0f;

        public float max { get { return offset + randRange; } }

        public float value { get { return _value ?? Rand(); } }
        float? _value;

        public float Rand()
        {
            _value = offset + Random.Range(-1f, 1f) * randRange;
            return _value.Value;
        }

        public RandParam() { }
        public RandParam(float offset) { this.offset = offset; }
    }
}