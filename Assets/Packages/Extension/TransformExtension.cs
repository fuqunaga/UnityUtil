using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

static public class TransformExtension
{
	static public List<T> GetComponentsAll<T>(this Transform trans) where T : Component
	{
		var ret = Enumerable.Range(0, trans.childCount)
			.SelectMany(i => trans.GetChild(i).GetComponentsAll<T>())
			.ToList();


		ret.AddRange(trans.GetComponents<T>());
		return ret;
	}

    static public List<Transform> GetChildren(this Transform trans)
    {
        var num = trans.childCount;
        var ret = new List<Transform>(num);
        for(var i=0; i<num; ++i) ret.Add(trans.GetChild(i));
        return ret;
    }
}
