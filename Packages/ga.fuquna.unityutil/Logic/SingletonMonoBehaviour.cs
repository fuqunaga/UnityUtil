using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace UnityUtil
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        
        // ReSharper disable once StaticMemberInGenericType
        private static bool _first = true;
        
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));
                    SingletonMonoBehaviour.Register<T>();

                    if (_instance == null)
                    {
                        Debug.LogError($"{typeof(T)} is nothing");
                    }
                }

                return _instance;
            }
        }
        
        public static T GetInstance()
        {
            if (_first && _instance == null)
            {
                _first = false;
                _instance = (T)FindObjectOfType(typeof(T));
                SingletonMonoBehaviour.Register<T>();
                return _instance;
            }
            return _instance;
        }
        
        public static void ResetStaticFields()
        {
            _instance = null;
            _first = true;
        }
    }

    /// <summary>
    /// Enter Play Mode optionsでstaticな値が初期化されない対策
    /// </summary>
    public static class SingletonMonoBehaviour
    {
        private static readonly HashSet<Type> RegisteredTypes = new();

#if UNITY_EDITOR
        static SingletonMonoBehaviour()
        {
            UnityEditor.EditorApplication.playModeStateChanged += state =>
            {
                if (state == UnityEditor.PlayModeStateChange.ExitingPlayMode)
                {
                    Reset();
                }
            };
        }
#endif
        
        public static void Register<T>() where T : MonoBehaviour
        {
            RegisteredTypes.Add(typeof(T));
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
        private static void Reset()
        {
            if ( RegisteredTypes.Count == 0 ) return;

            var singletonType = typeof(SingletonMonoBehaviour<>);
            
            
            foreach (var type in RegisteredTypes)
            {
                var genericType = singletonType.MakeGenericType(type);
                var methodInfo = genericType.GetMethod("ResetStaticFields");
                methodInfo?.Invoke(null, null);
            }
            
            RegisteredTypes.Clear();
        }
    }
}