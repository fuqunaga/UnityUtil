using UnityEngine;

namespace UnityUtil
{
    public class NoChildAutoDestroy : MonoBehaviour
    {
        // 一度子供ができるまで待つ
        public bool waitChildGenerate;

        bool _isChildGenerated;

        void Update()
        {
            var hasChild = transform.childCount > 0;
            _isChildGenerated |= hasChild;

            if (!hasChild && (!waitChildGenerate || _isChildGenerated))
            {
                Destroy(gameObject);
            }
        }
    }
}