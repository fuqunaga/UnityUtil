using System.Collections.Generic;
using UnityEngine;

namespace UnityUtil
{
    public static class MaterialPropertyBlockExtension
    {
        public static void Set<T>(this MaterialPropertyBlock mpb, int propertyId, T value)
        {
            switch (value)
            {
                case float           v: mpb.SetFloat(propertyId, v); break;
                case int             v: mpb.SetInteger(propertyId, v); break;
                case Vector4         v: mpb.SetVector(propertyId, v); break;
                case Color           v: mpb.SetColor(propertyId, v); break;
                case Matrix4x4       v: mpb.SetMatrix(propertyId, v); break;
                case ComputeBuffer   v: mpb.SetBuffer(propertyId, v); break;
                case GraphicsBuffer  v: mpb.SetBuffer(propertyId, v); break;
                case Texture         v: mpb.SetTexture(propertyId, v); break;
                case List<float>     v: mpb.SetFloatArray(propertyId, v); break;
                case List<Vector4>   v: mpb.SetVectorArray(propertyId, v); break;
                case List<Matrix4x4> v: mpb.SetMatrixArray(propertyId, v); break;
            }
        }
    }
}