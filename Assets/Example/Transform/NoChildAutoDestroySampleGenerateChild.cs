using System.Collections;
using UnityEngine;

namespace UnityUtil.Example
{
    public class NoChildAutoDestroySampleGenerateChild : MonoBehaviour
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