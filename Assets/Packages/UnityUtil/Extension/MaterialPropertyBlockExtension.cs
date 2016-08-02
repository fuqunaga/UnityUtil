using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

static public class MaterialPropertyBlockExtension
{
    static Dictionary<System.Type, MethodInfo> _setters = new Dictionary<System.Type, MethodInfo>()
    {
		{ typeof(float    ), typeof(MaterialPropertyBlock).GetMethod("SetFloat"  , new [] {typeof(string), typeof(float)     })}, 
		{ typeof(Matrix4x4), typeof(MaterialPropertyBlock).GetMethod("SetMatrix" , new [] {typeof(string), typeof(Matrix4x4) })},  
		{ typeof(Texture  ), typeof(MaterialPropertyBlock).GetMethod("SetTexture", new [] {typeof(string), typeof(Texture)   })},  
		{ typeof(Vector4  ), typeof(MaterialPropertyBlock).GetMethod("SetVector" , new [] {typeof(string), typeof(Vector4)   })},  
		{ typeof(Color    ), typeof(MaterialPropertyBlock).GetMethod("SetColor"  , new [] {typeof(string), typeof(Color)     })},  
	};

	static public void Set<T>(this MaterialPropertyBlock mpb, string name, T value)
	{
        _setters[typeof(T)].Invoke(mpb, new object[] { name, value });
	}
}
