using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnityUtil
{

    public class RandomColor : RandomForMaterialProperty<Color>
    {
    }


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
            transform.GetComponentsAll<Renderer>().ToList().ForEach(r =>
            {
                r.GetPropertyBlock(mpb);
                mpb.Set(_name, v);
                r.SetPropertyBlock(mpb);
            });
        }
    }
}