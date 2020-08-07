using UnityEngine;
using System.Collections;

public static class ColorExtension 
{
    public static Vector3 ToHSV(this Color color) {
        Vector3 hsv = Vector3.zero;
        Color.RGBToHSV(color, out hsv.x, out hsv.y, out hsv.z);
        return hsv;
    }

}
