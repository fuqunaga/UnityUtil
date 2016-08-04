using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace PrefsWrapper
{
    [Serializable]
    public class PrefsString : PrefsParam<string>
    {
        public PrefsString(string key, string defaultValue = default(string)) : base(key, defaultValue) { }
    }


    [Serializable]
    public class PrefsInt : PrefsParam<int>
    {
        public PrefsInt(string key, int defaultValue = default(int)) : base(key, defaultValue) { }
    }

    [Serializable]
    public class PrefsFloat : PrefsParam<float>
    {
        public PrefsFloat(string key, float defaultValue = default(float)) : base(key, defaultValue) { }

        public bool OnGUISlider(string label = null) { return OnGUISlider(0f, 1f, label); }
        public bool OnGUISlider(float min, float max, string label = null)
        {
            return OnGUIwithButton(() =>
            {
                var v = Get();
                var prev_v = v;

                GUILayout.Label(label ?? key);

                v = GUIUtil.Slider(v, min, max);
                GUILayout.FlexibleSpace();

                var changed = !prev_v.Equals(v);

                if ( changed )
                    Set(v);

                return changed;
            });
        }
    }


    [Serializable]
    public class PrefsBool : PrefsParam<bool>
    {
        public PrefsBool(string key, bool defaultValue = default(bool)) : base(key, defaultValue) { }
    }

    [Serializable]
    public class PrefsVector2 : PrefsVector<Vector2>
    {
        public PrefsVector2(string key, Vector2 defaultValue = default(Vector2)) : base(key, defaultValue) { }
    }

    [Serializable]
    public class PrefsVector3 : PrefsVector<Vector3>
    {
        public PrefsVector3(string key, Vector3 defaultValue = default(Vector3)) : base(key, defaultValue) { }
    }

    [Serializable]
    public class PrefsVector4 : PrefsVector<Vector4>
    {
        public PrefsVector4(string key, Vector4 defaultValue = default(Vector4)) : base(key, defaultValue) { }
    }


    public class PrefsVector<T> : _PrefsParam<T>
    {
        bool foldOpen;

        public PrefsVector(string key, T defaultValue = default(T)) : base(key, defaultValue) { }

        public override T Get()
        {
            return PlayerPrefsVector<T>.Get(key, defaultValue);
        }

        public override void Set(T v)
        {
            PlayerPrefsVector<T>.Set(key, v);
        }

        public override void Delete() { PlayerPrefsVector<T>.DeleteKey(key); }

        protected override bool _OnGUI(string key, string label, T defaultValue)
        {
            return PlayerPrefsVector<T>.OnGUI(key, label, defaultValue);
        }

        public bool OnGUISlider(string label = null)
        {
            return OnGUISlider(
                (T)typeof(T).InvokeMember("zero", BindingFlags.Static | BindingFlags.GetProperty, null, null, null),
                (T)typeof(T).InvokeMember("one", BindingFlags.Static | BindingFlags.GetProperty, null, null, null),
                label);
        }
        public bool OnGUISlider(T min, T max, string label = null, string[] elementLabels = null)
        {
            return OnGUIwithButton(() => PlayerPrefsVector<T>.OnGUISlider(key, label, ref foldOpen, defaultValue, min, max, elementLabels));
        }
    }

    [Serializable]
    public class PrefsColor : _PrefsParam<Color>
    {
        bool foldOpen;

        static Vector4 ToVector4(Color c)
        {
            Vector4 v4 = default(Vector4);
            Color.RGBToHSV(c, out v4.x, out v4.y, out v4.z);
            v4.w = c.a;
            return v4;
        }

        static Color ToColor(Vector4 v4)
        {
            var c = Color.HSVToRGB(v4.x, v4.y, v4.z);
            c.a = v4.w;
            return c;
        }

   
        public PrefsColor(string key, Color defaultValue = default(Color)) : base(key, defaultValue) { }

        public override void Delete()
        {
            PlayerPrefsVector<Vector4>.DeleteKey(key);
        }


        bool _first = true;
        public override Color Get()
        {
            if ( _first)
            {
                _first = false;
                Set(defaultValue);
            }

            return ToColor(PlayerPrefsVector<Vector4>.Get(key, defaultValue));
        }

        public override void Set(Color c)
        {
            PlayerPrefsVector<Vector4>.Set(key, ToVector4(c));
        }

        protected override bool _OnGUI(string key, string label = null, Color defaultValue = default(Color))
        {
            throw new NotImplementedException();
        }

        static readonly string[] _elementLabels = new[] { "H", "S", "V", "A" };

        public bool OnGUISlider(string label = null)
        {

            return OnGUIwithButton(() => {
                var changed = PlayerPrefsVector<Vector4>.OnGUISlider(key, label, ref foldOpen, defaultValue, Vector4.zero, Vector4.one, _elementLabels);

                var col = GUI.color;
                GUI.color = Get();
                GUILayout.Label("■■■");
                GUI.color = col;
                return changed;
            });
        }
    }


    public class PrefsParam<T> : _PrefsParam<T>
    {
        public PrefsParam(string key, T defaultValue = default(T)) : base(key, defaultValue) { }

        public override T Get() { return PlayerPrefs<T>.Get(key, defaultValue); }
        public override void Set(T v) { PlayerPrefs<T>.Set(key, v); }

        public override void Delete() { PlayerPrefs.DeleteKey(key); }

        protected override bool _OnGUI(string key, string label = null, T defaultValue = default(T))
        {
            return PlayerPrefs<T>.OnGUI(key, label, defaultValue);
        }
    }


    public abstract class _PrefsParam<T> : _PrefsParam
    {

        [SerializeField]
        protected T defaultValue;

        public _PrefsParam(string key, T defaultValue = default(T))
            : base(key)
        {
            this.defaultValue = defaultValue;
        }


        public static implicit operator T(_PrefsParam<T> me)
        {
            return me.Get();
        }

        public abstract T Get();
        public abstract void Set(T v);

        public void SetWithDefault(T v) { Set(v); defaultValue = v; }

        public abstract void Delete();

        protected abstract bool _OnGUI(string key, string label = null, T defaultValue = default(T));

        public bool OnGUI(string label = null)
        {
            return OnGUIwithButton(() => _OnGUI(key, label, defaultValue));
        }

        protected bool OnGUIwithButton(Func<bool> onGUIFunc)
        {
            var changed = false;
            using (var h = new GUILayout.HorizontalScope())
            {
                changed = onGUIFunc();

                if (GUILayout.Button("default", GUILayout.Width(60f)))
                {
                    Set(defaultValue);
                    changed = true;
                }
            }

            return changed;
        }
    }

    public abstract class _PrefsParam
    {
        public string key;

        public _PrefsParam(string key) { this.key = key; }
    }

    public static class PlayerPrefsColor
    {
    }




    public static class PlayerPrefsVector<T>
    {
        static readonly Dictionary<Type, int> _typeToRank = new Dictionary<Type, int>()
        {
            {typeof(Vector2), 2},
            {typeof(Vector3), 3},
            {typeof(Vector4), 4},
        };

        static readonly string[] _defaultElementLabels = new[] { "x", "y", "z", "w" };

        static int Rank { get { return _typeToRank[typeof(T)]; } }

        static float Get(T t, int i) { return (float)typeof(T).InvokeMember("Item", BindingFlags.GetProperty, null, t, new object[] { i }); }
        static void Set(ref T t, int i, float v)
        {
            var o = (object)t;
            typeof(T).InvokeMember("Item", BindingFlags.SetProperty, null, o, new object[] { i, v });
            t = (T)o;
        }

        static string KeyWithIdx(string key, int idx) { return key + "_" + idx + "_PlayerPrefsVector"; }

        static void ForEachRank(Action<int> action)
        {
            for (var i = 0; i < Rank; ++i) action(i);
        }

        public static bool OnGUISlider(string key, string label, ref bool foldOpen, T defaultValue, T min, T max, string[] elementLabels = null)
        {
            elementLabels = elementLabels ?? _defaultElementLabels;

            var v = Get(key, defaultValue);
            var prev_v = v;

            using (var h = new GUILayout.HorizontalScope())
            {
                var foldStr = foldOpen ? "▼" : "▶";

                //GUILayout.Label(label ?? key);
                foldOpen ^= GUILayout.Button(foldStr + (label ?? key), GUIUtil.Style.FoldoutPanelStyle);



                if (foldOpen)
                {
                    using (var hs = new GUILayout.HorizontalScope())
                    {
                        using (var vs = new GUILayout.VerticalScope())
                        {
                            //ForEachRank((i) => Set(ref v, i, GUIUtil.Slider(Get(v, i), Get(min, i), Get(max, i), elementLabels[i])));
                            ForEachRank((i) => GUILayout.Label(elementLabels[i]));
                        }
                        using (var vs = new GUILayout.VerticalScope())
                        {
                            ForEachRank((i) => Set(ref v, i, GUIUtil.Slider(Get(v, i), Get(min, i), Get(max, i))));
                        }
                    }
                }
                else
                {
                    ForEachRank((i) => PlayerPrefs<float>.OnGUI(KeyWithIdx(key, i), "", Get(defaultValue, i)));
                }
                //GUILayout.FlexibleSpace();

            }


            var changed = !prev_v.Equals(v);
            if ( changed)
                Set(key, v);

            return changed;
        }

        public static bool OnGUI(string key, string label = null, T defaultValue = default(T))
        {
            var changed = false;
            using (var h = new GUILayout.HorizontalScope())
            {
                GUILayout.Label(label ?? key);
                ForEachRank((i) =>
                {
                    var randChanged = PlayerPrefs<float>.OnGUI(KeyWithIdx(key, i), "", Get(defaultValue, i));
                    changed = changed || randChanged;
                });

                return changed;
            }
        }


        public static T Get(string key, T defaultValue = default(T))
        {
            var ret = default(T);
            ForEachRank((i) =>
                Set(ref ret, i, PlayerPrefs<float>.Get(KeyWithIdx(key, i), Get(defaultValue, i)))
            );

            return ret;
        }

        public static void Set(string key, T val)
        {
            ForEachRank((i) =>
                PlayerPrefs<float>.Set(KeyWithIdx(key, i), Get(val, i))
            );
        }

        public static void DeleteKey(string key)
        {
            ForEachRank((i) =>
                PlayerPrefs.DeleteKey(KeyWithIdx(key, i))

            );
        }
    }

    public static class PlayerPrefs<T>
    {

        public static bool OnGUI(string key, string label = null, T defaultValue = default(T))
        {
            var changed = false;
            if (!PlayerPrefs.HasKey(key))
            {
                Set(key, defaultValue);
                changed = true;
            }

            var prev = Get(key);
            var next = GUIUtil.Field<T>(Get(key), label ?? key);
            if ( !prev.Equals(next))
            {
                Set(key, next);
                changed = true;
            }

            return changed;
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


        static readonly Dictionary<Type, string> GetMethodTable = new Dictionary<Type, string>
    {
        {typeof(int), "GetInt"}
        ,{typeof(float), "GetFloat"}
        ,{typeof(string), "GetString"}
    };

        static readonly Dictionary<Type, string> SetMethodTable = new Dictionary<Type, string>
    {
        {typeof(int), "SetInt"}
        ,{typeof(float), "SetFloat"}
        ,{typeof(string), "SetString"}
    };

        public static T Get(string key, T defaultValue = default(T))
        {
            if (!PlayerPrefs.HasKey(key)) Set(key, defaultValue);

            System.Reflection.MethodInfo info = typeof(PlayerPrefs).GetMethod(GetMethodTable[type], new[] { typeof(string) });
            return typeof(T).IsEnum
                ? (T)info.Invoke(null, new[] { key })
                : (T)Convert.ChangeType(info.Invoke(null, new[] { key }), typeof(T));
        }

        public static void Set(string key, T val)
        {
            System.Reflection.MethodInfo info = typeof(PlayerPrefs).GetMethod(SetMethodTable[type], new[] { typeof(string), type });
            info.Invoke(null, new object[] { key, Convert.ChangeType(val, type) });
        }
    }
}