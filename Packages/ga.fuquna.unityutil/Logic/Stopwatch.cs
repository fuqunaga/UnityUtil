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
        protected float elapsed;

        protected virtual float Time => UnityEngine.Time.time;

        public void Start() => last = Time;
        public void Stop() { elapsed += Elapsed; last = 0; }
        public bool IsRunning => last.HasValue;
        public float Elapsed => last.HasValue ? Time - last.Value : elapsed;
    }
}