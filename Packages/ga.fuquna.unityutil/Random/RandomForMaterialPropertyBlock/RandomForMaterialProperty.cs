using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityUtil
{

    public class RandomForMaterialProperty<T> : MaterialPropertySetter<T>
    {
        [FormerlySerializedAs("_values")] public List<T> values;
        protected override T GetValue()
        {
            return values.SelectRandom();
        }
    }

    public abstract class MaterialPropertySetter<T> : MonoBehaviour
    {
        [FormerlySerializedAs("_name")] public string propertyName;
        protected abstract T GetValue();

        void Start()
        {
            var v = GetValue();

            var mpb = new MaterialPropertyBlock();
            foreach(var r in transform.GetComponentsAll<Renderer>())
            {
                r.GetPropertyBlock(mpb);
                mpb.Set(Shader.PropertyToID(propertyName), v);
                r.SetPropertyBlock(mpb);
            }
        }
    }
}