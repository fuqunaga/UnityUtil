using UnityEngine;
using System.Collections;

namespace UnityUtil
{
    public class NoChildAutoDestroy : MonoBehaviour
    {

        // 一度子供ができるまで待つ
        public bool waitChildGenerate;

        bool isChildGenerated;

        void Update()
        {
            var hasChild = transform.childCount > 0;
            isChildGenerated |= hasChild;

            if (!hasChild && (!waitChildGenerate || isChildGenerated))
            {
                Destroy(gameObject);
            }
        }
    }
}