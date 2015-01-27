using UnityEngine;
using System.Collections;

public class NoChildAutoDestroy : MonoBehaviour {

	void Update () {
		if ( transform.childCount <= 0 ) Destroy(gameObject);
	}
}
