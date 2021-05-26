using UnityEngine;

namespace UnityUtil
{

    public class RandomHSV : MaterialPropertySetter<Color>
    {
        public Color _min;
        public Color _max;

        public EasingType _easingH;

        protected virtual float Rand() { return Random.value; }

        protected override Color GetValue()
        {
            Vector3 min = _min.ToHSV();
            Vector3 max = _max.ToHSV();

            Vector3 hsv = new Vector3(
                Mathf.Lerp(min.x, max.x, Easing.easeWithType(_easingH, Rand())),
                Mathf.Lerp(min.y, max.y, Rand()),
                Mathf.Lerp(min.z, max.z, Rand())
            );

            var col = Color.HSVToRGB(hsv.x, hsv.y, hsv.z);
            col.a = Mathf.Lerp(_min.a, _max.a, Rand());

            return col;
        }
    }
}