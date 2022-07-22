using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace UnityUtil
{
    [Obsolete("Please use " + nameof(RandomHSVForMaterialPropertyBlock) + " instead.")]
    public class RandomHSV{}

    public class RandomHSVForMaterialPropertyBlock : MaterialPropertySetter<Color>
    {
        [FormerlySerializedAs("_min")] public Color min;
        [FormerlySerializedAs("_max")] public Color max;

        [FormerlySerializedAs("_easingH")] public EasingType easingH;

        protected virtual float Rand() => Random.value;

        protected override Color GetValue()
        {
            var low = min.ToHSV();
            var high = max.ToHSV();

            var hsv = new Vector3(
                Mathf.Lerp(low.x, high.x, Easing.easeWithType(easingH, Rand())),
                Mathf.Lerp(low.y, high.y, Rand()),
                Mathf.Lerp(low.z, high.z, Rand())
            );

            var col = Color.HSVToRGB(hsv.x, hsv.y, hsv.z);
            col.a = Mathf.Lerp(min.a, max.a, Rand());

            return col;
        }
    }
}