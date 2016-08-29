using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

static class Util {

	static public Vector2 CrossKey(float scale=1f, bool extraEnable=true){
		KeyCode[][] keys = new[]{
			new[]{KeyCode.RightArrow, KeyCode.LeftArrow},
			new[]{KeyCode.UpArrow, KeyCode.DownArrow},
		};
		
		var vec = new Vector2();
		for(var i=0; i<keys.Length; ++i)
		{
			var keyset = keys[i];
			vec[i] = Input.GetKeyUp(keyset[0]) 
				? 1f 
					: Input.GetKeyUp(keyset[1]) ? -1f : 0f;
	    }

		var extraScale = (Input.GetKey(KeyCode.LeftAlt)
							? 0.1f
		                  : (Input.GetKey(KeyCode.LeftShift) ? 10f : 1f)
		                  );

	   return vec * scale * (extraEnable ? extraScale : 1f);
	}

	static public Vector3 WorldToGuiPoint(Vector3 pos)
	{
		var camera = Camera.main;
		var screenPos = camera.WorldToScreenPoint(pos);
		return new Vector3(screenPos.x, camera.pixelHeight-screenPos.y, screenPos.z);
	}

	static public T SelectRandomWithWeight<T>(List<T> list, System.Func<T, float> getWeight)
	{
		var total = list.Aggregate(0f, (s, data) => s + getWeight(data));
		var corsor = Random.value * total;
		var ret = list.ToList().Find(data => {
			corsor -= getWeight(data);
			return corsor<=0f;
		});

		return ret;
	}

	static public T RandomSelect<T>(T[] list)
	{
		return list[Random.Range(0, list.Length)];
	}

	static public float Rate2Value(float rate, float min, float max)
	{
		return rate * (max-min) + min;
	}

	static public float Value2Rate(float v, float min, float max)
	{
		return (v-min) / (max-min);
	}

	static public float Value2RateClamp(float v, float min, float max)
	{
		return Mathf.Clamp01(Value2Rate(v, min, max));
	}


	public static System.Type GetType( string TypeName )
	{
		
		// Try Type.GetType() first. This will work with types defined
		// by the Mono runtime, etc.
		var type = System.Type.GetType( TypeName );
		
		// If it worked, then we're done here
		if( type != null )
			return type;
		
		// Get the name of the assembly (Assumption is that we are using
		// fully-qualified type names)
		var assemblyName = TypeName.Substring( 0, TypeName.IndexOf( '.' ) );
		
		// Attempt to load the indicated Assembly
		var assembly = System.Reflection.Assembly.Load( assemblyName );
		if( assembly == null )
			return null;
		
		// Ask that assembly to return the proper Type
		return assembly.GetType( TypeName );
		
	}


	public static int NamesToLayerMask(params string[] names)
	{
		return names.Aggregate(0, (s,name)=> s|=1<<LayerMask.NameToLayer(name));
	}

    public static T SelectWithWait<T>(List<T> list, System.Func<T, float> getWait) where T : class
    {
        var total = list.Aggregate(0f, (s, data) => s + getWait(data));
        var corsor = Random.value * total;
        var ret = list.Find(data =>
        {
            corsor -= getWait(data);
            return corsor <= 0f;
        });

        return ret;
    }


    [System.Serializable]
    public class Stopwatch
    {
        public static Stopwatch StartNew()
        {
            var s = new Stopwatch();
            s.Start();
            return s;
        }

        public float? last;
        protected float _elapsed;

        protected float time { get{ return Time.time;}}

        public void Start() { last = time; }
        public void Stop() { _elapsed += Elapsed; last = 0; }
        public bool IsRunning { get{ return last.HasValue; } }
        public float Elapsed { get { return IsRunning ? time - last.Value : _elapsed; } }
    }
}



[System.Serializable]
public class RandParam {
	
	public float offset =  0f;
	public float randRange = 0f;

    public float max { get { return offset + randRange; } }

	public float value{ get{ return _value ?? Rand();} }
	float? _value;
	
	public float Rand()
	{
		_value = offset + Random.Range(-1f,1f) * randRange;
		return _value.Value;
	}

    public RandParam() { }
    public RandParam(float offset) { this.offset = offset; }
}

[System.Serializable]
public class KeepTimeFlag {
	
	public float switchTime =  5f;
	bool flag = false;
	float? startTime;

	public KeepTimeFlag(float switchTime = 5f){ this.switchTime = switchTime; }

	public void Set(bool f)
	{
		if ( f ) {
			if ( !startTime.HasValue ) startTime = Time.time;
			if ( (Time.time - startTime) >= switchTime )
			{
				flag = f;
			}
		}
		else {
			flag = f;
			startTime = null;
		}
	}

	public bool Get()
	{
		return flag;
	}

	public void Clear(){ Set(false); }

	public bool Check(float offset=0f){ 
        Set(true);
        return offset == 0f ? Get() : (Time.time - startTime) >= (switchTime + offset);
    }
	public void Reset(){ Set(false); Set(true); }
    	public void Reset(float switchTime) { this.switchTime = switchTime; Reset(); }	

	public static implicit operator bool(KeepTimeFlag x){ return x.Get(); }
	//public static operator bool(KeepTimeFlag x){return x; }
	public bool raw{ get{ return startTime.HasValue;} }

    // あとremain秒でTrueになるように強制
    public void SetRemain(float remain)
    {
        // Time.time+remain - startTime > switchTime
        // より Time.time+remain - switchTime > startTime
        startTime = Time.time + remain - switchTime; ;
    }
}

public class LazyHolder<T>
{
	T _value;
	System.Func<T> getter;
	public T value{ get{ return _value == null ? _value=getter() : _value; } }

	public LazyHolder(System.Func<T> getter)
	{
		this.getter = getter;
	}
		
	public static implicit operator T(LazyHolder<T> self){ return self.value; }
}
