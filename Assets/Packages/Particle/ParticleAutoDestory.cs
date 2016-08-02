using UnityEngine;
using System.Collections;

public class ParticleAutoDestory : MonoBehaviour {

	void Update () {
		if ( !GetComponent<ParticleSystem>().IsAlive() ) Destroy(gameObject);
	}
}
