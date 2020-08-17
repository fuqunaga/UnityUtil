namespace UnityUtil
{
    [System.Serializable]
    public class RandFloat : Rand<float>
    {
        public RandFloat() : this(0f, 0f) { }
        public RandFloat(float min, float max) : base(min, max) { }
        protected override float _rand(float min, float max) => UnityEngine.Random.Range(min, max);
    }

    [System.Serializable]
    public class RandInt : Rand<int>
    {
        public RandInt() : this(0, 0) { }
        public RandInt(int min, int max) : base(min, max) { }
        protected override int _rand(int min, int max) => UnityEngine.Random.Range(min, max + 1);
    }

    public abstract class Rand<T> where T : struct
    {
        public T _min;
        public T _max;
        T? _value;

        public T value => _value ?? Calc();


        public Rand() { }
        public Rand(T min, T max) { _min = min; _max = max; }

        public T Calc()
        {
            _value = _rand(_min, _max);
            return _value.Value;
        }

        protected abstract T _rand(T min, T max);
    }
}