﻿using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Animator))]
public class AnimatorStarter : MonoBehaviour {

    public string trigger;

	void Start () {
        if ( !string.IsNullOrEmpty(trigger))
        {
            GetComponent<Animator>().SetTrigger(trigger);
        }
	
	}
	
}
