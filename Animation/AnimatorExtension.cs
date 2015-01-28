using UnityEngine;
using System.Collections;
using System.Linq;

static public class AnimatorExtension {
	
	static public bool IsStateFinished(this Animator a, string layerAndStateName, bool playIfNot = false)
	{
		var info = a.GetCurrentAnimatorStateInfo(0);
		bool isState = info.IsName(layerAndStateName);
		if ( !isState && playIfNot ){ 
			a.Play(layerAndStateName);
		}

		return( isState && (info.normalizedTime >= 1f));
	}

    static public bool IsState(this Animator a, string layerAndStateName)
    {
        var info = a.GetCurrentAnimatorStateInfo(0);
        return info.IsName(layerAndStateName);
    }
	
}