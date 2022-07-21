using UnityEngine;
using System.Collections;
using System.Linq;

public static class AnimatorExtension {
	
	public static bool IsStateFinished(this Animator a, string layerAndStateName, bool playIfNot = false)
	{
		var info = a.GetCurrentAnimatorStateInfo(0);
		bool isState = info.IsName(layerAndStateName);
		if ( !isState && playIfNot ){ 
			a.Play(layerAndStateName);
		}

		return( isState && (info.normalizedTime >= 1f));
	}

    public static bool IsState(this Animator a, string layerAndStateName)
    {
        var info = a.GetCurrentAnimatorStateInfo(0);
        return info.IsName(layerAndStateName);
    }
	
}