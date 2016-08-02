using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class RandomStartAnimator : MonoBehaviour {

	void Start () {
		Set(GetComponent<Animator>());
	}

    public static void Set(Animator animator)
    {
        var info = animator.GetCurrentAnimatorStateInfo(0);
        animator.Play(info.fullPathHash, 0, Random.value);
    }
}
