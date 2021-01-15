using UnityEngine;

namespace UnityUtil
{

    public class AutoRot : MonoBehaviour
    {
        public float speed = 30f;

        void Update()
        {

            var deg = transform.rotation.eulerAngles;
            deg.y += speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(deg);

        }
    }

}