using UnityEngine;

public static class ColorExtension 
{
    public static Vector3 ToHSV(this Color color) 
    {
        var hsv = Vector3.zero;
        Color.RGBToHSV(color, out hsv.x, out hsv.y, out hsv.z);
        return hsv;
    }
}
