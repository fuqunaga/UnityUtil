using UnityEngine;
using Primitive2D;

public class Primitive2DSample : MonoBehaviour {

	void Update () {

        var circle = new Circle(transform.lossyScale, 100, transform.position);
        Primitive2DDebug.Draw(circle, Color.red);
	
	}
}
