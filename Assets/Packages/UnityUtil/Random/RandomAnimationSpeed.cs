using UnityEngine;
using System.Collections;
using System.Linq;

public class RandomAnimationSpeed : MonoBehaviour {

    public RandFloat _speed = new RandFloat(1f, 1f);

	void Start () {

        transform.GetComponentsAll<Animator>().ToList().ForEach(a => a.speed = _speed.Calc());

    }
	
}
