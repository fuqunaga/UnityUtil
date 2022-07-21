namespace UnityUtil
{
    [System.Serializable]
    public class RandFloat : Rand<float>
    {
        public RandFloat() : this(0f, 0f) { }
        public RandFloat(float min, float max) : base(min, max) { }
        protected override float _rand(float minInclusive, float maxInclusive) { return UnityEngine.Random.Range(minInclusive, maxInclusive); }
    }

    [System.Serializable]
    public class RandInt : Rand<int>
    {
        public RandInt() : this(0, 0) { }
        public RandInt(int min, int max) : base(min, max) { }
        protected override int _rand(int minInclusive, int maxExclusive) => UnityEngine.Random.Range(minInclusive, maxExclusive + 1);
    }

    public abstract class Rand<T> where T : struct
    {
        public T min;
        public T max;
        T? _value;

        public T Value => _value ?? Calc();


        protected Rand(T min, T max) { this.min = min; this.max = max; }

        public T Calc()
        {
            _value = _rand(min, max);
            return _value.Value;
        }

        protected abstract T _rand(T minInclusive, T maxInclusive);
    }
}