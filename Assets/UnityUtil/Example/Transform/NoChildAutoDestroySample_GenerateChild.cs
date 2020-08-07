using UnityEngine;
using System.Collections;

namespace UnityUtil
{
    public class NoChildAutoDestroySample_GenerateChild : MonoBehaviour
    {
        public float delay = 1f;

        void Start()
        {
            StartCoroutine(DelayGenerateChild());
        }


        IEnumerator DelayGenerateChild()
        {
            yield return new WaitForSeconds(delay);
            var go = new GameObject();
            go.transform.SetParent(transform);
        }
    }

}