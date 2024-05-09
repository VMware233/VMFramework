using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VMFramework.Core
{
    #region Utility

    public static class ComponentPoolUtility
    {
        #region Get

        /// <summary>
        /// 从池中获取一个组件，如果池中为空则通过预制体prefab创建一个组件，
        /// 如果有父节点则将组件设置为父节点的一个子节点
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="prefab"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Get<T>(this IComponentPool<T> pool, T prefab, Transform parent)
            where T : Component
        {
            return pool.Get(prefab, parent, out _);
        }

        /// <summary>
        /// 从池中获取一个组件，如果池中为空则通过预制体prefab创建一个组件，
        /// 如果有父节点则将组件设置为父节点的一个子节点,
        /// 并通过isFreshlyCreated变量返回是否是新创建的对象
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="prefab"></param>
        /// <param name="parent"></param>
        /// <param name="isFreshlyCreated"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Get<T>(this IComponentPool<T> pool, T prefab, Transform parent,
            out bool isFreshlyCreated) where T : Component
        {
            var newObject = pool.Get(() =>
            {
                var newObject = Object.Instantiate(prefab, parent);
                return newObject;
            }, out isFreshlyCreated);

            if (parent != null)
            {
                newObject.transform.SetParent(parent);
            }

            return newObject;
        }
        
        /// <summary>
        /// 从池中获取一个组件，如果池中为空则通过创建函数creator创建一个组件，
        /// 如果有父节点则将组件设置为父节点的一个子节点,
        /// 并通过isFreshlyCreated变量返回是否是新创建的对象
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="creator"></param>
        /// <param name="parent"></param>
        /// <param name="isFreshlyCreated"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Get<T>(this IComponentPool<T> pool, Func<T> creator, Transform parent,
            out bool isFreshlyCreated) where T : Component
        {
            var newObject = pool.Get(creator, out isFreshlyCreated);

            if (parent != null)
            {
                newObject.transform.SetParent(parent);
            }

            return newObject;
        }
        
        /// <summary>
        /// 从池中获取一个组件，如果池中为空则通过创建函数creator创建一个组件，
        /// 如果有父节点则将组件设置为父节点的一个子节点
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="creator"></param>
        /// <param name="parent"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Get<T>(this IComponentPool<T> pool, Func<T> creator, Transform parent) 
            where T : Component
        {
            return pool.Get(creator, parent, out _);
        }

        /// <summary>
        /// 从池中获取一个组件，如果池中为空则创建一个新的GameObject并添加组件，
        /// 并且设置为父节点的一个子节点，如果resetLocalPositionAndRotation为true则重置本地坐标和旋转
        /// 并通过isFreshlyCreated变量返回是否是新创建的对象
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="parent"></param>
        /// <param name="resetLocalPositionAndRotation"></param>
        /// <param name="isFreshlyCreated"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Get<T>(this IComponentPool<T> pool, Transform parent,
            bool resetLocalPositionAndRotation, out bool isFreshlyCreated) where T : Component
        {
            var newObject = pool.Get(() => new GameObject().AddComponent<T>(),
                out isFreshlyCreated);

            newObject.transform.SetParent(parent);
            if (resetLocalPositionAndRotation)
            {
                newObject.transform.ResetLocalPositionAndRotation();
            }

            return newObject;
        }

        /// <summary>
        /// 从池中获取一个组件，如果池中为空则创建一个新的GameObject并添加组件
        /// 并且设置为父节点的一个子节点
        /// <para>如果<paramref name="resetLocalPositionAndRotation"/>为true则重置本地坐标和旋转</para>
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="parent"></param>
        /// <param name="resetLocalPositionAndRotation"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Get<T>(this IComponentPool<T> pool, Transform parent,
            bool resetLocalPositionAndRotation) where T : Component
        {
            return pool.Get(parent, resetLocalPositionAndRotation, out _);
        }

        #endregion

        #region Prewarm
        
        /// <summary>
        /// 对组件池进行预热，创建count个对象并放入池中
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="prefab"></param>
        /// <param name="parent"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Prewarm<T>(this IComponentPool<T> pool, T prefab, Transform parent,
            int count) where T : Component
        {
            for (int i = 0; i < count; i++)
            {
                var newObject = Object.Instantiate(prefab, parent);
                
                pool.Return(newObject);
            }
        }

        public static void Prewarm<T>(this IComponentPool<T> pool, int count, Transform parent,
            Func<T> creator) where T : Component
        {
            for (int i = 0; i < count; i++)
            {
                var newObject = creator();
                newObject.transform.SetParent(parent);

                pool.Return(newObject);
            }
        }

        #endregion
    }

    #endregion
    
    #region Interface

    public interface IComponentPool<T> : IPool<T> where T : Component
    {
       
    }

    public interface IComponentCheckablePool<T> : ICheckablePool<T>,
        IComponentPool<T> where T : Component
    {

    }

    public interface IComponentLimitPool<T> : ILimitPool<T>, IComponentPool<T>
        where T : Component
    {

    }

    public interface IComponentCheckableLimitPool<T> : ICheckableLimitPool<T>,
        IComponentLimitPool<T> where T : Component
    {

    }

    #endregion

    #region Generic

    #region Universal

    public abstract class ComponentPool<T> : IComponentPool<T> where T : Component
    {
        private Action<T> hideAction;
        private Action<T> showAction;
        private Action<T> destroyAction;

        protected ComponentPool(Action<T> hideAction = null,
            Action<T> showAction = null, Action<T> destroyAction = null)
        {
            this.hideAction = hideAction;
            this.showAction = showAction;
            this.destroyAction = destroyAction;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract T Get(Func<T> creator, out bool isFreshlyCreated);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract void Return(T item);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract bool IsEmpty();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract void Clear();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void Show(T item)
        {
            if (showAction != null)
            {
                showAction(item);
            }
            else
            {
                item.SetActive(true);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void Hide(T item)
        {
            if (hideAction != null)
            {
                hideAction(item);
            }
            else
            {
                item.SetActive(false);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void Destroy(T item)
        {
            if (destroyAction != null)
            {
                destroyAction(item);
            }
            else
            {
                Object.Destroy(item.gameObject);
            }
        }
    }

    public abstract class ComponentLimitPool<T> : ComponentPool<T>, IComponentLimitPool<T>
        where T : Component
    {
        [ShowInInspector]
        public int maxCapacity { get; private set; }

        protected ComponentLimitPool(int maxCapacity, Action<T> hideAction = null,
            Action<T> showAction = null, Action<T> destroyAction = null) : base(
            hideAction, showAction, destroyAction)
        {
            this.maxCapacity = maxCapacity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract bool IsFull();
    }

    #endregion

    #region Unlimited

    public abstract class ComponentReadOnlyCollectionPool<T, TCollection> : 
        ComponentPool<T>, IComponentCheckablePool<T>
        where T : Component
        where TCollection : IReadOnlyCollection<T>, new()
    {
        [ShowInInspector]
        protected TCollection pool = new();

        protected ComponentReadOnlyCollectionPool(Action<T> hideAction = null,
            Action<T> showAction = null, Action<T> destroyAction = null) : base(
            hideAction, showAction, destroyAction)
        {

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(T item)
        {
            return pool.Contains(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsEmpty()
        {
            return pool.Count == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Clear()
        {
            foreach (var component in pool)
            {
                Destroy(component);
            }

            pool = new TCollection();
        }
    }

    public class ComponentCollectionPool<T, TCollection> : 
        ComponentReadOnlyCollectionPool<T, TCollection>
        where T : Component
        where TCollection : IReadOnlyCollection<T>, ICollection<T>, new()
    {
        public ComponentCollectionPool(Action<T> hideAction = null,
            Action<T> showAction = null, Action<T> destroyAction = null) : base(
            hideAction, showAction, destroyAction)
        {

        }

        public override T Get(Func<T> creator, out bool isFreshlyCreated)
        {
            if (IsEmpty() == false)
            {
                var newOne = pool.First();
                pool.Remove(newOne);

                Show(newOne);

                isFreshlyCreated = false;
                return newOne;
            }

            isFreshlyCreated = true;
            return creator();
        }

        public override void Return(T item)
        {
            item.AssertIsNotNull(nameof(item));
            
            Hide(item);
            pool.Add(item);
        }
    }

    #endregion

    #region Limited

    public abstract class ComponentReadOnlyLimitedCollectionPool<T, TCollection> :
        ComponentLimitPool<T>, IComponentCheckableLimitPool<T>
        where T : Component
        where TCollection : IReadOnlyCollection<T>, new()
    {
        [ShowInInspector]
        protected TCollection pool = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(T item)
        {
            return pool.Contains(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsEmpty()
        {
            return pool.Count == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsFull()
        {
            return pool.Count >= maxCapacity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Clear()
        {
            foreach (var component in pool)
            {
                Destroy(component);
            }

            pool = new TCollection();
        }

        protected ComponentReadOnlyLimitedCollectionPool(int maxCapacity,
            Action<T> hideAction = null, Action<T> showAction = null,
            Action<T> destroyAction = null) : base(maxCapacity, hideAction,
            showAction, destroyAction)
        {

        }
    }

    public class
        ComponentLimitedCollectionPool<T, TCollection> :
            ComponentReadOnlyLimitedCollectionPool<T, TCollection>
        where T : Component
        where TCollection : IReadOnlyCollection<T>, ICollection<T>, new()
    {
        public ComponentLimitedCollectionPool(int maxCapacity,
            Action<T> hideAction = null, Action<T> showAction = null,
            Action<T> destroyAction = null) : base(maxCapacity, hideAction,
            showAction, destroyAction)
        {

        }

        public override T Get(Func<T> creator, out bool isFreshlyCreated)
        {
            if (IsEmpty() == false)
            {
                var newOne = pool.First();
                pool.Remove(newOne);

                Show(newOne);

                isFreshlyCreated = false;
                return newOne;
            }

            isFreshlyCreated = true;
            return creator();
        }

        public override void Return(T item)
        {
            item.AssertIsNotNull(nameof(item));
            
            if (IsFull())
            {
                Destroy(item);
            }
            else
            {
                Hide(item);
                pool.Add(item);
            }
        }
    }

    #endregion

    #endregion

    #region Unlimited

    public class ComponentHashSetPool<T> : ComponentCollectionPool<T, HashSet<T>>
        where T : Component
    {
        public ComponentHashSetPool(Action<T> hideAction = null,
            Action<T> showAction = null, Action<T> destroyAction = null) : base(
            hideAction, showAction, destroyAction)
        {

        }
    }

    public class ComponentStackPool<T> : ComponentReadOnlyCollectionPool<T, Stack<T>>
        where T : Component
    {
        public ComponentStackPool(Action<T> hideAction = null,
            Action<T> showAction = null, Action<T> destroyAction = null) : base(
            hideAction, showAction, destroyAction)
        {

        }

        public override T Get(Func<T> creator, out bool isFreshlyCreated)
        {
            if (pool.Count > 0)
            {
                var newOne = pool.Pop();
                Show(newOne);

                isFreshlyCreated = false;
                return newOne;
            }

            isFreshlyCreated = true;
            return creator();
        }

        public override void Return(T item)
        {
            item.AssertIsNotNull(nameof(item));
            
            Hide(item);
            pool.Push(item);
        }
    }

    public class ComponentQueuePool<T> : ComponentReadOnlyCollectionPool<T, Queue<T>>
        where T : Component
    {
        public ComponentQueuePool(Action<T> hideAction = null,
            Action<T> showAction = null, Action<T> destroyAction = null) : base(
            hideAction, showAction, destroyAction)
        {

        }

        public override T Get(Func<T> creator, out bool isFreshlyCreated)
        {
            if (pool.Count > 0)
            {
                var newOne = pool.Dequeue();
                Show(newOne);

                isFreshlyCreated = false;
                return newOne;
            }

            isFreshlyCreated = true;
            return creator();
        }

        public override void Return(T item)
        {
            item.AssertIsNotNull(nameof(item));
            
            Hide(item);
            pool.Enqueue(item);
        }
    }

    #endregion

    #region Limited

    public class ComponentHashSetLimitPool<T> : ComponentLimitedCollectionPool<T, HashSet<T>>
        where T : Component
    {
        public ComponentHashSetLimitPool(int maxCapacity,
            Action<T> hideAction = null, Action<T> showAction = null,
            Action<T> destroyAction = null) : base(maxCapacity, hideAction,
            showAction, destroyAction)
        {
        }
    }

    public class ComponentStackLimitPool<T> : ComponentReadOnlyLimitedCollectionPool<T, Stack<T>> 
        where T : Component
    {
        public ComponentStackLimitPool(int maxCapacity, Action<T> hideAction = null,
            Action<T> showAction = null, Action<T> destroyAction = null) : base(
            maxCapacity, hideAction, showAction, destroyAction)
        {

        }

        public override T Get(Func<T> creator, out bool isFreshlyCreated)
        {
            if (IsEmpty() == false)
            {
                var newOne = pool.Pop();
                Show(newOne);

                isFreshlyCreated = false;
                return newOne;
            }

            isFreshlyCreated = true;
            return creator();
        }

        public override void Return(T item)
        {
            item.AssertIsNotNull(nameof(item));
            
            if (IsFull())
            {
                Destroy(item);
            }
            else
            {
                Hide(item);
                pool.Push(item);
            }
        }
    }

    public class ComponentQueueLimitPool<T> : ComponentReadOnlyLimitedCollectionPool<T, Queue<T>> 
        where T : Component
    {
        public ComponentQueueLimitPool(int maxCapacity, Action<T> hideAction = null,
            Action<T> showAction = null, Action<T> destroyAction = null) : base(
            maxCapacity, hideAction, showAction, destroyAction)
        {

        }

        public override T Get(Func<T> creator, out bool isFreshlyCreated)
        {
            if (IsEmpty() == false)
            {
                var newOne = pool.Dequeue();
                Show(newOne);

                isFreshlyCreated = false;
                return newOne;
            }

            isFreshlyCreated = true;
            return creator();
        }

        public override void Return(T item)
        {
            item.AssertIsNotNull(nameof(item));
            
            if (IsFull())
            {
                Destroy(item);
            }
            else
            {
                Hide(item);
                pool.Enqueue(item);
            }
        }
    }

    public class ComponentArrayLimitPool<T> : ComponentLimitPool<T>
        where T : Component
    {
        private T[] pool;

        private int count;

        public ComponentArrayLimitPool(int maxCapacity, Action<T> hideAction = null,
            Action<T> showAction = null, Action<T> destroyAction = null) : base(
            maxCapacity, hideAction, showAction, destroyAction)
        {
            pool = new T[maxCapacity];
        }

        public override T Get(Func<T> creator, out bool isFreshlyCreated)
        {
            if (count > 0)
            {
                var newOne = pool[--count];
                Show(newOne);

                isFreshlyCreated = false;
                return newOne;
            }

            isFreshlyCreated = true;
            return creator();
        }

        public override void Return(T item)
        {
            item.AssertIsNotNull(nameof(item));
            
            if (count < maxCapacity)
            {
                Hide(item);
                pool[count++] = item;
            }
            else
            {
                Destroy(item);
            }
        }

        public override bool IsFull()
        {
            return count >= maxCapacity;
        }

        public override bool IsEmpty()
        {
            return count == 0;
        }

        public override void Clear()
        {
            for (int i = 0; i < count; i++)
            {
                Destroy(pool[i]);
            }

            count = 0;
        }
    }

    #endregion
}
