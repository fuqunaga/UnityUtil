using UnityEngine;

namespace UnityUtil
{

    [RequireComponent(typeof(Animator))]
    public class RandomStartAnimator : MonoBehaviour
    {
        void Start()
        {
            Set(GetComponent<Animator>());
        }

        public static void Set(Animator animator)
        {
            Set(animator, Random.value);
        }

        public static void Set(Animator animator, float normalizedTime)
        {
            var info = animator.GetCurrentAnimatorStateInfo(0);
            animator.Play(info.fullPathHash, 0, normalizedTime);
        }
    }
}