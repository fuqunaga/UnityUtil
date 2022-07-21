using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtil
{

    public interface IStateMachine
    {
        void Update();
        void LateUpdate();
        void FixedUpdate();

        void OnCollisionEnter2D(Collision2D coll);
        void OnCollisionStay2D(Collision2D coll);
        void OnCollisionExit2D(Collision2D coll);

        string CurrentName { get; }
    }

    public class StateMachine<TKey, TState> : IStateMachine 
        where TKey : struct, IConvertible
        where TState : StateBase
    {
        public readonly Dictionary<TKey, TState> states = new();
        TKey? _currentKey;
        TKey? _prevKey;

        public TKey CurrentKey => _currentKey ?? throw new ArgumentNullException(nameof(_currentKey));
        public TKey PrevKey =>  _prevKey ?? throw new ArgumentNullException(nameof(_prevKey));

        public string CurrentName => CurrentKey.ToString();

        public void Add(TKey key, TState state)
        {
            states.Add(key, state);
            if (_currentKey == null)
            {
                _currentKey = key;
                Current.Enter();
            }
        }

        public void Change(TKey key)
        {
            Current.Exit();
            _prevKey = _currentKey;
            _currentKey = key;
            OnChange();
            Current.Enter();
        }

        public bool Has(TKey key) => states.ContainsKey(key);

        protected virtual void OnChange() { }

        protected TState Current { get { return states[_currentKey.Value]; } }
        protected TState Prev { get { return states[PrevKey]; } }

        public void Update() { Current.Update(); }
        public void LateUpdate() { Current.LateUpdate(); }
        public void FixedUpdate() { Current.FixedUpdate(); }

        public void OnCollisionEnter2D(Collision2D coll) { Current.OnCollisionEnter2D(coll); }
        public void OnCollisionStay2D(Collision2D coll) { Current.OnCollisionStay2D(coll); }
        public void OnCollisionExit2D(Collision2D coll) { Current.OnCollisionExit2D(coll); }

        public void OnTriggerEnter2D(Collider2D col) { Current.OnTriggerEnter2D(col); }
        public void OnTriggerStay2D(Collider2D col) { Current.OnTriggerStay2D(col); }
        public void OnTriggerExit2D(Collider2D col) { Current.OnTriggerExit2D(col); }
    }

    public abstract class StateBase
    {
        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
        public virtual void LateUpdate() { }
        public virtual void FixedUpdate() { }

        public virtual void OnCollisionEnter2D(Collision2D coll) { }
        public virtual void OnCollisionStay2D(Collision2D coll) { }
        public virtual void OnCollisionExit2D(Collision2D coll) { }

        public virtual void OnTriggerEnter2D(Collider2D col) { }
        public virtual void OnTriggerStay2D(Collider2D col) { }
        public virtual void OnTriggerExit2D(Collider2D col) { }
    }

    public abstract class StateWithMonoBehaviour<T> : StateBase
        where T : MonoBehaviour
    {
        protected T script;
        protected GameObject GameObject => script.gameObject;
        protected Transform Transform => script.transform;


        public StateWithMonoBehaviour<T> Init(T monoBehaviourScript)
        {
            script = monoBehaviourScript; 
            return this;
        }
    }
}