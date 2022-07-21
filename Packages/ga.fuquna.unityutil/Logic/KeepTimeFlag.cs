using System;

namespace UnityUtil
{

    [Serializable]
    public class KeepTimeFlag
    {
        public float switchTime;
        bool _flag = false;
        float? _startTime;

        public KeepTimeFlag(float switchTime = 5f) { this.switchTime = switchTime; }

        public float? Time => UnityEngine.Time.time - _startTime;

        public void Set(bool f)
        {
            if (f)
            {
                _startTime ??= UnityEngine.Time.time;
                if (Time >= switchTime)
                {
                    _flag = true;
                }
            }
            else {
                _flag = false;
                _startTime = null;
            }
        }

        public bool Get() => _flag;

        public void Clear() => Set(false);

        public bool Check(float offset = 0f)
        {
            Set(true);
            return offset == 0f ? Get() : Time >= (switchTime + offset);
        }
        public void Reset() { Set(false); Set(true); }

        public void Reset(float newSwitchTime)
        {
            switchTime = newSwitchTime; 
            Reset();
        }

        public static implicit operator bool(KeepTimeFlag x) => x.Get();

        public bool raw => _startTime.HasValue;

        // あとremain秒でTrueになるように強制
        public void SetRemain(float remain)
        {
            // Time.time+remain - startTime > switchTime
            // より Time.time+remain - switchTime > startTime
            _startTime = UnityEngine.Time.time + remain - switchTime; ;
        }

        public void ForceTrue() { SetRemain(0f); }
    }
}