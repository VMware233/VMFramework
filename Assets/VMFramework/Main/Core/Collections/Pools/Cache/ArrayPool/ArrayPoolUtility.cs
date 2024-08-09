using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace VMFramework.Core.Pools
{
    public static class ArrayPoolUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] ToLargerArrayPooled<T>(this IEnumerable<T> source)
        {
            var array = ArrayPool<T>.GetByMinLength(source.Count());

            var i = 0;

            foreach (var item in source)
            {
                array[i++] = item;
            }

            return array;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] ToLargerArrayPooled<T>(this IEnumerable<T> source, int count)
        {
            var array = ArrayPool<T>.GetByMinLength(count);

            var i = 0;

            foreach (var item in source)
            {
                array[i++] = item;
            }

            return array;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] ToArrayPooled<T>(this IEnumerable<T> source)
        {
            var array = ArrayPool<T>.Get(source.Count());

            var i = 0;

            foreach (var item in source)
            {
                array[i++] = item;
            }

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] ToArrayPooled<T>(this IEnumerable<T> source, int count)
        {
            var array = ArrayPool<T>.Get(source.Count());

            var i = 0;

            foreach (var item in source)
            {
                array[i++] = item;
            }

            return array;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] ToArrayPooled<T>(this IEnumerable<T> source, int start, int length)
        {
            var array = ArrayPool<T>.Get(length);

            var i = 0;
            
            foreach (var item in source.Skip(start))
            {
                if (i >= length) break;
                
                array[i++] = item;
            }
            
            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] ToArrayPooled<T>(this IReadOnlyCollection<T> source)
        {
            var array = ArrayPool<T>.Get(source.Count);

            var i = 0;

            foreach (var item in source)
            {
                array[i++] = item;
            }

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] ToArrayPooled<T>(this IReadOnlyList<T> source, int start, int length)
        {
            var array = ArrayPool<T>.Get(length);

            for (var i = 0; i < length; i++)
            {
                array[i] = source[start + i];
            }
            
            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReturnToPool<T>(this T[] array) => ArrayPool<T>.Return(array);
    }
}