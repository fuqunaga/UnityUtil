using UnityEngine;
using System.Collections;

static class PlayerPrefsUtil
{
    static public Rect GetRect(string key) { return GetRect(key, new Rect()); }
    static public Rect GetRect(string key, Rect defaultValue)
    {
        return new Rect(
            PlayerPrefs.GetFloat(key + "RectX", defaultValue.x),
            PlayerPrefs.GetFloat(key + "RectY", defaultValue.y),
            PlayerPrefs.GetFloat(key + "RectWidth", defaultValue.width),
            PlayerPrefs.GetFloat(key + "RectHeight", defaultValue.height)
            );
    }

    static public void SetRect(string key, Rect rect)
    {
        PlayerPrefs.SetFloat(key + "RectX", rect.x);
        PlayerPrefs.SetFloat(key + "RectY", rect.y);
        PlayerPrefs.SetFloat(key + "RectWidth", rect.width);
        PlayerPrefs.SetFloat(key + "RectHeight", rect.height);
    }
}