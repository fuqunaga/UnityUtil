using System.Collections.Generic;
using UnityEngine;

namespace UnityUtil
{

    public class RandomForMaterialProperty<T> : MaterialPropertySetter<T>
    {
        public List<T> _values;
        protected override T GetValue()
        {
            return _values.SelectRandom();
        }
    }

    public abstract class MaterialPropertySetter<T> : MonoBehaviour
    {
        public string _name;
        protected abstract T GetValue();

        void Start()
        {
            var v = GetValue();

            var mpb = new MaterialPropertyBlock();
            foreach(var r in transform.GetComponentsAll<Renderer>())
            {
                r.GetPropertyBlock(mpb);
                mpb.Set(_name, v);
                r.SetPropertyBlock(mpb);
            }
        }
    }
}