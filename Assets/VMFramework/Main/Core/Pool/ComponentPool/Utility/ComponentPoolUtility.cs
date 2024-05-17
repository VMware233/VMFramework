using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VMFramework.Core.Pool
{
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
            out bool isFreshlyCreated)
            where T : Component
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
            out bool isFreshlyCreated)
            where T : Component
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
            bool resetLocalPositionAndRotation, out bool isFreshlyCreated)
            where T : Component
        {
            var newObject = pool.Get(() => new GameObject().AddComponent<T>(), out isFreshlyCreated);

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
            bool resetLocalPositionAndRotation)
            where T : Component
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
        public static void Prewarm<T>(this IComponentPool<T> pool, T prefab, Transform parent, int count)
            where T : Component
        {
            for (int i = 0; i < count; i++)
            {
                var newObject = Object.Instantiate(prefab, parent);

                pool.Return(newObject);
            }
        }

        public static void Prewarm<T>(this IComponentPool<T> pool, int count, Transform parent,
            Func<T> creator)
            where T : Component
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
}