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
        #region static

        static protected List<T> instances = new List<T>();
        static public T Instance => GetSortedList(instances.First()).First();

        static protected IOrderedEnumerable<T> GetSortedList(T sample)
        {
            instances.RemoveAll(ins => ins == null);
            return instances.Where(ins => sample.IsKeyEqual(ins)).OrderByDescending(ins => ins.priority);
        }

        #endregion

        public int priority;

        // 同じT内でも種類を分けたいときに指定
        protected virtual bool IsKeyEqual(T other) => true;
        protected virtual string label => name;

        protected IOrderedEnumerable<T> GetSortedList() => GetSortedList((T)this);

        protected bool isTop => GetSortedList().First() == this;

        protected virtual void Awake()
        {
            instances.Add((T)this);
        }

        int currentIdx;

        public virtual void DoGUI()
        {
            var list = GetSortedList((T)this);
            var count = list.Count();

            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label($"{label}: ");
                currentIdx = GUILayout.Toolbar(currentIdx, list.Select(ins => ins.name).ToArray());
            }

            var current = list.ElementAt(Mathf.Min(currentIdx, count - 1));
            var isTop = list.First() == current;

            var tmp = GUI.enabled;
            GUI.enabled = isTop;

            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Space(16f);
                using (new GUILayout.VerticalScope())
                {
                    current.DoGUIInternal();
                }
            }

            GUI.enabled = tmp;
        }

        protected virtual void DoGUIInternal() { }
    }
}