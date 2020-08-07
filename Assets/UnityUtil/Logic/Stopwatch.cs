using UnityEngine;

namespace UnityUtil
{
    public class Stopwatch
    {
        public static Stopwatch StartNew()
        {
            var s = new Stopwatch();
            s.Start();
            return s;
        }

        public float? last;
        protected float _elapsed;

        protected float time => Time.time;

        public void Start() => last = time;
        public void Stop() { _elapsed += Elapsed; last = 0; }
        public bool IsRunning => last.HasValue;
        public float Elapsed => IsRunning ? time - last.Value : _elapsed;
    }
}