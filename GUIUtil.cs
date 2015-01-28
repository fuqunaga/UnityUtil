using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

public static class GUIUtil
{
    public static void Vertical(Action action, params GUILayoutOption[] options) { Vertical(action, GUIStyle.none, options);}
    public static void Vertical(Action action, GUIStyle style, params GUILayoutOption[] options)
    {
        GUILayout.BeginVertical(style, options);
        action();
        GUILayout.EndVertical();
    }

    public static void Horizontal(Action action, params GUILayoutOption[] options) { Horizontal(action, GUIStyle.none, options);}
    public static void Horizontal(Action action, GUIStyle style, params GUILayoutOption[] options)
    {
        GUILayout.BeginHorizontal(style, options);
        action();
        GUILayout.EndHorizontal();
    }


    public class Folds : Dictionary<string, Fold>
    {
        public void Add(string name, Action action, bool enableFirst = false)
        {
            Add(name, new Fold(name, action, enableFirst));
        }

        public void OnGUI()
        {
            Values.ToList().ForEach(f => f.OnGUI());
        }
    }

    public class Fold
    {
        bool enable;
        string name;
        Action draw;

        public Fold(string n, Action action, bool enableFirst = false)
        {
            name = n;
            draw += action;
            enable = enableFirst;
        }

        public void Add(Action action)
        {
            draw += action;
        }

        public void OnGUI()
        {
            enable = GUILayout.Toggle(enable, name);
            if (enable)
            {
                GUIUtil.Vertical(() => draw(), "window");
            }
        }
    }

    public static T Field<T>(T v, string label = "")// where T : IConvertible
    {
        var type = typeof(T);
        Func<T> func = (Func<T>)(() => (T)Convert.ChangeType(GUILayout.TextField(v.ToString()), type));

        if (type == typeof(bool))    func = (Func<T>)(() => (T)Convert.ChangeType(GUILayout.Toggle(Convert.ToBoolean(v), ""), type));
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
            var values = Enum.GetValues(type).OfType<T>().Select(value => value.ToString()).ToArray();
            return (T)((object)GUILayout.SelectionGrid(
                (int)((object)v),
                values,
                values.Length));
        });

        T ret = default(T);

        GUIUtil.Horizontal(() =>
        {
            if (!string.IsNullOrEmpty(label)) GUILayout.Label(label);
            ret = func();
        });

        return ret;
    }

    public static int Slider(int v, int min, int max, string label = "")
    {
        return Mathf.FloorToInt(Slider((float)v,  min,  max, label));
    }

    public static float Slider(float v, string label = ""){ return Slider(v, 0f, 1f, label); }
    public static float Slider(float v, float min, float max, string label = "")
    {
        float ret = default(float);

        GUIUtil.Horizontal(() =>
        {
            if (!string.IsNullOrEmpty(label)) GUILayout.Label(label);
			ret = GUILayout.HorizontalSlider(v, min, max, GUILayout.MinWidth(200));
            ret = Field(ret);
        });

        return ret;
    }
}


public static class PlayerPrefsVector2
{
    public static void OnGUI(string key, string label = null, Vector2 defaultValue = default(Vector2))
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(label ?? key);
        PlayerPrefs<float>.OnGUI(key + "x", "", defaultValue.x);
        PlayerPrefs<float>.OnGUI(key + "y", "", defaultValue.y);
        GUILayout.EndHorizontal();
    }

    public static Vector2 Get(string key, Vector2 defaultValue = default(Vector2))
    {
        var ret = default(Vector2);
        ret.x = PlayerPrefs<float>.Get(key + "x", defaultValue.x);
        ret.y = PlayerPrefs<float>.Get(key + "y", defaultValue.y);

        return ret;
    }

    public static void Set(string key, Vector2 val)
    {
        PlayerPrefs<float>.Set(key + "x", val.x);
        PlayerPrefs<float>.Set(key + "y", val.y);
    }
}

public static class PlayerPrefs<T> where T : IConvertible//, IEquatable<T>
{

    public static void OnGUI(string key, string label = null, T defaultValue = default(T))
    {
        if (!PlayerPrefs.HasKey(key)) Set(key, defaultValue);
#if false
		var oldValue = Get(key);
		var newValue = ;
		if ( !newValue.Equals(oldValue) ) Set(key, newValue);
#else
        Set(key, GUIUtil.Field<T>(Get(key), label ?? key));
#endif
    }

    static System.Type type
    {
        get
        {
            return (typeof(T) == typeof(bool) || typeof(T).IsEnum)
                ? typeof(int)
                : typeof(T);
        }
    }

    public static T Get(string key, T defaultValue = default(T))
    {
        if (!PlayerPrefs.HasKey(key)) Set(key, defaultValue);
        var table = new Dictionary<Type, string>{
			{typeof(int), "GetInt"}
			,{typeof(float), "GetFloat"}
			,{typeof(string), "GetString"}
		};
        System.Reflection.MethodInfo info = typeof(PlayerPrefs).GetMethod(table[type], new[] { typeof(string) });
        return typeof(T).IsEnum
            ? (T)info.Invoke(null, new[] { key })
            : (T)Convert.ChangeType(info.Invoke(null, new[] { key }), typeof(T));
    }

    public static void Set(string key, T val)
    {
        var table = new Dictionary<Type, string>{
			{typeof(int), "SetInt"}
			,{typeof(float), "SetFloat"}
			,{typeof(string), "SetString"}
		};
        System.Reflection.MethodInfo info = typeof(PlayerPrefs).GetMethod(table[type], new[] { typeof(string), type });
        info.Invoke(null, new object[] { key, Convert.ChangeType(val, type) });
    }
}

public class PrefsParam<T> where T : System.IConvertible //, System.IEquatable<T>
{
    public string key;
    protected T defaultValue;


    public PrefsParam(string key, T defaultValue = default(T))
    {
        this.key = key;
        this.defaultValue = defaultValue;
    }

    public T Get() { return PlayerPrefs<T>.Get(key, defaultValue); }
    public void Set(T v) { PlayerPrefs<T>.Set(key, v); }

    public static implicit operator T(PrefsParam<T> me)
    {
        return me.Get();
    }

    public void OnGUI(string label = null)
    {
        PlayerPrefs<T>.OnGUI(key, label, defaultValue);
    }
}
