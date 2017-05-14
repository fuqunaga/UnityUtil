using UnityEngine;
using System.Collections;

namespace UnityUtil
{

    public class AutoRot : MonoBehaviour
    {

        public float speed;

        void Update()
        {

            var deg = transform.rotation.eulerAngles;
            deg.y += speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(deg);

        }
    }

}