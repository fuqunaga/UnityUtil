using UnityEngine;


public class RandomHSV : MaterialPropertySetter<Color>
{
    public Color _min;
    public Color _max;

    public EasingType _easingH;

    protected override Color CalcValue()
    {
        Vector3 min = _min.ToHSV();
        Vector3 max = _max.ToHSV();

        Vector3 hsv = new Vector3(
            Mathf.Lerp(min.x, max.x, Easing.easeWithType(_easingH, Random.value)),
            Mathf.Lerp(min.y, max.y, Random.value),
            Mathf.Lerp(min.z, max.z, Random.value)
        );

        var col = Color.HSVToRGB(hsv.x, hsv.y, hsv.z);
        col.a = Mathf.Lerp(_min.a, _max.a, Random.value);

        return col;
    }
}
