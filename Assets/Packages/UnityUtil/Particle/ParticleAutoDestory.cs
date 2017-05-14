using UnityEngine;

namespace UnityUtil
{

    public class ParticleAutoDestory : MonoBehaviour
    {

        void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive()) Destroy(gameObject);
        }
    }
}