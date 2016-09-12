using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class RandomAnimatorController : MonoBehaviour {

    public RuntimeAnimatorController[] controllers;

	void Start () {

        GetComponent<Animator>().runtimeAnimatorController = controllers[Random.Range(0, controllers.Length)];
	
	}

}
