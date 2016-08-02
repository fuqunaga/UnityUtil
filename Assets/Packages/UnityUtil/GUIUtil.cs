using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class GUIUtil
{
    public static class Style {
        // 参考 https://github.com/XJINE/XJUnity3D.GUI
        public static readonly GUIStyle FoldoutPanelStyle;

        static Style()
        {
            FoldoutPanelStyle = new GUIStyle(GUI.skin.label);
            FoldoutPanelStyle.normal.textColor = GUI.skin.toggle.normal.textColor;
            FoldoutPanelStyle.hover.textColor = GUI.skin.toggle.hover.textColor;

            var tex = new Texture2D(1, 1);
            tex.SetPixels(new[] { new Color(0.5f, 0.5f, 0.5f,0.5f)});
            tex.Apply();
            FoldoutPanelStyle.hover.background = tex;
        }
    }

    public class Folds : Dictionary<string, Fold>
    {
        public void Add(string name, Action action, bool enableFirst = false)
        {
            Fold fold;
            if (TryGetValue(name, out fold))
            {
                fold.Add(action);
            }
            else {
                Add(name, new Fold(name, action, enableFirst));
            }
        }

        public void OnGUI()
        {
            Values.ToList().ForEach(f => f.OnGUI());
        }
    }

    public class Fold
    {
        bool foldOpen;
        string name;
        Action draw;

        public Fold(string n, Action action, bool enableFirst = false)
        {
            name = n;
            draw += action;
            foldOpen = enableFirst;
        }

        public void Add(Action action)
        {
            draw += action;
        }

        public void OnGUI()
        {
            var foldStr = foldOpen ? "▼" : "▶";

            foldOpen ^= GUILayout.Button(foldStr + name, Style.FoldoutPanelStyle);
            if (foldOpen)
            {
                using (var v = new GUILayout.VerticalScope("window"))
                {
                    draw();

                }
            }
        }
    }

    public static T Field<T>(T v, string label = "")
    {
        var type = typeof(T);
        Func<T> func = (Func<T>)(() => (T)Convert.ChangeType(GUILayout.TextField(v.ToString()), type));

        if (type == typeof(bool)) func = (Func<T>)(() => (T)Convert.ChangeType(GUILayout.Toggle(Convert.ToBoolean(v), ""), type));
        if (type == typeof(Vector2))
        {
            func = (Func<T>)(() =>
            {
                var vVec2 = (Vector2)Convert.ChangeType(v, type);
                vVec2.x = Field(vVec2.x);
                vVec2.y = Field(vVec2.y);
                return (T)Convert.ChangeType(vVec2, type);
            });
        }
        else if (type.IsEnum) func = (Func<T>)(() =>
        {
            var enumValues = Enum.GetValues(type).OfType<T>().ToList();

            var isFlag = type.GetCustomAttributes(typeof(System.FlagsAttribute), true).Any();
            if (isFlag)
            {
                var flagV = Convert.ToUInt64(v);
                enumValues.ForEach(value =>
                {
                    var flag = Convert.ToUInt64(value);
                    if (flag > 0)
                    {
                        var has = (flag & flagV) == flag;
                        has = GUILayout.Toggle(has, value.ToString());

                        flagV = has ? (flagV | flag) : (flagV & ~flag);
                    }
                });

                v = (T)Enum.ToObject(type, flagV);
            }
            else
            {
                var valueNames = enumValues.Select(value => value.ToString()).ToArray();
                var idx = enumValues.IndexOf(v);
                idx = GUILayout.SelectionGrid(
                    idx,
                    valueNames,
                    valueNames.Length);
                v = enumValues.ElementAtOrDefault(idx);
            }
            return v;
        });

        T ret = default(T);

        using (var h = new GUILayout.HorizontalScope())
        {
            if (!string.IsNullOrEmpty(label)) GUILayout.Label(label);
            ret = func();
        }

        return ret;
    }

    public static int IntButton(int v, string label = "")
    {
        using (var h = new GUILayout.HorizontalScope())
        {
            v = Field(v, label);
            if (GUILayout.Button("+")) v++;
            if (GUILayout.Button("-")) v--;
        }
        return v;
    }

    public static int Slider(int v, int min, int max, string label = "")
    {
        return Mathf.FloorToInt(Slider((float)v, min, max, label));
    }

    public static float Slider(float v, string label = "") { return Slider(v, 0f, 1f, label); }
    public static float Slider(float v, float min, float max, string label = "")
    {
        float ret = default(float);
        using (var h = new GUILayout.HorizontalScope())
        {
            if (!string.IsNullOrEmpty(label)) GUILayout.Label(label);
            ret = GUILayout.HorizontalSlider(v, min, max, GUILayout.MinWidth(200));
            ret = Field(ret);
        }

        return ret;
    }


    static readonly string[] defaultElemLabels = new[] { "x", "y", "z" };
    public static Vector3 Slider(Vector3 v, Vector3 min, Vector3 max, string label = "", string[] elemLabels = null)
    {
        var ret = default(Vector3);
        var eLabels = elemLabels ?? defaultElemLabels;

        using (var h0 = new GUILayout.HorizontalScope())
        {
            if (!string.IsNullOrEmpty(label)) GUILayout.Label(label);
            using (var vertical = new GUILayout.VerticalScope())
            {
                for (var i = 0; i < 3; ++i)
                {
                    using (var h1 = new GUILayout.HorizontalScope())
                    {
                        GUILayout.Label(eLabels[i]);
                        ret[i] = GUILayout.HorizontalSlider(v[i], min[i], max[i], GUILayout.MinWidth(200));
                        ret[i] = Field(ret[i]);
                    }
                }
            }
        }

        return ret;
    }

    public static Vector2 Vector2(Vector2 v, string label = "")
    {
        if (!string.IsNullOrEmpty(label)) GUILayout.Label(label);
        return new Vector2(Field(v.x), Field(v.y));
    }


    public static void TexWindow(List<RenderTexture> texs, string label)
    {
        var offset = 20;
        var x = offset;
        var y = offset;
        var hMax = 0;

        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), label, "Window");

        texs.ForEach(tex =>
        {
            var w = tex.width;
            var h = tex.height;
            hMax = Mathf.Max(hMax, h);

            if (x + w >= Screen.width - offset)
            {
                x = 0;
                y += hMax + offset;
                hMax = h;
            }
            GUI.DrawTexture(new Rect(x, y, w, h), tex, ScaleMode.ScaleToFit, false);
            x += w + offset;
        });
    }

    public static void Indent(Action action) { Indent(1, action); }
    public static void Indent(int level, Action action)
    {
        const int TAB = 20;
        using (var h = new GUILayout.HorizontalScope())
        {
            GUILayout.Space(TAB * level);
            using (var v = new GUILayout.VerticalScope())
            {
                action();
            }
        }
    }
}