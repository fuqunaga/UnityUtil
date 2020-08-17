using System.Collections.Generic;
using UnityEngine;

namespace UnityUtil
{

    public abstract class RandomForMaterialProperty<T> : MaterialPropertySetter<T>
    {
        public List<T> _values;
        protected override T CalcValue()
        {
            return _values.SelectRandom();
        }
    }

    public abstract class MaterialPropertySetter<T> : MonoBehaviour
    {
        public string _name;
        protected abstract T CalcValue();

        void Start()
        {
            var v = CalcValue();

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