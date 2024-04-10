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

                return _instance;
            }
            return _instance;
        }
        
        public static void ResetStaticFields()
        {
            _instance = null;
            _first = true;
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                ResetStaticFields();
            }
        }
    }
}