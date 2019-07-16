using UnityEngine;

namespace UnityUtil
{

    [System.Serializable]
    public class KeepTimeFlag
    {
        public float switchTime = 5f;
        bool flag = false;
        float? startTime;

        public KeepTimeFlag(float switchTime = 5f) { this.switchTime = switchTime; }

        public float? time => Time.time - startTime;

        public void Set(bool f)
        {
            if (f)
            {
                if (!startTime.HasValue) startTime = Time.time;
                if (time >= switchTime)
                {
                    flag = f;
                }
            }
            else {
                flag = f;
                startTime = null;
            }
        }

        public bool Get()
        {
            return flag;
        }

        public void Clear() { Set(false); }

        public bool Check(float offset = 0f)
        {
            Set(true);
            return offset == 0f ? Get() : time >= (switchTime + offset);
        }
        public void Reset() { Set(false); Set(true); }
        public void Reset(float switchTime) { this.switchTime = switchTime; Reset(); }

        public static implicit operator bool(KeepTimeFlag x) { return x.Get(); }

        public bool raw { get { return startTime.HasValue; } }

        // あとremain秒でTrueになるように強制
        public void SetRemain(float remain)
        {
            // Time.time+remain - startTime > switchTime
            // より Time.time+remain - switchTime > startTime
            startTime = Time.time + remain - switchTime; ;
        }

        public void ForceTrue() { SetRemain(0f); }
    }
}