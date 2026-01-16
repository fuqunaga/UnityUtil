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

        string CurrentName { get; }
    }

    public class StateMachine<TKey, TState> : IStateMachine 
        where TKey : struct, IConvertible
        where TState : StateBase
    {
        public readonly Dictionary<TKey, TState> states = new();
        private TKey? _currentKey;
        private TKey? _prevKey;

        public TKey CurrentKey => _currentKey ?? throw new ArgumentNullException(nameof(_currentKey));
        public TKey PrevKey =>  _prevKey ?? throw new ArgumentNullException(nameof(_prevKey));

        public string CurrentName => CurrentKey.ToString();

        public virtual void Clear()
        {
            states.Clear();
            _currentKey = null;
            _prevKey = null;
        }
        
        public virtual void Add(TKey key, TState state)
        {
            states.Add(key, state);
            if (_currentKey == null)
            {
                _currentKey = key;
                Current.Enter();
            }
        }

        public virtual void Change(TKey key)
        {
            Current.Exit();
            _prevKey = _currentKey;
            _currentKey = key;
            OnChange();
            Current.Enter();
        }

        public bool Has(TKey key) => states.ContainsKey(key);

        protected virtual void OnChange() { }

        protected TState Current
        {
            get
            {
                if (_currentKey is { } key && states.TryGetValue(key, out var state))
                {
                    return state;
                }

                return null;
            }
        }

        protected TState Prev {
            get
            {
                if (_prevKey is { } key && states.TryGetValue(key, out var state))
                {
                    return state;
                }

                return null;
            }
        }

        public virtual void Update() => Current.Update();
        public virtual void LateUpdate() => Current.LateUpdate();
        public virtual void FixedUpdate() => Current.FixedUpdate();
    }

    public abstract class StateBase
    {
        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
        public virtual void LateUpdate() { }
        public virtual void FixedUpdate() { }
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