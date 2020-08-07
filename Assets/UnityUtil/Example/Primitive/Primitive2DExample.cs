using UnityEngine;
using UnityUtil.Primitive2D;

namespace UnityUtil.Example
{
    public class Primitive2DExample : MonoBehaviour
    {

        void Update()
        {

            var circle = new Circle(transform.lossyScale, 100, transform.position);
            Primitive2DDebug.Draw(circle, Color.red);

        }
    }
}