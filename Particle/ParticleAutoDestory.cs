using UnityEngine;
using System.Collections;

public class ParticleAutoDestory : MonoBehaviour {

	void Update () {
		if ( !particleSystem.IsAlive() ) Destroy(gameObject);
	}
}
