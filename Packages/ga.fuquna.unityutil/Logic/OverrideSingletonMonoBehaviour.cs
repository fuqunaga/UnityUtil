using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityUtil
{
    /// <summary>
    /// 複数のインスタンスを共用するが、クラス外には最もpriorityの高いインスタンスとして振る舞う
    /// デフォルトパラメータを起きつつ、特定の状態では別のパラメータにしたいときなどに使う
    /// </summary>
    public class OverrideSingletonMonoBehaviour<T> : MonoBehaviour
        where T : OverrideSingletonMonoBehaviour<T>
    {
        #region Static

        protected static readonly List<T> Instances = new();
        public static T Instance => GetSortedList(Instances.First()).First();

        private static IEnumerable<T> GetSortedList(T sample)
        {
            Instances.RemoveAll(ins => ins == null);
            return Instances.Where(sample.IsKeyEqual).OrderByDescending(ins => ins.priority);
        }

        static void Add(T instance)
        {
            ClearAllInstanceList();
            Instances.Add(instance);
        }

        static void Remove(T instance)
        {
            if (Instances.Remove(instance))
            {
                ClearAllInstanceList();
            }
        }
        

        static void ClearAllInstanceList()
        {
            foreach (var ins in Instances) ins.ClearList();
        }

        #endregion

        
        public int priority;
        
        private List<T> _sortedList;
        protected List<T> SortedList => _sortedList ??= GetSortedList((T)this).ToList();
        
        // 同じT内でも種類を分けたいときに指定
        protected virtual bool IsKeyEqual(T other) => true;
        protected virtual string Label => name;

        protected bool IsTop => SortedList.First() == this;

        
        protected virtual void Awake() => Add((T)this);

        protected virtual void OnDestroy() => Remove((T)this);

        protected void ClearList() => _sortedList = null;

        protected int currentIdx;

        protected T GetCurrent()
        {
            return SortedList.ElementAt(Mathf.Min(currentIdx, SortedList.Count - 1));
        }
    }
}