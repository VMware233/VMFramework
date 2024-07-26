using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Core.Pools
{
    public static class ArrayPool<T>
    {
        private static readonly object lockObj = new();
        private static readonly Dictionary<int, Stack<T[]>> free = new();
        private static readonly HashSet<T[]> busy = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] New(int length)
        {
            lock (lockObj)
            {
                if (!free.ContainsKey(length))
                {
                    free.Add(length, new Stack<T[]>());
                }

                if (free[length].Count == 0)
                {
                    free[length].Push(new T[length]);
                }

                var array = free[length].Pop();

                busy.Add(array);

                return array;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Free(T[] array)
        {
            lock (lockObj)
            {
                if (!busy.Contains(array))
                {
                    throw new ArgumentException("The array to free is not in use by the pool.", nameof(array));
                }

                for (var i = 0; i < array.Length; i++)
                {
                    array[i] = default(T);
                }

                busy.Remove(array);

                free[array.Length].Push(array);
            }
        }
    }
}