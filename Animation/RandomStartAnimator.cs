using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class RandomStartAnimator : MonoBehaviour {

	void Start () {
		var animator = GetComponent<Animator>();
		var info = animator.GetCurrentAnimatorStateInfo(0);
		animator.Play(info.nameHash, 0, Random.value);
	}

}
