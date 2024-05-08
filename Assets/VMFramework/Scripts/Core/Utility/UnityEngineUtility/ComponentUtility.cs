using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VMFramework.Core
{
    public static class ComponentUtility
    {
        #region Query

        public static T QueryComponentInChildren<T>(this Component c,
            bool includingSelf) where T : Component
        {
            foreach (var transform in c.transform.GetAllChildren(includingSelf))
            {
                var component = transform.GetComponent<T>();

                if (component != null)
                {
                    return component;
                }
            }

            return default;
        }

        public static T QueryComponentInChildren<T>(this Component c, string name,
            bool includingSelf)
            where T : Component
        {
            foreach (var transform in c.transform.GetAllChildren(includingSelf))
            {
                if (transform.name != name)
                {
                    continue;
                }

                var component = transform.GetComponent<T>();

                if (component != null)
                {
                    return component;
                }
            }

            return default;
        }

        public static T QueryComponentInParents<T>(this Component c,
            bool includingSelf)
            where T : Component
        {
            foreach (var transform in c.transform.GetAllParents(includingSelf))
            {
                var component = transform.GetComponent<T>();

                if (component != null)
                {
                    return component;
                }
            }

            return default;
        }

        public static T QueryComponentInParents<T>(this Component c, string name,
            bool includingSelf)
            where T : Component
        {
            foreach (var transform in c.transform.GetAllParents(includingSelf))
            {
                if (transform.name != name)
                {
                    continue;
                }

                var component = transform.GetComponent<T>();

                if (component != null)
                {
                    return component;
                }
            }

            return default;
        }

        #endregion

        #region Find

        /// <summary>
        /// 在所有子物体中查找特定类型的组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="c"></param>
        /// <param name="includingSelf">查找对象是否也包含自身</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> FindComponentsInChildren<T>(this Component c,
            bool includingSelf)
            where T : Component
        {
            foreach (var transform in c.transform.GetAllChildren(includingSelf))
            {
                var components = transform.GetComponents<T>();

                foreach (var component in components)
                {
                    yield return component;
                }
            }
        }

        /// <summary>
        /// 在所有父物体中查找特定类型的组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="c"></param>
        /// <param name="includingSelf">查找对象是否也包含自身</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> FindComponentsInParents<T>(this Component c,
            bool includingSelf) where T : Component
        {
            foreach (var transform in c.transform.GetAllParents(includingSelf))
            {
                var components = transform.GetComponents<T>();

                foreach (var component in components)
                {
                    yield return component;
                }
            }
        }

        #endregion

        #region Has

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasComponent(this Component c, Type componentType)
        {
            return c.GetComponent(componentType) != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasComponent(this GameObject obj, Type componentType)
        {
            return obj.GetComponent(componentType) != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasComponent<T>(this Component c) where T : Component
        {
            return c.GetComponent<T>() != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasComponent<T>(this GameObject obj) where T : Component
        {
            return obj.GetComponent<T>() != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasComponentInChildren<T>(this Component c,
            bool includingSelf)
            where T : Component
        {
            return HasComponentInChildren(c, typeof(T), includingSelf);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasComponentInChildren(this Component c,
            Type componentType, bool includingSelf)
        {
            return c.transform.GetAllChildren(includingSelf)
                .Any(child => child.HasComponent(componentType));
        }

        #endregion

        #region Get Or Add

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [NotNull]
        public static T GetOrAddComponent<T>(this Component component)
            where T : Component
        {
            var result = component.GetComponent<T>();

            if (result == null)
            {
                result = component.gameObject.AddComponent<T>();
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [NotNull]
        public static Component GetOrAddComponent(this Component component,
            Type componentType)
        {
            var result = component.GetComponent(componentType);

            if (result == null)
            {
                result = component.gameObject.AddComponent(componentType);
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [NotNull]
        public static T GetOrAddComponent<T>(this GameObject gameObject)
            where T : Component
        {
            var result = gameObject.GetComponent<T>();

            if (result == null)
            {
                result = gameObject.AddComponent<T>();
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [NotNull]
        public static Component GetOrAddComponent(this GameObject gameObject,
            Type componentType)
        {
            var result = gameObject.GetComponent(componentType);

            if (result == null)
            {
                result = gameObject.AddComponent(componentType);
            }

            return result;
        }

        #endregion

        #region Add

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddComponent<T>(this Component component) where T : Component
        {
            return component.gameObject.AddComponent<T>();
        }

        #endregion

        #region Remove First

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveFirstComponent(this Component component,
            Type componentType)
        {
            var target = component.GetComponent(componentType);

            if (target != null)
            {
                Object.Destroy(target);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveFirstComponent(this GameObject gameObject,
            Type componentType)
        {
            var target = gameObject.GetComponent(componentType);

            if (target != null)
            {
                Object.Destroy(target);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveFirstComponent<T>(this GameObject gameObject)
            where T : Component
        {
            gameObject.RemoveFirstComponent(typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveFirstComponent<T>(this Component component)
            where T : Component
        {
            component.RemoveFirstComponent(typeof(T));
        }

        #endregion

        #region Remove First Immediate

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveFirstComponentImmediate(this Component component,
            Type componentType)
        {
            var target = component.GetComponent(componentType);

            if (target != null)
            {
                Object.DestroyImmediate(target);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveFirstComponentImmediate(this GameObject gameObject,
            Type componentType)
        {
            var target = gameObject.GetComponent(componentType);

            if (target != null)
            {
                Object.DestroyImmediate(target);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveFirstComponentImmediate<T>(this GameObject gameObject)
            where T : Component
        {
            gameObject.RemoveFirstComponentImmediate(typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveFirstComponentImmediate<T>(this Component component)
            where T : Component
        {
            component.RemoveFirstComponentImmediate(typeof(T));
        }

        #endregion

        #region Remove All

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveAllComponents<T>(this GameObject gameObject)
            where T : Component
        {
            var targets = gameObject.GetComponents<T>();

            foreach (var target in targets)
            {
                Object.Destroy(target);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveAllComponents<T>(this Component component)
            where T : Component
        {
            RemoveAllComponents<T>(component.gameObject);
        }

        #endregion
    }
}
