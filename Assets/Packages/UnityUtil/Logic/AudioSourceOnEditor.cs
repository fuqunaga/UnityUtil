using UnityEngine;

namespace UnityUtil
{

    public class AudioSourceOnEditor : MonoBehaviour
    {
#if UNITY_EDITOR
        public AudioClip clip;
        public bool loop;

        private void Start()
        {
            if ( clip != null)
            {
                var src = gameObject.AddComponent<AudioSource>();
                src.clip = clip;
                src.Play();
                src.loop = loop;
            }
        }
#endif
    }

}
