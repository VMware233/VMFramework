using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class ArrayWriteUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set<T>(this T[,,] array, Vector3Int pos, T content)
        {
            array[pos.x, pos.y, pos.z] = content;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set<T>(this T[,,] array, T content)
        {
            foreach (var pos in array.GetSize().GetCube())
            {
                array.Set(pos, content);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Remove<T>(this T[,,] array, Vector3Int pos, out T item)
        {
            ReferenceUtility.Exchange(ref array[pos.x, pos.y, pos.z], default, out item);
        }
    }
}