using UnityEngine;
using System.Collections.Generic;

public interface IStateMachine
{
	void Update();
	void FixedUpdate();
	
	void OnCollisionEnter2D(Collision2D coll);
	void OnCollisionStay2D(Collision2D coll);
	void OnCollisionExit2D(Collision2D coll);

	string currentName{ get; }
}

public class StateMachine<T, U> : IStateMachine where T : struct, System.IConvertible where U : StateBase
{
	public Dictionary<T, U> _states = new Dictionary<T, U>();
	T? _currentKey = null;
	T? _prevKey = null;
	public T currentKey { get{ return _currentKey.Value; }}
	public T prevKey { get{ return _prevKey.Value; }}

	public string currentName{ get{return currentKey.ToString();} }

    public void Add(T key, U state)
	{
		_states.Add(key, state);
        if (_currentKey == null)
        {
            _currentKey = key;
            Current.Enter();
        }
	}

	public void Change(T key)
	{
		Current.Exit();
		_prevKey = _currentKey;
		_currentKey = key;
        OnChange();
		Current.Enter();
	}

    protected virtual void OnChange() { }

    protected U Current { get { return _states[_currentKey.Value]; } }
    protected U Prev { get { return _states[prevKey];  } }

	public void Update(){ Current.Update(); }
	public void FixedUpdate(){ Current.FixedUpdate(); }

	public void OnCollisionEnter2D(Collision2D coll){ Current.OnCollisionEnter2D(coll); }
	public void OnCollisionStay2D(Collision2D coll){ Current.OnCollisionStay2D(coll); }
	public void OnCollisionExit2D(Collision2D coll){ Current.OnCollisionExit2D(coll); }

	public void OnTriggerEnter2D(Collider2D col){ Current.OnTriggerEnter2D(col); }
	public void OnTriggerStay2D(Collider2D col){ Current.OnTriggerStay2D(col); }
	public void OnTriggerExit2D(Collider2D col){ Current.OnTriggerExit2D(col); }
}

public abstract class StateBase
{
	public virtual void Enter(){}
	public virtual void Exit(){}
	public virtual void Update(){}
	public virtual void FixedUpdate(){}

	public virtual void OnCollisionEnter2D(Collision2D coll){}
	public virtual void OnCollisionStay2D(Collision2D coll){}
	public virtual void OnCollisionExit2D(Collision2D coll){}

	public virtual void OnTriggerEnter2D(Collider2D col){}
	public virtual void OnTriggerStay2D(Collider2D col){}
	public virtual void OnTriggerExit2D(Collider2D col){}
}
